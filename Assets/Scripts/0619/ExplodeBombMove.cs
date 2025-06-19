using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeBombMove : MonoBehaviour
{
    public float radius = 2f;

    Vector3 velocity;
    Vector3 gravity = new Vector3(0, -20f, 0);

    float defaultSpeed = 5f;
    float arrivalTime;

    int damage;


    private bool canMove = false;

    public void Init(Vector3 hitPoint, int damageValue)
    {
        hitPoint.y = 0;
        Vector3 position = transform.position;

        position.y = 0;

        float distance = Vector3.Distance(position, hitPoint);

        arrivalTime = distance / defaultSpeed;

        velocity = GetStartVelocity(transform.position, hitPoint, arrivalTime);

        damage = damageValue;

        canMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            velocity += gravity * Time.deltaTime;
            transform.position += velocity * Time.deltaTime;
        }
    }

    Vector3 GetStartVelocity(Vector3 start, Vector3 end, float time)                    //시작 속력 구하기
    {
        Vector3 velocity = new Vector3();
        velocity.x = (end.x - start.x) / time;
        velocity.z = (end.z - start.z) / time;
        velocity.y = (end.y - start.y - 0.5f * gravity.y * time * time) / time;
        return velocity;
    }

    private void Explosion()
    {
        ManualExplode IExplode = GetComponent<ManualExplode>();
        if (IExplode != null)
        {
            IExplode.DelayStart(damage);
        }

        canMove = false;
    }


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.CompareTag("Plane"))
        Explosion();
    }
}
