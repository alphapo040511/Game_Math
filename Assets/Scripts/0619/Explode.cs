using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode : MonoBehaviour
{
    public float force = 300f;

    public float radius = 5f;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("RunExplode", 2f);       //폭발 지연 시간 
    }

    private void RunExplode()
    {
        Vector3 explosionPos = transform.position;
        Collider[] hitColliders = Physics.OverlapSphere(explosionPos, radius);
        foreach(var col in hitColliders)
        {
            Rigidbody rb = col.attachedRigidbody;
            if(rb != null)
            {
                rb.AddExplosionForce(force, explosionPos, radius);
            }
        }
        Destroy(gameObject);
    }
}
