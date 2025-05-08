using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour
{
    public SkinnedMeshRenderer mesh;
    private Collider collider;

    private void Awake()
    {
        collider = GetComponent<Collider>();
    }

    public void Die()
    {
        mesh.enabled = false;
        collider.enabled = false;
        Invoke("Respawn", 3f);
    }    

    private void Respawn()
    {
        mesh.enabled = true;
        collider.enabled = true;
    }
}
