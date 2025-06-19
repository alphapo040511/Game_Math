using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManualExplode : MonoBehaviour
{
    public float delay = 1.5f;

    public float radius = 5f;

    public float force = 300f;

    public float upwardsModifier;

    private int damage = 1;

    public GameObject model;
    public ParticleSystem explosionVFX;

    // Start is called before the first frame update
    public void DelayStart(int damageValue)
    {
        damage = damageValue;
        GetComponent<Rigidbody>().isKinematic = false;
        Invoke(nameof(RunExplode), delay);
    }

    private void RunExplode()
    {
        Vector3 explosionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
        foreach(var col in colliders)
        {
            Rigidbody rb = col.attachedRigidbody;
            if (rb == null) continue;
            Vector3 toTarget = rb.position - explosionPos;
            float distance = toTarget.magnitude;
            Vector3 dir = toTarget.normalized;
            float attenuation = 1f - Mathf.Clamp01(distance / radius);
            dir += dir.normalized;
            Vector3 impulse = dir * force * attenuation;
            rb.AddForce(impulse, ForceMode.Impulse);

            IMovedObject IMoved = col.GetComponent<IMovedObject>();
            if (IMoved != null)
            {
                IMoved.ChangeStats(IMovedObject.ObjectStats.Airborne);
            }

            MinionStatus minion = col.GetComponent<MinionStatus>();
            if(minion != null)
            {
                minion.TakeDamage(damage);
            }

        }
        model.SetActive(false);
        explosionVFX.Play();
        Destroy(gameObject, 1.5f);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
