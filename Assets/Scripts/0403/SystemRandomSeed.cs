using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemRandomSeed : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        System.Random rnd = new System.Random(1234);    //�׻� ���� ������ ��µ�
        for(int i = 0; i < 5; i++)
        {
            Debug.Log(rnd.Next(1, 7));  //1~6 ������ ����


        }
    }
}
