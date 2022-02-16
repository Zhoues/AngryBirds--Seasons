using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Win : MonoBehaviour
{
    public void Show()  //胜利时出现的动画
    {
        GameManager._instance.ShowStars();  //出现星星
    }
}
