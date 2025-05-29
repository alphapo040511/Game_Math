
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bezier : MonoBehaviour
{
    public Transform point0;
    public Transform point1;
    public Transform point2;
    public Transform point3;

    public List<Transform> points = new List<Transform>();
    private List<Vector3> pointsPosition = new List<Vector3>();

    float timeValue = 0;

    private void Start()
    {
        foreach(Transform pt in points)
        {
            if (pointsPosition != null)
            {
                pointsPosition.Add(pt.position);
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        timeValue += Time.deltaTime / 2f;       //2초 동안 애니메이션
        //transform.position = GetPointOnBezierCurve(point0.position, point1.position, point2.position, point3.position, timeValue);
        transform.position = DeCastelijau(pointsPosition, timeValue);
    }

    //4차 이상 부터
    private Vector3 DeCastelijau(List<Vector3> p, float t)
    {
        while(p.Count > 1)                  //남은 point가 1이 될 때 까지 반복 (모든 point가 lerp 된 상황 까지)
        {
            int last = p.Count - 1;         //마지막 요소의 인덱스

            List<Vector3> next = new List<Vector3>();
            for(int i = 0; i < last; i ++)                                  //0부터 마지막 요소(-1)까지 순회
            {
                next.Add(Vector3.Lerp(p[i], p[i + 1], t));                  //ab,bc,cd 형식으로 계속 Lerp
            }

            p = next;                       //p(다음 순회의 대상)을 현재의 결과로 변경
        }

        return p[0];                        //마지막 남은 위치가 Bezier Curve의 결과
    }


    //4차 미만 까지 직접 구현
    private Vector3 GetPointOnBezierCurve(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
    {
        Vector3 a = Vector3.Lerp(p0, p1, t);
        Vector3 b = Vector3.Lerp(p1, p2, t);
        Vector3 c = Vector3.Lerp(p2, p3, t);
        Vector3 ab = Vector3.Lerp(a, b, t);
        Vector3 bc = Vector3.Lerp(b, c, t);
        Vector3 abc = Vector3.Lerp(ab, bc, t);

        return abc;
    }
}
