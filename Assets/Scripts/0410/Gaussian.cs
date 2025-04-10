using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gaussian
{
    float GenerateGaussian(float mean, float stdDev)
    {
        float u1 = 1.0f - Random.value; //0���� ū ����
        float u2 = 1.0f - Random.value; //0���� ū ����

        float randStdNormal = Mathf.Sqrt(-2.0f * Mathf.Log(u1)) *
                            Mathf.Sin(2.0f * Mathf.PI * u2);   //ǥ�� ���� ����

        return mean + stdDev * randStdNormal;       //���ϴ� ��հ� ǥ�������� ��ȯ
    }
}
