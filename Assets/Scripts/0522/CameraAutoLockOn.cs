using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAutoLockOn : MonoBehaviour
{
    public LayerMask enemyLayer;

    [SerializeField] float smoothTime = 0.3f;

    [SerializeField] float maxSpeed = 100f;

    Transform target;

    float speed = 2f;

    Vector3 offset = new Vector3(0, 2, -3);

    Vector3 velocity = Vector3.zero;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (target != null)
            {
                //target.GetComponent<MeshRenderer>().material.color = Color.white;
            }

            target = null;
        }

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit,100f, enemyLayer))
            {
                if(target != null)
                {
                    //target.GetComponent<MeshRenderer>().material.color = Color.white;
                }

                target = hit.transform;

                //target.GetComponent<MeshRenderer>().material.color = Color.red;

                ApplyOffset();
            }
        }

        if (!target) return;

        Quaternion lookRot = Quaternion.LookRotation(target.position - transform.position);

        float t = 1f - Mathf.Exp(-speed * Time.deltaTime);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRot, t);

        Vector3 desired = target.position + target.TransformDirection(offset);

        transform.position = Vector3.SmoothDamp(
            transform.position,
            desired,
            ref velocity,
            smoothTime,
            maxSpeed,
            Time.deltaTime
            );
    }

    private void ApplyOffset()
    {
        if (target != null)
        {
            Vector3.Lerp(transform.position, target.position, 0.75f);
        }
        else
        {
            new Vector3(0, 2, -3);
        }
    }

}
