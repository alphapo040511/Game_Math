using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyDotFov
    : MonoBehaviour
{
    public Transform player;
    public float viewAngle = 60f;       //시야각

    // Update is called once per frame
    void Update()
    {
        Vector3 toPlayer = (player.position - transform.position).normalized;
        Vector3 forward = transform.forward;

        float dot = DotProduction(forward, toPlayer);
        float angle = Mathf.Acos(dot) * Mathf.Rad2Deg;      //내적을 각도로 변환

        if(angle < viewAngle / 2)
        {
            Debug.Log("플레이어가 시야 안에 있음!");
        }
    }

    float DotProduction(Vector3 detector, Vector3 player)
    {
        return detector.x * player.x + detector.y * player.y + detector.z * player.z;
    }
}
