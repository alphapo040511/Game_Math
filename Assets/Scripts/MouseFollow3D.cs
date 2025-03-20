using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MouseFollow3D : MonoBehaviour
{
    public float speed = 5f;

    public LayerMask targetLayer;

    private Vector3 targetPosition;

    void Start()
    {
        targetPosition = transform.position;
    }

    void Update()
    {
        //마우스 왼쪽 버튼 클릭
         
        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit, Mathf.Infinity, targetLayer))
            {
                targetPosition = hit.point;
            }
        }

        targetPosition.y = transform.position.y;
        Vector3 direcion = (targetPosition - transform.position).normalized;
        transform.position += direcion * speed * Time.deltaTime;
        float distance = Vector3.Distance(transform.position, targetPosition);
        if (distance < 0.1f)
        {
            transform.position = targetPosition;
        }

    }
}
