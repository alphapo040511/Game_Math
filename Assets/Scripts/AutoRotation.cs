using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoRotation : MonoBehaviour
{

    // Y축을 기준으로 1초마다 45도 회전
    void Update()
    {
        transform.Rotate(0, 45 * Time.deltaTime, 0);
    }
}
