using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityRandomSeed : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Random.InitState(1234);    //Unity 난수 시드 고정

        for (int i = 0; i < 5; i++)
        {
            Debug.Log(Random.Range(1, 7));  //1~6 사이의 정수
        }
    }
}
