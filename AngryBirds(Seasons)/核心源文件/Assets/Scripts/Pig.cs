using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pig : MonoBehaviour
{
    //使用动量守恒：设小鸟质量为m，速度为v，猪质量为M，碰撞后小鸟的速度为（M-m)*v/(M+m)，猪的速度为2mv/(M+m)
    //因此比较小鸟与猪碰撞前的相对速度和比较碰撞后猪的速度的变化量大小其实一样，都可以判断猪的状态

    public float maxSpeed = 10;
    public float minSpeed = 2;
    public Sprite hurt;             //猪受伤的图片
    private SpriteRenderer render;  //当前猪的状态图片
    public GameObject boom;         //猪的死亡爆炸效果
    public GameObject score;        //猪死亡的得分效果

    public AudioClip hurtClip;       //猪或者建筑物碰撞时使用的音乐组件
    public AudioClip dead;           //猪死亡或者建筑毁坏是使用的音乐组件
    public AudioClip birdCollision;  //小鸟碰撞时使用的音乐组件

    public bool isPig = false;      //由于猪和木块的效果差不多，只是相对速度有所差别，所以用一个bool判断是否为猪即可，其他的都可以共同调用
    public bool isDead = false;     //判断当前物体是否已经触发Dead方法
    private void Awake()        //初始化
    {
        render = GetComponent<SpriteRenderer>();    //获取当前猪的状态的图片
    }

    private void OnCollisionEnter2D(Collision2D collision)//碰撞检测
    {
        //print(collision.relativeVelocity.magnitude);      //显示相对速度，合理选取受伤和死亡的取值范围
        //collision.relativeVelocity（相对速度）是向量，需要转换为标量
        if(collision.gameObject.tag == "Player")    //如果小鸟碰撞的是猪或者建筑物
        {
            Audioplay(birdCollision);    //播放小鸟碰撞时使用的音乐组件
            collision.transform.GetComponent<Bird>().Hurt();  //更新小鸟受伤图片
        }
        
        if (collision.relativeVelocity.magnitude > maxSpeed)//碰撞相对速度大于最大速度，直接死亡
        {
            Dead();     //猪死亡的效果处理
        }
        else if (collision.relativeVelocity.magnitude > minSpeed && collision.relativeVelocity.magnitude < maxSpeed) //相对速度位于最大和最小速度之间为受伤状态
        {
            render.sprite = hurt;   //图片更新为受伤图片
            Audioplay(hurtClip);    //播放猪或者建筑物碰撞时使用的音乐组件
        }
    }
    
    public void Dead()
    {
        isDead = true;      //已经死亡
        if(isPig)
        {
            GameManager._instance.pig.Remove(this);     //如果是猪，从列表中移除它
        }
        Destroy(gameObject); //猪或建筑物死亡后销毁
        Instantiate(boom, transform.position,Quaternion.identity);  //产生爆炸效果
        
        GameObject go = Instantiate(score, transform.position + new Vector3(0,0.5f,0), Quaternion.identity);    //产生得分效果
        Destroy(go,2);   //销毁得分特效

        Audioplay(dead); //播放猪死亡或者建筑毁坏是使用的音乐组件
    }
    public void Audioplay(AudioClip clip)     //播放音乐的方法
    {
        AudioSource.PlayClipAtPoint(clip, transform.position);   //在当前位置播放音乐
    }
}
