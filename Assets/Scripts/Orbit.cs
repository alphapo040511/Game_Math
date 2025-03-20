using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbit : MonoBehaviour
{
    public Transform center;
    public float orbitCycle;

    private float dayAngle;
    private float distance;
    private float currentAngle;

    void Start()
    {
        distance = Vector3.Distance(center.position, transform.position);
        dayAngle = 360 / orbitCycle;
    }

    // Update is called once per frame
    void Update()
    {
        currentAngle = Mathf.Repeat(currentAngle + dayAngle * Time.deltaTime, 360);             //1초당 하루로 계산


        float radian = currentAngle * Mathf.Deg2Rad;
        float xPosition = Mathf.Cos(radian) * distance;
        float zPosition = Mathf.Sin(radian) * distance;

        transform.position = center.position + center.forward * zPosition + center.right * xPosition;
    }
}
