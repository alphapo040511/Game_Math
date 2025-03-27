using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCrossProduct : MonoBehaviour
{
    public Transform target;

    // Update is called once per frame
    void Update()
    {
        Vector3 forward = transform.forward;
        Vector3 toTarget = (target.position - transform.position).normalized;

        if(IsLeft(forward, toTarget, Vector3.up))
        {
            Debug.Log("Ÿ���� ���ʿ� �ֽ��ϴ�.");
        }
        else
        {
            Debug.Log("Ÿ���� �����ʿ� �ֽ��ϴ�.");
        }
    }


    bool IsLeft(Vector3 forward, Vector3 toTarget, Vector3 up)
    {
        Vector3 cross = CrossProduction(forward, toTarget);
        return DotProduction(cross, up) < 0;
    }

    Vector3 CrossProduction(Vector3 forward, Vector3 target)
    {
        return new Vector3(
            forward.y * target.z - forward.z * target.y,
            forward.z * target.x - forward.x * target.z,
            forward.x * target.y - forward.y * target.x
            );
    }

    float DotProduction(Vector3 cross, Vector3 vectorUp)
    {
        return cross.x * vectorUp.x + cross.y * vectorUp.y + cross.z * vectorUp.z;
    }
}
