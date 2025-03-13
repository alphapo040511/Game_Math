using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public float speed = 5f;
    // Update is called once per frame
    void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        if (moveX == 0 || moveY == 0) return;

        float magnitude = Mathf.Sqrt(moveX * moveX + moveY * moveY);

        Vector3 direction = new Vector3(moveX, moveY, 0) / magnitude;

        Vector3 move = direction * speed * Time.deltaTime;

        transform.position += move;
    }
}
