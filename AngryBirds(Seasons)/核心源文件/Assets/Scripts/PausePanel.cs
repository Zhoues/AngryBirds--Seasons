using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  //新的命名空间，实现Unity中的重新加载

public class PausePanel : MonoBehaviour
{
    private Animator anim; //动画状态机
    public GameObject button;   //屏幕上暂停键按钮
    private void Awake()
    {
        anim = GetComponent<Animator>();   //获取当前动画状态
    }
    public void Retry()
    {
        Time.timeScale = 1;         //游戏动画开始
        SceneManager.LoadScene(2);  //File中Build Setting中对于02-Game场景的模块序号为2
    }
    public void Home()
    {
        Time.timeScale = 1;          //游戏动画开始
        SceneManager.LoadScene(1);  //File中Build Setting中对于01-Level场景的模块序号为1
    }




    public void Pause() //点击pause按钮后的全部过程
    {
        //1.播放动画
        //2.暂停

        anim.SetBool("isPause", true);  //播放暂停动画
        button.SetActive(false);        //暂停键消失

        if(GameManager._instance.birds.Count > 0)       //场景中还有小鸟
        {
            if(GameManager._instance.birds[0].isReleased == false)  //如果未飞出（与弹弓还有联系）
            {
                GameManager._instance.birds[0].canMove = false;     //暂停界面的待飞小鸟不可以被操控
            }
        }
    }

    public void Resume()       //点击了继续的按钮
    {
        //1.播放动画
        //2.暂停
        Time.timeScale = 1;              //游戏动画暂停
        anim.SetBool("isPause", false);  //播放继续动画

        if (GameManager._instance.birds.Count > 0)       //场景中还有小鸟
        {
            if (GameManager._instance.birds[0].isReleased == false)  //如果未飞出（与弹弓还有联系）
            {
                GameManager._instance.birds[0].canMove = true;     //点击继续后待飞小鸟可以被操控
            }
        }
    }
    public void PauseAnimEnd()  //pause动画播放完调用
    {
        Time.timeScale = 0;     //游戏动画暂停
    }

    public void ResumeAnimEnd() //Resume动画播放完调用
    {
        button.SetActive(true);        //暂停键出现
    }
}
