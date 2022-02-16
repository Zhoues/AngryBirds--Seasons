using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenBird : Bird      //继承一般红鸟的脚本
{
    public override void ShowSkill()    //重写技能板块
    {
        base.ShowSkill();   //继承基类
        Vector3 speed = rg.velocity;    //修改速度
        speed.x *= -0.5f;                  //x轴反向
        //y轴变为0（按照原游戏，应该是加速度朝上的匀减速运动，之后再做加速度向上的加速运动，画出一个类圆弧的形状）
        speed.y = 0;
        rg.velocity = speed;
    }
}
