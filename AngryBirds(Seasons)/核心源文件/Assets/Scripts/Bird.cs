using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;     //用于解决暂停界面单击鼠标会触发小鸟技能的bug

public class Bird : MonoBehaviour   //实现小鸟的拖拽
{
    public float maxDis = 1.5f;        //公共变量，用于输入最大长度以限制小鸟位置

    private bool isClick = false;   //bool变量判断鼠标是否点击
    [HideInInspector]
    public bool canMove = false;    //保证小鸟飞出去之后与下一只小鸟上弹弓之间该小鸟不可以被鼠标控制
    public bool isFly = false;      //判断是否处于飞行状态，这样方便让黄锋（黄鸟）实现继承，同时加上飞行过程中单击左键实现加速的效果

    [HideInInspector]//判断小鸟在弹弓上时，非暂停情况下有没有拉扯然后松开（用于解决在暂停页面也可以点击小鸟放在弹弓上以至于取消暂停后小鸟失效的bug）
    public bool isReleased = false;  
    
    
    [HideInInspector]              //把公共变量隐藏起来，不让用户改动
    public SpringJoint2D sp;       //用于控制弹动与摆动的物理效果(设置为公共是因为可以传输至GameManager中管理待发射小鸟的物理效果)
    protected Rigidbody2D rg;      //用于控制飞出后小鸟刚体的状态(设置为保护类型，外界看不到，但是内部可以调用)

    public LineRenderer right;      //弹弓右边划线皮筋
    public LineRenderer left;       //弹弓左边划线皮筋
    public Transform rightPos;      //公共变量，用于传入弹弓右子中心的位置
    public Transform leftPos;      //公共变量，用于传入弹弓左子中心的位置

    public Sprite hurt;             //小鸟受伤的图片
    protected SpriteRenderer render;  //当前小鸟的状态图片
    public GameObject boom;        //小鸟消失的特效
    protected TestMyTrail myTrail;    //小鸟拖尾效果

    public float smooth = 3.5f;        //解决相机移动的平滑度值

    public AudioClip select;        //选择小鸟是使用的音乐组件
    public AudioClip fly;           //小鸟飞行是使用的音乐组件

    private void Awake()            //初始化
    {
        sp = GetComponent<SpringJoint2D>(); //获取当前弹动与摆动的物理效果
        rg = GetComponent<Rigidbody2D>();   //获取当前刚体的物理运动状态
        myTrail = GetComponent<TestMyTrail>();//获取当前拖尾状态
        render = GetComponent<SpriteRenderer>();    //获取当前小鸟的状态的图片
    }
    private void OnMouseDown()  //鼠标按下
    {
        if(canMove)
        {
            Audioplay(select);      //开启选择音效
            isClick = true;
            rg.isKinematic = true;  //开启物理动力学
        }
        
    }
    private void OnMouseUp()    //鼠标抬起
    {
        if (canMove)
        {
            isClick = false;
            rg.isKinematic = false; //关闭物理动力学
            Invoke("Fly", 0.1f);    //延迟0.1s，给予足够多的时间进行物理计算
            
            //禁用划线组件
            right.enabled = false;  //弹弓右子中心的划线效果消失
            left.enabled = false;   //弹弓左子中心的划线效果消失

            canMove = false;        //飞出去的小鸟不再被鼠标控制
        }
       
    }
    private void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject())      //是否在点击UI界面（解决暂停后点击会触发小鸟技能的bug）
            return;
        if (isClick)    //鼠标一直按下,进行位置跟随
        {    
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);   //坐标系转换
            
            //transform.position += new Vector3(0, 0, 10);
            transform.position += new Vector3(0, 0, -Camera.main.transform.position.z); //小鸟图层深度的改变

            if(Vector3.Distance(transform.position,rightPos.position) > maxDis) //进行位置限定
            {
                Vector3 pos = (transform.position - rightPos.position).normalized;  //获得从弹弓子中心指向小鸟的单位向量
                pos *= maxDis;  //最大长度向量
                transform.position = pos + rightPos.position;   //限制小鸟的活动范围
            }

            Line();     //开始划线，出现皮筋的效果
        }

        //相机跟随
        float posX = transform.position.x;  //获取到待飞小鸟的一维x轴的位置
        float posY = transform.position.y;  //获取到待飞小鸟的一维x轴的位置

        //Lerp( , , )通过向量插值实现主相机平滑地跟随，第一个点是当前目标点，第二个是目的地，第三给为当前运动速率(平滑度 * 时间间隔)
        //Mathf.Clamp(value,min,max)    把value限制在min和max之间                                      相机x的范围           

        Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, new Vector3(Mathf.Clamp(posX, 0, 40), Mathf.Clamp(posY, 0, 15),
            Camera.main.transform.position.z), smooth * Time.deltaTime);

        if (isFly)       //如果处于飞行状态
        {
            if(Input.GetMouseButtonDown(0))     //此时单击鼠标
            {
                ShowSkill();            //触发小鸟技能
            }
        }
    
    
    }
    void Fly()
    {
        isFly = true;           //在飞行途中
        isReleased = true;      //已经被释放，此时与弹弓没有联系
        Audioplay(fly);         //开启飞行音效
        myTrail.StartTrails();  //开启拖尾效果
        sp.enabled = false;     //弹动与摆动的物理效果消失
        Invoke("Next", 5);      //小鸟飞出5s之后，分别依次执行移除列表中的小鸟，销毁小鸟，出现小鸟消失特效
    }
    void Line() //划线操作（两点确定一条直线）
    {
        //启用划线组件
        right.enabled = true;      //弹弓右子中心的划线效果开启
        left.enabled = true;        //弹弓左子中心的划线效果开启

        right.SetPosition(0, rightPos.position);        //两点确定一条直线
        right.SetPosition(1, transform.position);

        left.SetPosition(0, leftPos.position);          //两点确定一条直线
        left.SetPosition(1, transform.position);
    }
    protected virtual void Next()     //处理下一只小鸟的飞出（设置为protected是防止黑炮出现两次爆炸特效）
    {
        //GameManager._instance.birds.RemoveAt(0);   //把飞出的小鸟从小鸟的集合中移除
        GameManager._instance.birds.Remove(this);   //把飞出的小鸟从小鸟的集合中移除
        Destroy(gameObject);                        //销毁小鸟
        Instantiate(boom, transform.position, Quaternion.identity); //消失小鸟的位置处产生爆炸特效
        GameManager._instance.NextBird();           //根据结果进行游戏逻辑的判断
    }

    private void OnCollisionEnter2D(Collision2D collision)  //碰撞检测
    {
        isFly = false;                  //小鸟撞到物体后飞行状态取消
        myTrail.ClearTrails();          //小鸟与其他物体碰撞后拖尾效果消失
    }

    public void Audioplay(AudioClip clip)     //播放音乐的方法
    {
        AudioSource.PlayClipAtPoint(clip,transform.position);   //在当前位置播放音乐
    }

    public virtual void ShowSkill()     //小鸟各种不同的技能(虚方法，方便重载继承)
    {
        isFly = false;          //飞行过程结束，不可再实现小鸟技能
    }

    public void Hurt()          //产生碰撞受伤后的效果
    {
        render.sprite = hurt;           //图片更新为受伤图片
    }
}
