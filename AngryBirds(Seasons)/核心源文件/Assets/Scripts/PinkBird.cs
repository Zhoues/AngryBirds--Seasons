public class PinkBird : Bird      //�̳�һ�����Ľű�
{
    public override void ShowSkill()    //��д���ܰ��
    {
        base.ShowSkill();       //�̳л���
        rg.velocity *= 4;       //�ı����࣬ʹ���ٶȱ�Ϊԭ��������
    }
}
