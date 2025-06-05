using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsTest : MonoBehaviour
{
    public float forcePower = 10f;

    Rigidbody rb;


    [SerializeField] private float speed;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(Vector3.forward * forcePower, ForceMode.Impulse);

        //rb.AddForce(Vector3.forward * 10, ForceMode.Force);   //기본 값 -> 연속적으로 힘을 밀어주고 싶을 때 (매 프레임 조금씩)
        //rb.AddForce(Vector3.forward * 10, ForceMode.Impulse); //즉시 적용 -> 순간적으로 힘을 주고 싶을 때 (한 번에 즉시)
        //forward = 0,0,1
        //실제 준 힘 = 0,0,10
    }

    private void FixedUpdate()
    {
        //space바를 누를 때
        //if(Input.GetKey(KeyCode.Space))
        //{
        //    rb.AddForce(Vector3.forward * 10, ForceMode.Force);
        //}
    }

    // Update is called once per frame
    void Update()
    {
        speed = rb.velocity.magnitude;  //속도의 크기 변수화
    }
}
