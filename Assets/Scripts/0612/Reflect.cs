using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reflect : MonoBehaviour
{
    public Vector3 velocity = new Vector3(2f, 3f, 0);
    public Vector3 gravity = new Vector3(0, -9.81f, 0);
    float damping = 0.9f;   //감쇠 계수

    // Update is called once per frame
    void Update()
    {
        velocity += gravity * Time.deltaTime;
        transform.position += velocity * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Vector3 normal = collision.contacts[0].normal.normalized;       //충돌 시점의 법선 벡터 (정규화)

        float dot = Vector3.Dot(velocity, normal);
        Vector3 reflect = velocity - 2f * dot * normal; //반사 벡터 수식 : R - V -2(V ⋅ R)N


        velocity = reflect * damping;
    }
}

