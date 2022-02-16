using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowBird : Bird      //继承一般红鸟的脚本
{
    public override void ShowSkill()    //重写技能板块
    {
        base.ShowSkill();       //继承基类
        rg.velocity *= 2;       //改变子类，使其速度变为原来的两倍
    }
}
