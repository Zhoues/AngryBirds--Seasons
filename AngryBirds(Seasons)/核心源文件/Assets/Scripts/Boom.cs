using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boom : MonoBehaviour
{
    public void destroy()
    {
        Destroy(gameObject);    //猪死亡爆炸效果出现后，猪消失
    }
}
