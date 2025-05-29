using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierCurve : MonoBehaviour
{
    public Transform p0;        //시작점
    public Transform p3;        //도착점


    [Header("Random Ranges")]
    public float p1Radius = 2f;         //p0 근처에서 뽑는 반경

    public float p2Radius = 2f;         //p3 근처에서 뽑는 반경

    public float p1Height = 2f;         //p1 y축 추가 높이

    public float p2Height = 2f;        //p2 y축 추가 높이


    //결과 제어점
    [HideInInspector] public Vector3 p1;
    [HideInInspector] public Vector3 p2;

    private List<Vector3> points;

    private float timeValue = 0f;

    // Start is called before the first frame update
    void Start()
    {
        GenerateRandomControlPoint();
        points = new List<Vector3>{ 
            p0.position,
            p1,
            p2,
            p3.position
            };
    }

    // Update is called once per frame
    void Update()
    {
        timeValue = Mathf.Repeat(timeValue + Time.deltaTime / 2f, 1);
        transform.position = DeCastelijau(points, timeValue);
    }


    private void GenerateRandomControlPoint()
    {
        Vector3 random1 = Random.insideUnitCircle * p1Radius;
        p1 = p0.position + new Vector3(random1.x, 0, random1.y);
        p1.y += p1Height;                                               //살짝 위로 띄워 궤적 상승

        Vector3 random2 = Random.insideUnitCircle * p2Radius;
        p2 = p3.position + new Vector3(random2.x, 0, random2.y);
        p2.y += p2Height;                                               //도착 직전 살짝 뜨도록
    }

    //4차 이상 부터
    private Vector3 DeCastelijau(List<Vector3> p, float t)
    {
        while (p.Count > 1)                  //남은 point가 1이 될 때 까지 반복 (모든 point가 lerp 된 상황 까지)
        {
            int last = p.Count - 1;         //마지막 요소의 인덱스

            List<Vector3> next = new List<Vector3>();
            for (int i = 0; i < last; i++)                                  //0부터 마지막 요소(-1)까지 순회
            {
                next.Add(Vector3.Lerp(p[i], p[i + 1], t));                  //ab,bc,cd 형식으로 계속 Lerp
            }

            p = next;                       //p(다음 순회의 대상)을 현재의 결과로 변경
        }

        return p[0];                        //마지막 남은 위치가 Bezier Curve의 결과
    }
}
