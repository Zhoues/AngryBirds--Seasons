using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenBird : Bird      //�̳�һ�����Ľű�
{
    public override void ShowSkill()    //��д���ܰ��
    {
        base.ShowSkill();   //�̳л���
        Vector3 speed = rg.velocity;    //�޸��ٶ�
        speed.x *= -0.5f;                  //x�ᷴ��
        //y���Ϊ0������ԭ��Ϸ��Ӧ���Ǽ��ٶȳ��ϵ��ȼ����˶���֮���������ٶ����ϵļ����˶�������һ����Բ������״��
        speed.y = 0;
        rg.velocity = speed;
    }
}
