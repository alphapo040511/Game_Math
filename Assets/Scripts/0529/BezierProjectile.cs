using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierProjectile : MonoBehaviour
{
    public GameObject mesh;

    public Vector3 p0;        //시작점
    public MinionStatus p3;        //도착점
    private Vector3 lastpoint;


    [Header("Random Ranges")]
    public float p1Radius = 3f;         //p0 근처에서 뽑는 반경

    public float p2Radius = 3f;         //p3 근처에서 뽑는 반경

    public float p1Height = 5f;         //p1 y축 추가 높이

    public float p2Height = 3f;        //p2 y축 추가 높이

    private bool ready = false;

    //결과 제어점
    [HideInInspector] public Vector3 p1;
    [HideInInspector] public Vector3 p2Added;       //p2 위치가 p3(타겟)을 따라 유동적이도록 추가값만 저장

    private List<Vector3> points;

    private float timeValue = 0f;
    private bool hit = false;

    public void Init(Vector3 playerPosition, MinionStatus target)
    {
        GenerateRandomControlPoint();
        p0 = playerPosition;
        p3 = target;
        ready = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!ready) return;

        if(p3 != null)
        {
            timeValue += Time.deltaTime;
            transform.position = new Vector3(
                FourPointBezier(p0.x, p1.x, p2Added.x, p3.transform.position.x, timeValue),
                 FourPointBezier(p0.y, p1.y, p2Added.y, p3.transform.position.y, timeValue),
                  FourPointBezier(p0.z, p1.z, p2Added.z, p3.transform.position.z, timeValue)
                );

            if (Vector3.Distance(transform.position, p3.transform.position) < 0.1f)
            {
                if (!hit)
                {
                    p3.TakeDamage();
                    hit = true;
                }
                mesh.SetActive(false);
                Invoke("DestroyObject", 0.5f);
            }

            lastpoint = p3.transform.position;
        }
        else
        {
            timeValue += Time.deltaTime;
            transform.position = new Vector3(
                FourPointBezier(p0.x, p1.x, p2Added.x, lastpoint.x, timeValue),
                 FourPointBezier(p0.y, p1.y, p2Added.y, lastpoint.y, timeValue),
                  FourPointBezier(p0.z, p1.z, p2Added.z, lastpoint.z, timeValue)
                );

            if (Vector3.Distance(transform.position, lastpoint) < 0.1f)
            {
                mesh.SetActive(false);
                Invoke("DestroyObject", 0.5f);
            }
        }

        
    }

    private void DestroyObject()
    {
        Destroy(gameObject);
    }


    private void GenerateRandomControlPoint()
    {
        Vector3 random1 = Random.insideUnitCircle * p1Radius;
        p1 = p0 + new Vector3(random1.x, 0, random1.y);
        p1.y += p1Height;                                               //살짝 위로 띄워 궤적 상승

        Vector3 random2 = Random.insideUnitCircle * p2Radius;
        p2Added = new Vector3(random2.x, 0, random2.y);
        p2Added.y += p2Height;                                              //도착 직전 살짝 뜨도록
    }

    private float FourPointBezier(float a, float b, float c, float d, float t)
    {
        return Mathf.Pow(1 - t, 3) * a
            + Mathf.Pow(1 - t, 2) * 3 * t * b
            + Mathf.Pow(t, 2) * 3 * (1 - t) * (d + c)           //p3 + p2(상대 위치 추가)
            + Mathf.Pow(t, 3) * d;
    }
}
