using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleRotation : MonoBehaviour
{
    public float rotationSpeed = 90f;

    // Update is called once per frame
    void Update()
    {
        float input = Input.GetAxis("Horizontal");
        transform.Rotate(0, input * rotationSpeed * Time.deltaTime, 0);
    }
}
