using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadLevel : MonoBehaviour
{
    private void Awake()
    {
        /*
         * ʹ�ñ��س־û���PlayerPrefs���Unity������Ϸ�����ݴ洢
         */

        //��ȡ��ǰ�ؿ���Ӧ�ĳ������Ѿ������÷���Resources�У�,�����ɣ�������������£�
        Instantiate( Resources.Load(PlayerPrefs.GetString("nowLevel")) );      

    }
}
