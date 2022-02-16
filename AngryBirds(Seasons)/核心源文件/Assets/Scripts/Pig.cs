using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pig : MonoBehaviour
{
    //ʹ�ö����غ㣺��С������Ϊm���ٶ�Ϊv��������ΪM����ײ��С����ٶ�Ϊ��M-m)*v/(M+m)������ٶ�Ϊ2mv/(M+m)
    //��˱Ƚ�С��������ײǰ������ٶȺͱȽ���ײ������ٶȵı仯����С��ʵһ�����������ж����״̬

    public float maxSpeed = 10;
    public float minSpeed = 2;
    public Sprite hurt;             //�����˵�ͼƬ
    private SpriteRenderer render;  //��ǰ���״̬ͼƬ
    public GameObject boom;         //���������ըЧ��
    public GameObject score;        //�������ĵ÷�Ч��

    public AudioClip hurtClip;       //����߽�������ײʱʹ�õ��������
    public AudioClip dead;           //���������߽����ٻ���ʹ�õ��������
    public AudioClip birdCollision;  //С����ײʱʹ�õ��������

    public bool isPig = false;      //�������ľ���Ч����ֻ࣬������ٶ��������������һ��bool�ж��Ƿ�Ϊ���ɣ������Ķ����Թ�ͬ����
    public bool isDead = false;     //�жϵ�ǰ�����Ƿ��Ѿ�����Dead����
    private void Awake()        //��ʼ��
    {
        render = GetComponent<SpriteRenderer>();    //��ȡ��ǰ���״̬��ͼƬ
    }

    private void OnCollisionEnter2D(Collision2D collision)//��ײ���
    {
        //print(collision.relativeVelocity.magnitude);      //��ʾ����ٶȣ�����ѡȡ���˺�������ȡֵ��Χ
        //collision.relativeVelocity������ٶȣ�����������Ҫת��Ϊ����
        if(collision.gameObject.tag == "Player")    //���С����ײ��������߽�����
        {
            Audioplay(birdCollision);    //����С����ײʱʹ�õ��������
            collision.transform.GetComponent<Bird>().Hurt();  //����С������ͼƬ
        }
        
        if (collision.relativeVelocity.magnitude > maxSpeed)//��ײ����ٶȴ�������ٶȣ�ֱ������
        {
            Dead();     //��������Ч������
        }
        else if (collision.relativeVelocity.magnitude > minSpeed && collision.relativeVelocity.magnitude < maxSpeed) //����ٶ�λ��������С�ٶ�֮��Ϊ����״̬
        {
            render.sprite = hurt;   //ͼƬ����Ϊ����ͼƬ
            Audioplay(hurtClip);    //��������߽�������ײʱʹ�õ��������
        }
    }
    
    public void Dead()
    {
        isDead = true;      //�Ѿ�����
        if(isPig)
        {
            GameManager._instance.pig.Remove(this);     //����������б����Ƴ���
        }
        Destroy(gameObject); //�����������������
        Instantiate(boom, transform.position,Quaternion.identity);  //������ըЧ��
        
        GameObject go = Instantiate(score, transform.position + new Vector3(0,0.5f,0), Quaternion.identity);    //�����÷�Ч��
        Destroy(go,2);   //���ٵ÷���Ч

        Audioplay(dead); //�������������߽����ٻ���ʹ�õ��������
    }
    public void Audioplay(AudioClip clip)     //�������ֵķ���
    {
        AudioSource.PlayClipAtPoint(clip, transform.position);   //�ڵ�ǰλ�ò�������
    }
}
