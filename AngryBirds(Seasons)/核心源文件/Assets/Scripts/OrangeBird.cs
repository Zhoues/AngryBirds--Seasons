using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrangeBird : Bird      //�̳�һ�����Ľű�
{
    private CircleCollider2D circle;
    public Sprite clickOrange;
    private SpriteRenderer render_orange;
    public override void ShowSkill()    //��д���ܰ��
    {
        base.ShowSkill();       //�̳л���
        circle = GetComponent<CircleCollider2D>();
        render_orange = GetComponent<SpriteRenderer>();
        circle.radius += 1;
        render_orange.sprite = clickOrange;
    }
}