using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class MinionMovement : MonoBehaviour
{
    public float speed = 5f;

    Vector3 direct;

    private bool wait = false;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(NewDirect());
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        if(Physics.Raycast(ray, 0.5f) || wait)
        {
            animator.SetBool("wall", true);
            return;
        }
        else
        {
            animator.SetBool("wall", false);
        }

        transform.position += direct * speed * Time.deltaTime;
        if (direct != Vector3.zero)
        {

            Quaternion lookRot = Quaternion.LookRotation(direct);

            float t = 1f - Mathf.Exp(-speed * Time.deltaTime);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRot, t);
        }
    }

    IEnumerator NewDirect()
    {
        while (true)
        {
            direct = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized;
            wait = false;

            yield return new WaitForSeconds(Random.Range(3f, 7f));

            direct = Vector3.zero;
            wait = true;

            yield return new WaitForSeconds(1f);
        }
    }
}
