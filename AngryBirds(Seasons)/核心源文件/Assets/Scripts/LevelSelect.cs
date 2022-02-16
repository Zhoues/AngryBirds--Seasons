using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;          //用于不同场景的跳转

public class LevelSelect : MonoBehaviour
{
    //该算法原理：采用前驱法，如果某一个关卡它的前面一关是开启的，且星星数量大于0，说明该关卡可以被选择
    //考虑边界情况，如果这个关卡没有前驱，且自己没有星星数量，说明这个是第一关，那么直接开启即可

    public bool isSelect = false;           //判断关卡是否可选择
    public Sprite levelBG;                  //可选择后的关卡图片
    private Image image;                    //当前关卡图片信息

    public GameObject[] stars;              //该数组中存储每一关通过后获得的星星
    private void Awake()
    {
        image = GetComponent<Image>();      //获取当前关卡图片信息
    }

    private void Start()
    {
        if (transform.parent.GetChild(0).name == gameObject.name)    //如果是第一关
        {
            isSelect = true;                //该关卡可以被选择
        }
        else
        {
            //前驱算法开启下一关
            int beforeNum = int.Parse(gameObject.name) - 1; //获得前一关的关卡名
            if (PlayerPrefs.GetInt("level" + beforeNum.ToString()) > 0) //使用前驱算法，判断是否满足开启条件
            {
                isSelect = true;        //可选择
            }
        }

        if (isSelect)
        {
            image.overrideSprite = levelBG; //替换为可选择后的关卡图片
            transform.Find("num").gameObject.SetActive(true);   //激活num对应的关卡图片

            //通过字符串拼接获得关卡名字，然后获得该关卡对应的星星数量
            int count = PlayerPrefs.GetInt("level" + gameObject.name);
            //
            if (count > 0)                   //如果该关卡星星数大于零
            {
                for (int i = 0; i < count; i++)  //遍历，同时让相等数量的星星显示在选择关卡界面
                {
                    stars[i].SetActive(true);   //显示星星
                }
            }
        }
    }



    public void Selected()                  //用于注册鼠标事件，鼠标点击后判断该关卡是否开放
    {
        if (isSelect)                        //如果已经开启
        {
            PlayerPrefs.SetString("nowLevel", "level" + gameObject.name);   //通过 nowLevel , level + 该关卡的序号构成一个独一无二的键值对，方便存储和访问
            SceneManager.LoadScene(2);              //File中Build Setting中对于02-Game场景的模块序号为2
        }
    }

}
