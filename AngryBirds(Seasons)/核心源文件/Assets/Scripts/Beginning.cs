using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;          //���ڲ�ͬ��������ת

public class Beginning : MonoBehaviour
{
    public GameObject playButton;           //��ʼ������play��ť
    public GameObject Myname;                 //��ʼ�����ҵ����֣�BUAA.Zhou��
    public GameObject updateInformation;    //������Ϣ�İ�ť
    public GameObject text1;                 //���ܵ���Ϣ����
    public GameObject text2;                 //���ܵ���Ϣ����
    public GameObject returnPlay;           //��������ʼ����
    public GameObject nextPageButton;       //��һҳ
    public GameObject backPageButton;       //��һҳ

    public void Introducted()               //����ע������¼�����������жϸùؿ��Ƿ񿪷�
    {
            SceneManager.LoadScene(3);      //File��Build Setting�ж���03-introduction������ģ�����Ϊ3
    }

    public void UpdateInformation()         //���ܸ�����Ϣ
    {
        playButton.SetActive(false);        //��������ͼƬ��ʧ�����ֽ���ͼƬ
        Myname.SetActive(false);
        updateInformation.SetActive(false);

        text1.SetActive(true);               //����������Ϣ�ͻ��˰�ť�Լ���һҳ��ť
        returnPlay.SetActive(true);
        nextPageButton.SetActive(true);     
    }

    public void BackToPlay()                
    {
        playButton.SetActive(true);        //��������ͼƬ���֣�����ͼƬ��ʧ
        Myname.SetActive(true);
        updateInformation.SetActive(true);

        text1.SetActive(false);               
        text2.SetActive(false);
        returnPlay.SetActive(false);
        nextPageButton.SetActive(false);
        backPageButton.SetActive(false);
    }

    public void NextPage()
    {
        text1.SetActive(false);
        text2.SetActive(true);
        nextPageButton.SetActive(false);
        backPageButton.SetActive(true);
    }

    public void BackPage()
    {
        text1.SetActive(true);
        text2.SetActive(false);
        nextPageButton.SetActive(true);
        backPageButton.SetActive(false);
    }
}
