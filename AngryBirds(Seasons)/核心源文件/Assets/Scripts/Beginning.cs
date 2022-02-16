using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;          //用于不同场景的跳转

public class Beginning : MonoBehaviour
{
    public GameObject playButton;           //开始场景的play按钮
    public GameObject Myname;                 //开始界面我的名字（BUAA.Zhou）
    public GameObject updateInformation;    //介绍信息的按钮
    public GameObject text1;                 //介绍的信息内容
    public GameObject text2;                 //介绍的信息内容
    public GameObject returnPlay;           //回退至开始场景
    public GameObject nextPageButton;       //下一页
    public GameObject backPageButton;       //上一页

    public void Introducted()               //用于注册鼠标事件，鼠标点击后判断该关卡是否开放
    {
            SceneManager.LoadScene(3);      //File中Build Setting中对于03-introduction场景的模块序号为3
    }

    public void UpdateInformation()         //介绍更新信息
    {
        playButton.SetActive(false);        //三个场景图片消失，出现介绍图片
        Myname.SetActive(false);
        updateInformation.SetActive(false);

        text1.SetActive(true);               //出来介绍信息和回退按钮以及下一页按钮
        returnPlay.SetActive(true);
        nextPageButton.SetActive(true);     
    }

    public void BackToPlay()                
    {
        playButton.SetActive(true);        //三个场景图片出现，介绍图片消失
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
