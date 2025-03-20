using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PovDetector : MonoBehaviour
{
    public Transform player;
    public float viewAngle = 60f;
    public float viewDistance = 5f;

    private Vector3 enemyForward;

    // Start is called before the first frame update
    void Start()
    {
        enemyForward = transform.forward;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetDirection = (player.position - transform.position).normalized;

        float dot = Vector3.Dot(targetDirection, enemyForward);

        float angle = Mathf.Acos(dot) * Mathf.Rad2Deg;

        if(angle < viewAngle / 2 && GetDistance(player.position) < viewDistance)
        {
            transform.localScale += Vector3.one * Time.deltaTime;
        }
    }

    float GetDistance(Vector3 playerPos)
    {
        Vector3 temp = playerPos - transform.position;
        return Mathf.Sqrt(temp.x * temp.x + temp.y * temp.y + temp.z * temp.z);
    }
}
