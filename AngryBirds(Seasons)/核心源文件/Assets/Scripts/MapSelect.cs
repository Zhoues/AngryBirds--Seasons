using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;               //用于处理Text文件

public class MapSelect : MonoBehaviour
{
    public int starNum = 0;         //解锁新场景需要的星星数量
    private bool isSelect = false;   //判断是否可以开启新场景
    public bool isThird = false;     //判断是否是第三个场景，因为三个场景的星星总数不同

    public GameObject locks;        //场景封锁
    public GameObject stars;        //该场景获得的星星数量

    public GameObject panel;        //场景下的关卡（完成与LevelSelect的交互）
    public GameObject map;          //整个游戏的所有场景

    public Text starsText;          //处理选择场景表面的星星比值
    public int startNum = 1;       //起始关卡数
    public int endNum = 5;          //终止关卡数
    private void Start()
    {
        /*
         * 使用本地持久化类PlayerPrefs完成Unity整个游戏的数据存储
         */
       // PlayerPrefs.DeleteAll();

        //PlayerPrefs.GetInt("string",int num)              通过键值对存储所有场景中的星星数量
        if(PlayerPrefs.GetInt("totalNum", 0) >= starNum)  //如果星星足够解锁该场景
        {
            isSelect = true;        //该场景开放
        }

        if(isSelect)
        {
            locks.SetActive(false); //场景开启
            stars.SetActive(true); //显示该场景获得的星星数量

            /*
             * 
             * 这里遇到困难了
             * 要修改text显示的值
             *
             */
            int counts = 0;          //记录当前场景的所有关卡里的星星数量
            for(int i = startNum; i <= endNum; i++)
            {
                counts += PlayerPrefs.GetInt("level" + i.ToString(),0);   //累加(如果没有，默认为0)
            }
            if(isThird)
                starsText.text = counts.ToString() + "/9";     //得到比值
            else
                starsText.text = counts.ToString() + "/15" ;     //得到比值
        }
    }


    public void Selected()          //用于注册鼠标事件，鼠标点击后判断该场景是否开放
    {
        if(isSelect)                //如果这个场景还未开放
        {
            panel.SetActive(true);  //可以开启该场景的具体关卡
            map.SetActive(false);    //进入选择关卡的步骤后，选择场景的图片要全部隐藏
        }
    }

    public void panelSelect()    //从关卡选择到
    {
        panel.SetActive(false);  
        map.SetActive(true);    

    }
}
