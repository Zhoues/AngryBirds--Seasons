using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;          //���ڲ�ͬ��������ת

public class Introduction : MonoBehaviour
{
    public GameObject introdution;          //���̷ֽ̳���
    public GameObject start;                //��ʼ���ֽ̳�
    public GameObject sling_intro;          //���ܵ���ʹ��
    public GameObject target_intro;         //������ϷĿ��
    public GameObject star_intro;           //������ϷĿ��
    public GameObject bird_intro;           //������ϷĿ��
    public GameObject bird_intro2;           //������ϷĿ��

    public GameObject continue_1;           //�����굯����׼��������ϷĿ��
    public GameObject continue_2;           //��������ϷĿ�꣬׼�����ܹؿ�
    public GameObject continue_3;           //������ؿ���׼������С��
    public GameObject continue_4;           //��������С��
    //public GameObject skip;               //�������ֽ̳�

    public void Skip()                  //����ע������¼�����������жϸùؿ��Ƿ񿪷�
    {
            SceneManager.LoadScene(4);              //File��Build Setting�ж���02-Game������ģ�����Ϊ2
    }
    public void StartNow()
    {
        introdution.SetActive(false);
        sling_intro.SetActive(true);
    }
    public void Continue_1()
    {
        sling_intro.SetActive(false);
        target_intro.SetActive(true);
    }
    public void Continue_2()
    {
        target_intro.SetActive(false);
        star_intro.SetActive(true);
    }
    public void Continue_3()
    {
        star_intro.SetActive(false);
        bird_intro.SetActive(true);
    }
    public void Continue_4()
    {
        bird_intro.SetActive(false);
        bird_intro2.SetActive(true);
    }
}
