using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  //�µ������ռ䣬ʵ��Unity�е����¼���

public class GameManager : MonoBehaviour
{
    public List<Bird> birds;    //���б����ʽ��ʾС��ļ���
    public List<Pig> pig;       //���б����ʽ��ʾ��ļ���
    public static GameManager _instance;    //��ǰ����Ϸ�������

    private Vector3 originPos;    //��ʼС���λ��

    public GameObject win;       //��ȡ������Ӯ״��
    public GameObject lose;

    public GameObject[] stars;  //������л�ȡ���Ǽ���

    private int starNum = 0;    //��¼ÿһ�صĵõ�������������֮�����ݵĴ洢��

    private int totalNum = 15;  //�ó��������еĹؿ���
    private void Awake()        //��ʼ��
    {
        _instance = this;           //��ȡ��Ϸ�������
        if (birds.Count > 0)        //�����С��
        {
            originPos = birds[0].transform.position;    //��ʼλ��Ϊ��һֻС���λ��
        }

    }
    private void Start()
    {
        Initialized();          //��ʼ��
    }
    private void Initialized()  //��ʼ��
    {
        for (int i = 0; i < birds.Count; i++) //����С�񼯺�
        {
            if (i == 0)  //����ǵ�һֻС��
            {
                birds[i].transform.position = originPos;    //��С������ڳ�ʼλ�ã�ʹ������
                birds[i].enabled = true;    //��С�����
                birds[i].sp.enabled = true; //��С�������Ч��������sp����Bird�ű���ĵ�����ڶ�������Ч����
                birds[i].canMove = true;    //С����Ա������
            }
            else
            {
                birds[i].enabled = false;    //��С�񲻿���
                birds[i].sp.enabled = false; //��С�������Ч���ر�
            }
        }
    }
    public void NextBird() //�ж���Ϸ�߼�������Ѿ���ʤ�ˣ��Ͳ�����Ҫ����Initialized()�ĳ�ʼ�������ˣ���ʱʣ���С���ڵ����ϣ�
    {
        if (pig.Count > 0)
        {
            if (birds.Count > 0) //׼����ʼ����һֻС��
            {
                Initialized();  //��ʼ��
            }
            else//����
            {
                lose.SetActive(true);   //�������˵Ļ���
            }
        }
        else//���Ѿ�ȫ�����𣬻��ʤ��
        {
            win.SetActive(true);        //����Ӯ�˵Ļ���
        }
    }

    public void ShowStars()     //ʤ��ʱ��������
    {
        StartCoroutine("show");    //����Э��
    }

    IEnumerator show()
    {
        for (; starNum < birds.Count + 1; starNum++)
        {
            if (starNum >= stars.Length)
                break;                     //��������������Ǿ�ֱ������ѭ��
            else
            {
                yield return new WaitForSeconds(0.2f);  //�ȴ�0.2s
                stars[starNum].SetActive(true);               //����������Ч
            }
        }
    }
    public void StartTOPlay()
    {
        SceneManager.LoadScene(1);  //File��Build Setting�ж���01-Level������ģ�����Ϊ1
    }
    public void Replay()    //����������水ť
    {
        SavaData();                 //��������
        Time.timeScale = 1;
        SceneManager.LoadScene(2);  //File��Build Setting�ж���02-Game������ģ�����Ϊ2
    }

    public void Home()
    {
        SavaData();                 //��������
        Time.timeScale = 1;
        SceneManager.LoadScene(1);  //File��Build Setting�ж���01-Level������ģ�����Ϊ1
    }

    public void NextLevel()    //���������һ�صİ�ť
    {
        SavaData();
        Time.timeScale = 1;
        string levelNum = PlayerPrefs.GetString("nowLevel");
        //ȥ���ַ������level���ַ������õ���ǰ�ǵڼ���
        levelNum = levelNum.Replace("level", "");
        //�ؿ�����һ ���ﻹҪ�ж�һ�µ�ǰi�Ƿ���ڵ�ǰ��ͼ������Ĺؿ���
        int i = int.Parse(levelNum) + 1;
        levelNum = "level" + i.ToString();
        PlayerPrefs.SetString("nowLevel", levelNum);
        SceneManager.LoadScene(2);
    }

    public void SavaData()
    {
        //PlayerPrefs.SetInt("string",int num)              ͨ����ֵ�Դ洢�ùؿ�����������
        /*
         * ʹ�ñ��س־û���PlayerPrefs���Unity������Ϸ�����ݴ洢
         * ���м��ɵ����ݴ洢
         * ������ⲿ���ݿ��ʹ��
         */
        if (starNum > PlayerPrefs.GetInt(PlayerPrefs.GetString("nowLevel")))    //�����õ����Ǵ�����ʷ��¼
        {
            PlayerPrefs.SetInt(PlayerPrefs.GetString("nowLevel"), starNum);      //��ȡnowLevel��Ӧ��level + �ùؿ�����ţ����¸ùؿ��Ļ��������
        }
        //�洢���е����Ǹ���
        int sum = 0;
        for (int i = 1; i <= totalNum; i++)
        {
            sum += PlayerPrefs.GetInt("level" + i.ToString());               //�ۼӸó���Ŀǰ������������������ʾtext�еķ��ӣ�
        }


        PlayerPrefs.SetInt("totalNum", sum);            //ͨ����ֵ�Դ洢�ó���totalNum���ؿ�����õ�����������
        //print(PlayerPrefs.GetInt("totalNum"));
    }
}