using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class Jailer : MonoBehaviour
{
    public Transform player;

    public float viewAngle;
    public float viewDistance;
    public float rotateSpeed;

    float distance
    {
        get
        {
            return (player.position - transform.position).magnitude;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Rotate();
        FindPlayer();
    }

    void Rotate()
    {
        Quaternion deltaRotation = Quaternion.Euler(0, rotateSpeed * Time.deltaTime, 0);
        transform.rotation = deltaRotation * transform.rotation;
    }

    void FindPlayer()
    {
        Vector3 toPlayer = (player.position - transform.position).normalized;
        Vector3 forward = transform.forward;

        float dot = DotProduction(forward, toPlayer);
        float angle = Mathf.Acos(dot) * Mathf.Rad2Deg;

        if (angle < viewAngle / 2 && distance <= viewDistance)
        {

            player.GetComponent<PlayerMove>().Die();
        }
    }

    float DotProduction(Vector3 forward, Vector3 player)
    {
        return forward.x * player.x + forward.y * player.y + forward.z * player.z;
    }
}
