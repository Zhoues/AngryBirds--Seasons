using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadLevel : MonoBehaviour
{
    private void Awake()
    {
        /*
         * 使用本地持久化类PlayerPrefs完成Unity整个游戏的数据存储
         */

        //读取当前关卡对应的场景（已经制作好放在Resources中）,并生成（放置在主相机下）
        Instantiate( Resources.Load(PlayerPrefs.GetString("nowLevel")) );      

    }
}
