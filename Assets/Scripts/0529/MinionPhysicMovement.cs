using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionPhysicMovement : IMovedObject
{
    public float speed = 5f;

    Vector3 direct;

    private bool wait = false;

    private Animator animator;

    private Rigidbody _rigidbody;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(NewDirect());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float velY = _rigidbody.velocity.y;
        Vector3 vel = direct * speed;
        vel.y += velY;

        if (currentState != ObjectStats.Idel)
        {
            _rigidbody.velocity += Vector3.up * velY;
            return;
        }

        _rigidbody.velocity = vel;
        if (_rigidbody.velocity != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(_rigidbody.velocity.normalized);
            _rigidbody.MoveRotation(Quaternion.Slerp(_rigidbody.rotation, targetRotation, 
                0.1f)); // 부드럽게 회전
        }
    }

    IEnumerator NewDirect()
    {
        while (true)
        {
            direct = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized;
            wait = false;

            yield return new WaitForSeconds(Random.Range(3f, 7f));

            direct = Vector3.zero;
            wait = true;

            yield return new WaitForSeconds(1f);
        }
    }
}
