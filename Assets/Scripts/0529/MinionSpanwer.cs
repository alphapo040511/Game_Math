using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionSpanwer : MonoBehaviour
{
    public GameObject minion;

    public Transform parent;

    public Transform startPoint;
    public Transform endPoint;

    // Update is called once per frame
    void Update()
    {
        if(parent.childCount < 20)
        {
            Instantiate(minion, RandomPoint(), Quaternion.identity, parent);
        }
    }

    private Vector3 RandomPoint()
    {
        Vector3 point;

        point.x = Mathf.Lerp(startPoint.position.x, endPoint.position.x, Random.value);
        point.z = Mathf.Lerp(startPoint.position.z, endPoint.position.z, Random.value);

        point.y = 0;

        return point;
    }
}
