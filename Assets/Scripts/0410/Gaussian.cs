using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gaussian
{
    float GenerateGaussian(float mean, float stdDev)
    {
        float u1 = 1.0f - Random.value; //0보다 큰 난수
        float u2 = 1.0f - Random.value; //0보다 큰 난수

        float randStdNormal = Mathf.Sqrt(-2.0f * Mathf.Log(u1)) *
                            Mathf.Sin(2.0f * Mathf.PI * u2);   //표준 정규 분포

        return mean + stdDev * randStdNormal;       //원하는 평균과 표준편차로 반환
    }
}
