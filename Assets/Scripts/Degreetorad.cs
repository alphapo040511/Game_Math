using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Degreetorad : MonoBehaviour
{
    public float Degree;
    public float RadianValue;

    // Start is called before the first frame update
    void Start()
    {
        float radians = Degree * Mathf.Deg2Rad;
        Debug.Log($"Degree to radian : {radians}");

        float degreeValue = RadianValue * Mathf.Rad2Deg;
        Debug.Log($"Radian to Degree: {degreeValue}");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
