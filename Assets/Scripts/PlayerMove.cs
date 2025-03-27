using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float rotateSpeed = 45f;

    private Vector3 startPosition;
    private Quaternion startRotation;

    void Start()
    {
        startPosition = transform.position;
        startRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Rotate();
    }

    public void Die()
    {
        transform.position = startPosition;
        transform.rotation = Quaternion.identity;
    }

    void Movement()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 moveDirection = (transform.forward * moveZ + transform.right * moveX).normalized;
        transform.position += moveDirection * moveSpeed * Time.deltaTime;
    }

    void Rotate()
    {
        float input = Input.GetAxis("Rotate");

        if (Mathf.Abs(input) > 0.01f)
        {
            Quaternion deltaRotation = Quaternion.Euler(0, input * rotateSpeed * Time.deltaTime, 0);
            transform.rotation = deltaRotation * transform.rotation;
        }
    }
}
