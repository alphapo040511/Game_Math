using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class StandardDeviation : MonoBehaviour
{
    public int n = 1000;                                //»ùÇÃ ¼ö

    public float min = 0f;
    public float max = 1f;

    // Start is called before the first frame update
    void Start()
    {
        StdDev();
    }

    void StdDev()
    {
        float[] samples = new float[n];
        for (int i = 0; i < n; i++)
        {
            samples[i] = Random.Range(min, max);
        }

        float mean = MyAverage(samples);
        float sumOfSquares = MySum(samples, mean);
        float stdDev = Mathf.Sqrt(sumOfSquares / n);

        Debug.Log($"Æò±Õ: {mean}, Ç¥ÁØÆíÂ÷: {stdDev}");
    }

    float MyAverage(float[] samples)
    {
        float sum = 0;
        int length = samples.Length;

        for (int i = 0; i < length; i++)
        {
            sum += samples[i];
        }

        return sum / length;
    }

    float MySum(float[] samples, float mean)
    {
        float sum = 0;
        for (int i = 0; i < samples.Length; i++)
        {
            sum += Mathf.Pow(samples[i] - mean, 2);
        }

        return sum;
    }
}
