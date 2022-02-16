using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackBird : Bird                //�̳�һ�����Ľű�
{

    public List<Pig> blocks = new List<Pig>(); //������������Ժ���ΪԲ�ĵ�Բ�пɱ�ը������ͽ������Enemy�ı�ǩ��
    private void OnTriggerEnter2D(Collider2D collision) //����ԲȦ��Ĵ�������
    {
        if (collision.gameObject.tag == "Enemy")  //�����ǩ��Enemy
        {
            blocks.Add(collision.gameObject.GetComponent<Pig>());
        }
    }
    private void OnTriggerExit2D(Collider2D collision)  //�뿪ԲȦ��Ĵ�������
    {
        if (collision.gameObject.tag == "Enemy")  //�����ǩ��Enemy
        {
            if (collision.GetComponent<Pig>().isDead == false)   //֮ǰû�д���Dead����
                blocks.Remove(collision.gameObject.GetComponent<Pig>());
        }
    }

    public override void ShowSkill()        //��д���ܰ��
    {
        base.ShowSkill();                    //�̳л���
        if (blocks.Count > 0 && blocks != null)     //��������ϰ���
        {
            for (int i = 0; i < blocks.Count; i++)  //��ը����ϰ���
            {
                blocks[i].Dead();    //��ը����ϰ���
            }
        }
        OnClear();  //�������
    }

    void OnClear()  //���ڱ�ը��Ĵ���
    {
        rg.velocity = Vector3.zero;     //�ٶȱ�Ϊ0
        Instantiate(boom, transform.position, Quaternion.identity); //���ű�ը��Ч
        render.enabled = false;         //����ʾͼƬ
        GetComponent<CircleCollider2D>().enabled = false;   //���ɵ��
        myTrail.ClearTrails();      //��βЧ����ʧ
    }
    protected override void Next()
    {
        //GameManager._instance.birds.RemoveAt(0);   //�ѷɳ���С���С��ļ������Ƴ�
        GameManager._instance.birds.Remove(this);   //�ѷɳ���С���С��ļ������Ƴ�
        Destroy(gameObject);                        //����С��
        GameManager._instance.NextBird();           //���ݽ��������Ϸ�߼����ж�
    }
}


