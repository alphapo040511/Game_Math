using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class Axe : MonoBehaviour
{
    public float rotateSpeed = 10f;
    public float moveSpeed = 10f;

    public Transform model;

    private Vector3 startPos;

    private Transform target;

    private float duration = 2f;
    private float t = 0f;

    Quaternion deltaRotation;

    // Update is called once per frame
    void Update()
    {
        if (model != null)
        {
            model.Rotate(rotateSpeed * Time.deltaTime, 0, 0);
        }

        if (target == null) return;

        transform.LookAt(target);

        t += Time.deltaTime / duration;


        transform.position = Vector3.Lerp(startPos, target.position, t);

        if(Vector3.Distance(transform.position, target.position) < 0.5f)
        {
            Slime slime = target.GetComponent<Slime>();
            if (slime != null)
            {
                slime.Die();
                Destroy(gameObject);
            }
        }
    }

    public void LockOn(Transform t)
    {
        target = t;
        startPos = transform.position;
    }
}
