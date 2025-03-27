using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuaternionRotation : MonoBehaviour
{
    public float rotationSpeed = 100f;

    // Update is called once per frame
    void Update()
    {
        float input = Input.GetAxis("Horizontal");

        if(Mathf.Abs(input) > 0.01f)
        {
            Quaternion deltaRotation = Quaternion.Euler(0, input * rotationSpeed * Time.deltaTime, 0);
            transform.rotation = deltaRotation * transform.rotation;
        }
    }
}
