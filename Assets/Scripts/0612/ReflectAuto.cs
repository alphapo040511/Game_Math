using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectAuto : MonoBehaviour
{
    public Vector3 velocity = new Vector3(2f, -3f, 0);


    // Update is called once per frame
    void Update()
    {
        transform.position += velocity * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Vector3 normal = collision.contacts[0].normal.normalized;       //충돌 시점의 법선 벡터 (정규화)

        //Vector3 reflect = Vector3.Reflect(veloticy, normal);            //반사 벡터 계산

        float dot = Vector3.Dot(velocity, normal);
        Vector3 reflect = velocity - 2f * dot * normal; //반사 벡터 수식 : R - V -2(V ⋅ R)N


        velocity = reflect;
    }
}
