using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;               //���ڴ���Text�ļ�

public class MapSelect : MonoBehaviour
{
    public int starNum = 0;         //�����³�����Ҫ����������
    private bool isSelect = false;   //�ж��Ƿ���Կ����³���
    public bool isThird = false;     //�ж��Ƿ��ǵ�������������Ϊ��������������������ͬ

    public GameObject locks;        //��������
    public GameObject stars;        //�ó�����õ���������

    public GameObject panel;        //�����µĹؿ��������LevelSelect�Ľ�����
    public GameObject map;          //������Ϸ�����г���

    public Text starsText;          //����ѡ�񳡾���������Ǳ�ֵ
    public int startNum = 1;       //��ʼ�ؿ���
    public int endNum = 5;          //��ֹ�ؿ���
    private void Start()
    {
        /*
         * ʹ�ñ��س־û���PlayerPrefs���Unity������Ϸ�����ݴ洢
         */
       // PlayerPrefs.DeleteAll();

        //PlayerPrefs.GetInt("string",int num)              ͨ����ֵ�Դ洢���г����е���������
        if(PlayerPrefs.GetInt("totalNum", 0) >= starNum)  //��������㹻�����ó���
        {
            isSelect = true;        //�ó�������
        }

        if(isSelect)
        {
            locks.SetActive(false); //��������
            stars.SetActive(true); //��ʾ�ó�����õ���������

            /*
             * 
             * ��������������
             * Ҫ�޸�text��ʾ��ֵ
             *
             */
            int counts = 0;          //��¼��ǰ���������йؿ������������
            for(int i = startNum; i <= endNum; i++)
            {
                counts += PlayerPrefs.GetInt("level" + i.ToString(),0);   //�ۼ�(���û�У�Ĭ��Ϊ0)
            }
            if(isThird)
                starsText.text = counts.ToString() + "/9";     //�õ���ֵ
            else
                starsText.text = counts.ToString() + "/15" ;     //�õ���ֵ
        }
    }


    public void Selected()          //����ע������¼�����������жϸó����Ƿ񿪷�
    {
        if(isSelect)                //������������δ����
        {
            panel.SetActive(true);  //���Կ����ó����ľ���ؿ�
            map.SetActive(false);    //����ѡ��ؿ��Ĳ����ѡ�񳡾���ͼƬҪȫ������
        }
    }

    public void panelSelect()    //�ӹؿ�ѡ��
    {
        panel.SetActive(false);  
        map.SetActive(true);    

    }
}
