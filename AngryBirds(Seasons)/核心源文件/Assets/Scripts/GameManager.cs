using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  //新的命名空间，实现Unity中的重新加载

public class GameManager : MonoBehaviour
{
    public List<Bird> birds;    //用列表的形式表示小鸟的集合
    public List<Pig> pig;       //用列表的形式表示猪的集合
    public static GameManager _instance;    //当前的游戏管理对象

    private Vector3 originPos;    //初始小鸟的位置

    public GameObject win;       //获取面板的输赢状况
    public GameObject lose;

    public GameObject[] stars;  //从面板中获取星星集合

    private int starNum = 0;    //记录每一关的得到星星数（方便之后数据的存储）

    private int totalNum = 15;  //该场景下所有的关卡数
    private void Awake()        //初始化
    {
        _instance = this;           //读取游戏管理对象
        if (birds.Count > 0)        //如果有小鸟
        {
            originPos = birds[0].transform.position;    //初始位置为第一只小鸟的位置
        }

    }
    private void Start()
    {
        Initialized();          //初始化
    }
    private void Initialized()  //初始化
    {
        for (int i = 0; i < birds.Count; i++) //遍历小鸟集合
        {
            if (i == 0)  //如果是第一只小鸟
            {
                birds[i].transform.position = originPos;    //把小鸟放置在初始位置，使其更柔和
                birds[i].enabled = true;    //该小鸟可用
                birds[i].sp.enabled = true; //该小鸟的物理效果开启（sp是在Bird脚本里的弹动与摆动的物理效果）
                birds[i].canMove = true;    //小鸟可以被鼠标点击
            }
            else
            {
                birds[i].enabled = false;    //该小鸟不可用
                birds[i].sp.enabled = false; //该小鸟的物理效果关闭
            }
        }
    }
    public void NextBird() //判断游戏逻辑（如果已经获胜了，就不再需要进行Initialized()的初始化操作了，此时剩余的小鸟都在地面上）
    {
        if (pig.Count > 0)
        {
            if (birds.Count > 0) //准备初始化下一只小鸟
            {
                Initialized();  //初始化
            }
            else//输了
            {
                lose.SetActive(true);   //启动输了的画面
            }
        }
        else//猪已经全部消灭，获得胜利
        {
            win.SetActive(true);        //启动赢了的画面
        }
    }

    public void ShowStars()     //胜利时出现星星
    {
        StartCoroutine("show");    //开启协程
    }

    IEnumerator show()
    {
        for (; starNum < birds.Count + 1; starNum++)
        {
            if (starNum >= stars.Length)
                break;                     //如果超过三个星星就直接跳出循环
            else
            {
                yield return new WaitForSeconds(0.2f);  //等待0.2s
                stars[starNum].SetActive(true);               //开启星星特效
            }
        }
    }
    public void StartTOPlay()
    {
        SceneManager.LoadScene(1);  //File中Build Setting中对于01-Level场景的模块序号为1
    }
    public void Replay()    //如果按下重玩按钮
    {
        SavaData();                 //保存数据
        Time.timeScale = 1;
        SceneManager.LoadScene(2);  //File中Build Setting中对于02-Game场景的模块序号为2
    }

    public void Home()
    {
        SavaData();                 //保存数据
        Time.timeScale = 1;
        SceneManager.LoadScene(1);  //File中Build Setting中对于01-Level场景的模块序号为1
    }

    public void NextLevel()    //如果按下下一关的按钮
    {
        SavaData();
        Time.timeScale = 1;
        string levelNum = PlayerPrefs.GetString("nowLevel");
        //去掉字符串里带level的字符，即得到当前是第几关
        levelNum = levelNum.Replace("level", "");
        //关卡数加一 这里还要判断一下当前i是否大于当前地图里边最大的关卡数
        int i = int.Parse(levelNum) + 1;
        levelNum = "level" + i.ToString();
        PlayerPrefs.SetString("nowLevel", levelNum);
        SceneManager.LoadScene(2);
    }

    public void SavaData()
    {
        //PlayerPrefs.SetInt("string",int num)              通过键值对存储该关卡的星星数量
        /*
         * 使用本地持久化类PlayerPrefs完成Unity整个游戏的数据存储
         * 很有技巧的数据存储
         * 避免的外部数据库的使用
         */
        if (starNum > PlayerPrefs.GetInt(PlayerPrefs.GetString("nowLevel")))    //如果获得的星星大于历史记录
        {
            PlayerPrefs.SetInt(PlayerPrefs.GetString("nowLevel"), starNum);      //获取nowLevel对应的level + 该关卡的序号，存下该关卡的获得星星数
        }
        //存储所有的星星个数
        int sum = 0;
        for (int i = 1; i <= totalNum; i++)
        {
            sum += PlayerPrefs.GetInt("level" + i.ToString());               //累加该场景目前所有星星数（由于显示text中的分子）
        }


        PlayerPrefs.SetInt("totalNum", sum);            //通过键值对存储该场景totalNum个关卡所获得的所有星星数
        //print(PlayerPrefs.GetInt("totalNum"));
    }
}