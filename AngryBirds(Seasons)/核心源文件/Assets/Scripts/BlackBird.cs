using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackBird : Bird                //继承一般红鸟的脚本
{

    public List<Pig> blocks = new List<Pig>(); //这个集合里存放以黑炮为圆心的圆中可爆炸销毁猪和建筑物（有Enemy的标签）
    private void OnTriggerEnter2D(Collider2D collision) //进入圆圈里的触发区域
    {
        if (collision.gameObject.tag == "Enemy")  //如果标签是Enemy
        {
            blocks.Add(collision.gameObject.GetComponent<Pig>());
        }
    }
    private void OnTriggerExit2D(Collider2D collision)  //离开圆圈里的触发区域
    {
        if (collision.gameObject.tag == "Enemy")  //如果标签是Enemy
        {
            if (collision.GetComponent<Pig>().isDead == false)   //之前没有触发Dead方法
                blocks.Remove(collision.gameObject.GetComponent<Pig>());
        }
    }

    public override void ShowSkill()        //重写技能板块
    {
        base.ShowSkill();                    //继承基类
        if (blocks.Count > 0 && blocks != null)     //如果存在障碍物
        {
            for (int i = 0; i < blocks.Count; i++)  //爆炸清除障碍物
            {
                blocks[i].Dead();    //爆炸清除障碍物
            }
        }
        OnClear();  //处理后事
    }

    void OnClear()  //黑炮爆炸后的处理
    {
        rg.velocity = Vector3.zero;     //速度变为0
        Instantiate(boom, transform.position, Quaternion.identity); //播放爆炸特效
        render.enabled = false;         //不显示图片
        GetComponent<CircleCollider2D>().enabled = false;   //不可点击
        myTrail.ClearTrails();      //拖尾效果消失
    }
    protected override void Next()
    {
        //GameManager._instance.birds.RemoveAt(0);   //把飞出的小鸟从小鸟的集合中移除
        GameManager._instance.birds.Remove(this);   //把飞出的小鸟从小鸟的集合中移除
        Destroy(gameObject);                        //销毁小鸟
        GameManager._instance.NextBird();           //根据结果进行游戏逻辑的判断
    }
}


