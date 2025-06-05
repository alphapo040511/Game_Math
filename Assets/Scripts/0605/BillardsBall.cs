
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillardsBall : MonoBehaviour
{
    public BilliardsPlayer managers;

    public int playerIndex;

    private GameObject breakedBall;

    private bool hitOtherPlayer = false;
    private bool addedPoint = false;

    private Rigidbody rb;

    [SerializeField] float speed = 0;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        speed = rb.velocity.magnitude;
    }

    public void HitBall(Vector3 dir, float force)
    { 
        rb.AddForce(dir * force, ForceMode.Impulse);
        hitOtherPlayer = false;
        breakedBall = null;
        addedPoint = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (managers.currentPlayer != playerIndex) return;

        if(hitOtherPlayer)
        {
            if(addedPoint)
            {
                BillardsScoreManager.Instance.CanclePoint(playerIndex);
            }
            return;
        }

        if(collision.transform.CompareTag("Player"))
        {
            hitOtherPlayer = true;
        }

        if(collision.transform.CompareTag("BillardsBall"))
        {
            if(breakedBall == null)
            {
                breakedBall = collision.gameObject;
            }
            else if(!addedPoint)
            {
                BillardsScoreManager.Instance.GetPoint(playerIndex);
                addedPoint = true;
            }
        }
    }
}
