using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public LayerMask groundLayer;
    public LayerMask enemyLayer;
    public float radius = 2f;

    public GameObject model;
    public ParticleSystem explosionVFX;

    Vector3 velocity;
    Vector3 gravity = new Vector3(0, - 20f, 0);
    float damping = 0.8f;   //감쇠 계수

    float defaultSpeed = 5f;
    float arrivalTime;

    int damage;

    int bouncCount = 0;


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
        canMove = false;
        model.SetActive(false);
        explosionVFX.Play();
        Destroy(gameObject,1.5f);
    }

    private void TakeDamage(List<MinionStatus> minions)
    {
        for(int i = minions.Count - 1; i >= 0; i--)
        {
            minions[i].TakeDamage(damage);
        }

        Explosion();
    }

    private bool FindTarget()
    {
        bool isEnemy = false;

        Collider[] targets = Physics.OverlapSphere(transform.position, radius, enemyLayer);

        if (targets.Length > 0)
        {
            List<MinionStatus> minions = new List<MinionStatus>();

            foreach (var target in targets)
            {
                MinionStatus minion = target.GetComponent<MinionStatus>();
                if (minion != null)
                {
                    minions.Add(minion);
                }
            }

            if(minions.Count > 0)
            {
                isEnemy = true;                                         //적이 하나라도 있는 경우 발견 & 데미지
                TakeDamage(minions);
            }
        }
        return isEnemy;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (FindTarget())           //바닥에 튀길 때 적이 있으면 일단 폭발
        {
            return;
        }
            
        bouncCount++;               //충돌 횟수 증가
        if(bouncCount >= 3)         //3번 이상 튕겼을 경우 폭발
        {
            Explosion();
            return;
        }


        Vector3 normal = collision.contacts[0].normal.normalized;

        float dot = Vector3.Dot(velocity, normal);
        Vector3 reflect = velocity - 2f * dot * normal;

        velocity = reflect * damping;

        Debug.Log("충돌 후 속도 : "+velocity);

    }
}
