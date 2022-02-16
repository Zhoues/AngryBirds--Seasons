using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;          //用于不同场景的跳转

public class Introduction : MonoBehaviour
{
    public GameObject introdution;          //新手教程封面
    public GameObject start;                //开始新手教程
    public GameObject sling_intro;          //介绍弹弓使用
    public GameObject target_intro;         //介绍游戏目标
    public GameObject star_intro;           //介绍游戏目标
    public GameObject bird_intro;           //介绍游戏目标
    public GameObject bird_intro2;           //介绍游戏目标

    public GameObject continue_1;           //介绍完弹弓，准备介绍游戏目标
    public GameObject continue_2;           //介绍完游戏目标，准备介绍关卡
    public GameObject continue_3;           //介绍完关卡，准备介绍小鸟
    public GameObject continue_4;           //继续介绍小鸟
    //public GameObject skip;               //跳过新手教程

    public void Skip()                  //用于注册鼠标事件，鼠标点击后判断该关卡是否开放
    {
            SceneManager.LoadScene(4);              //File中Build Setting中对于02-Game场景的模块序号为2
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
