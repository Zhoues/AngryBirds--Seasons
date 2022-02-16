using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;     //���ڽ����ͣ���浥�����ᴥ��С���ܵ�bug

public class Bird : MonoBehaviour   //ʵ��С�����ק
{
    public float maxDis = 1.5f;        //��������������������󳤶�������С��λ��

    private bool isClick = false;   //bool�����ж�����Ƿ���
    [HideInInspector]
    public bool canMove = false;    //��֤С��ɳ�ȥ֮������һֻС���ϵ���֮���С�񲻿��Ա�������
    public bool isFly = false;      //�ж��Ƿ��ڷ���״̬�����������ûƷ棨����ʵ�ּ̳У�ͬʱ���Ϸ��й����е������ʵ�ּ��ٵ�Ч��

    [HideInInspector]//�ж�С���ڵ�����ʱ������ͣ�������û������Ȼ���ɿ������ڽ������ͣҳ��Ҳ���Ե��С����ڵ�����������ȡ����ͣ��С��ʧЧ��bug��
    public bool isReleased = false;  
    
    
    [HideInInspector]              //�ѹ����������������������û��Ķ�
    public SpringJoint2D sp;       //���ڿ��Ƶ�����ڶ�������Ч��(����Ϊ��������Ϊ���Դ�����GameManager�й��������С�������Ч��)
    protected Rigidbody2D rg;      //���ڿ��Ʒɳ���С������״̬(����Ϊ�������ͣ���翴�����������ڲ����Ե���)

    public LineRenderer right;      //�����ұ߻���Ƥ��
    public LineRenderer left;       //������߻���Ƥ��
    public Transform rightPos;      //�������������ڴ��뵯���������ĵ�λ��
    public Transform leftPos;      //�������������ڴ��뵯���������ĵ�λ��

    public Sprite hurt;             //С�����˵�ͼƬ
    protected SpriteRenderer render;  //��ǰС���״̬ͼƬ
    public GameObject boom;        //С����ʧ����Ч
    protected TestMyTrail myTrail;    //С����βЧ��

    public float smooth = 3.5f;        //�������ƶ���ƽ����ֵ

    public AudioClip select;        //ѡ��С����ʹ�õ��������
    public AudioClip fly;           //С�������ʹ�õ��������

    private void Awake()            //��ʼ��
    {
        sp = GetComponent<SpringJoint2D>(); //��ȡ��ǰ������ڶ�������Ч��
        rg = GetComponent<Rigidbody2D>();   //��ȡ��ǰ����������˶�״̬
        myTrail = GetComponent<TestMyTrail>();//��ȡ��ǰ��β״̬
        render = GetComponent<SpriteRenderer>();    //��ȡ��ǰС���״̬��ͼƬ
    }
    private void OnMouseDown()  //��갴��
    {
        if(canMove)
        {
            Audioplay(select);      //����ѡ����Ч
            isClick = true;
            rg.isKinematic = true;  //����������ѧ
        }
        
    }
    private void OnMouseUp()    //���̧��
    {
        if (canMove)
        {
            isClick = false;
            rg.isKinematic = false; //�ر�������ѧ
            Invoke("Fly", 0.1f);    //�ӳ�0.1s�������㹻���ʱ������������
            
            //���û������
            right.enabled = false;  //�����������ĵĻ���Ч����ʧ
            left.enabled = false;   //�����������ĵĻ���Ч����ʧ

            canMove = false;        //�ɳ�ȥ��С���ٱ�������
        }
       
    }
    private void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject())      //�Ƿ��ڵ��UI���棨�����ͣ�����ᴥ��С���ܵ�bug��
            return;
        if (isClick)    //���һֱ����,����λ�ø���
        {    
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);   //����ϵת��
            
            //transform.position += new Vector3(0, 0, 10);
            transform.position += new Vector3(0, 0, -Camera.main.transform.position.z); //С��ͼ����ȵĸı�

            if(Vector3.Distance(transform.position,rightPos.position) > maxDis) //����λ���޶�
            {
                Vector3 pos = (transform.position - rightPos.position).normalized;  //��ôӵ���������ָ��С��ĵ�λ����
                pos *= maxDis;  //��󳤶�����
                transform.position = pos + rightPos.position;   //����С��Ļ��Χ
            }

            Line();     //��ʼ���ߣ�����Ƥ���Ч��
        }

        //�������
        float posX = transform.position.x;  //��ȡ������С���һάx���λ��
        float posY = transform.position.y;  //��ȡ������С���һάx���λ��

        //Lerp( , , )ͨ��������ֵʵ�������ƽ���ظ��棬��һ�����ǵ�ǰĿ��㣬�ڶ�����Ŀ�ĵأ�������Ϊ��ǰ�˶�����(ƽ���� * ʱ����)
        //Mathf.Clamp(value,min,max)    ��value������min��max֮��                                      ���x�ķ�Χ           

        Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, new Vector3(Mathf.Clamp(posX, 0, 40), Mathf.Clamp(posY, 0, 15),
            Camera.main.transform.position.z), smooth * Time.deltaTime);

        if (isFly)       //������ڷ���״̬
        {
            if(Input.GetMouseButtonDown(0))     //��ʱ�������
            {
                ShowSkill();            //����С����
            }
        }
    
    
    }
    void Fly()
    {
        isFly = true;           //�ڷ���;��
        isReleased = true;      //�Ѿ����ͷţ���ʱ�뵯��û����ϵ
        Audioplay(fly);         //����������Ч
        myTrail.StartTrails();  //������βЧ��
        sp.enabled = false;     //������ڶ�������Ч����ʧ
        Invoke("Next", 5);      //С��ɳ�5s֮�󣬷ֱ�����ִ���Ƴ��б��е�С������С�񣬳���С����ʧ��Ч
    }
    void Line() //���߲���������ȷ��һ��ֱ�ߣ�
    {
        //���û������
        right.enabled = true;      //�����������ĵĻ���Ч������
        left.enabled = true;        //�����������ĵĻ���Ч������

        right.SetPosition(0, rightPos.position);        //����ȷ��һ��ֱ��
        right.SetPosition(1, transform.position);

        left.SetPosition(0, leftPos.position);          //����ȷ��һ��ֱ��
        left.SetPosition(1, transform.position);
    }
    protected virtual void Next()     //������һֻС��ķɳ�������Ϊprotected�Ƿ�ֹ���ڳ������α�ը��Ч��
    {
        //GameManager._instance.birds.RemoveAt(0);   //�ѷɳ���С���С��ļ������Ƴ�
        GameManager._instance.birds.Remove(this);   //�ѷɳ���С���С��ļ������Ƴ�
        Destroy(gameObject);                        //����С��
        Instantiate(boom, transform.position, Quaternion.identity); //��ʧС���λ�ô�������ը��Ч
        GameManager._instance.NextBird();           //���ݽ��������Ϸ�߼����ж�
    }

    private void OnCollisionEnter2D(Collision2D collision)  //��ײ���
    {
        isFly = false;                  //С��ײ����������״̬ȡ��
        myTrail.ClearTrails();          //С��������������ײ����βЧ����ʧ
    }

    public void Audioplay(AudioClip clip)     //�������ֵķ���
    {
        AudioSource.PlayClipAtPoint(clip,transform.position);   //�ڵ�ǰλ�ò�������
    }

    public virtual void ShowSkill()     //С����ֲ�ͬ�ļ���(�鷽�����������ؼ̳�)
    {
        isFly = false;          //���й��̽�����������ʵ��С����
    }

    public void Hurt()          //������ײ���˺��Ч��
    {
        render.sprite = hurt;           //ͼƬ����Ϊ����ͼƬ
    }
}
