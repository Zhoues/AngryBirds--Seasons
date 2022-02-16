using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrangeBird : Bird      //继承一般红鸟的脚本
{
    private CircleCollider2D circle;
    public Sprite clickOrange;
    private SpriteRenderer render_orange;
    public override void ShowSkill()    //重写技能板块
    {
        base.ShowSkill();       //继承基类
        circle = GetComponent<CircleCollider2D>();
        render_orange = GetComponent<SpriteRenderer>();
        circle.radius += 1;
        render_orange.sprite = clickOrange;
    }
}