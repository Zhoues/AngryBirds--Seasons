using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowBird : Bird      //�̳�һ�����Ľű�
{
    public override void ShowSkill()    //��д���ܰ��
    {
        base.ShowSkill();       //�̳л���
        rg.velocity *= 2;       //�ı����࣬ʹ���ٶȱ�Ϊԭ��������
    }
}
