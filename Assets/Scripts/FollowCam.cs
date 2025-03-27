using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    public Transform target;

    public Vector3 offset = new Vector3(0, 2, -4);

    public float smoothSpeed = 5f;

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 desiredPosition = target.position + Quaternion.Euler(0, target.eulerAngles.y, 0) * offset;

        transform.position = desiredPosition;

        transform.LookAt(target);
    }
}
