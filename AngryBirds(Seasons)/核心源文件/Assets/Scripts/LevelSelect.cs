using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;          //���ڲ�ͬ��������ת

public class LevelSelect : MonoBehaviour
{
    //���㷨ԭ������ǰ���������ĳһ���ؿ�����ǰ��һ���ǿ����ģ���������������0��˵���ùؿ����Ա�ѡ��
    //���Ǳ߽�������������ؿ�û��ǰ�������Լ�û������������˵������ǵ�һ�أ���ôֱ�ӿ�������

    public bool isSelect = false;           //�жϹؿ��Ƿ��ѡ��
    public Sprite levelBG;                  //��ѡ���Ĺؿ�ͼƬ
    private Image image;                    //��ǰ�ؿ�ͼƬ��Ϣ

    public GameObject[] stars;              //�������д洢ÿһ��ͨ�����õ�����
    private void Awake()
    {
        image = GetComponent<Image>();      //��ȡ��ǰ�ؿ�ͼƬ��Ϣ
    }

    private void Start()
    {
        if (transform.parent.GetChild(0).name == gameObject.name)    //����ǵ�һ��
        {
            isSelect = true;                //�ùؿ����Ա�ѡ��
        }
        else
        {
            //ǰ���㷨������һ��
            int beforeNum = int.Parse(gameObject.name) - 1; //���ǰһ�صĹؿ���
            if (PlayerPrefs.GetInt("level" + beforeNum.ToString()) > 0) //ʹ��ǰ���㷨���ж��Ƿ����㿪������
            {
                isSelect = true;        //��ѡ��
            }
        }

        if (isSelect)
        {
            image.overrideSprite = levelBG; //�滻Ϊ��ѡ���Ĺؿ�ͼƬ
            transform.Find("num").gameObject.SetActive(true);   //����num��Ӧ�Ĺؿ�ͼƬ

            //ͨ���ַ���ƴ�ӻ�ùؿ����֣�Ȼ���øùؿ���Ӧ����������
            int count = PlayerPrefs.GetInt("level" + gameObject.name);
            //
            if (count > 0)                   //����ùؿ�������������
            {
                for (int i = 0; i < count; i++)  //������ͬʱ�����������������ʾ��ѡ��ؿ�����
                {
                    stars[i].SetActive(true);   //��ʾ����
                }
            }
        }
    }



    public void Selected()                  //����ע������¼�����������жϸùؿ��Ƿ񿪷�
    {
        if (isSelect)                        //����Ѿ�����
        {
            PlayerPrefs.SetString("nowLevel", "level" + gameObject.name);   //ͨ�� nowLevel , level + �ùؿ�����Ź���һ����һ�޶��ļ�ֵ�ԣ�����洢�ͷ���
            SceneManager.LoadScene(2);              //File��Build Setting�ж���02-Game������ģ�����Ϊ2
        }
    }

}
