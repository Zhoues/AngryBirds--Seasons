public class PinkBird : Bird      //继承一般红鸟的脚本
{
    public override void ShowSkill()    //重写技能板块
    {
        base.ShowSkill();       //继承基类
        rg.velocity *= 4;       //改变子类，使其速度变为原来的两倍
    }
}
