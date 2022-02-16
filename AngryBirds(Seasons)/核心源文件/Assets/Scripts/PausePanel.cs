using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  //�µ������ռ䣬ʵ��Unity�е����¼���

public class PausePanel : MonoBehaviour
{
    private Animator anim; //����״̬��
    public GameObject button;   //��Ļ����ͣ����ť
    private void Awake()
    {
        anim = GetComponent<Animator>();   //��ȡ��ǰ����״̬
    }
    public void Retry()
    {
        Time.timeScale = 1;         //��Ϸ������ʼ
        SceneManager.LoadScene(2);  //File��Build Setting�ж���02-Game������ģ�����Ϊ2
    }
    public void Home()
    {
        Time.timeScale = 1;          //��Ϸ������ʼ
        SceneManager.LoadScene(1);  //File��Build Setting�ж���01-Level������ģ�����Ϊ1
    }




    public void Pause() //���pause��ť���ȫ������
    {
        //1.���Ŷ���
        //2.��ͣ

        anim.SetBool("isPause", true);  //������ͣ����
        button.SetActive(false);        //��ͣ����ʧ

        if(GameManager._instance.birds.Count > 0)       //�����л���С��
        {
            if(GameManager._instance.birds[0].isReleased == false)  //���δ�ɳ����뵯��������ϵ��
            {
                GameManager._instance.birds[0].canMove = false;     //��ͣ����Ĵ���С�񲻿��Ա��ٿ�
            }
        }
    }

    public void Resume()       //����˼����İ�ť
    {
        //1.���Ŷ���
        //2.��ͣ
        Time.timeScale = 1;              //��Ϸ������ͣ
        anim.SetBool("isPause", false);  //���ż�������

        if (GameManager._instance.birds.Count > 0)       //�����л���С��
        {
            if (GameManager._instance.birds[0].isReleased == false)  //���δ�ɳ����뵯��������ϵ��
            {
                GameManager._instance.birds[0].canMove = true;     //������������С����Ա��ٿ�
            }
        }
    }
    public void PauseAnimEnd()  //pause�������������
    {
        Time.timeScale = 0;     //��Ϸ������ͣ
    }

    public void ResumeAnimEnd() //Resume�������������
    {
        button.SetActive(true);        //��ͣ������
    }
}
