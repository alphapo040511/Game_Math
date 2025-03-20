using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TFMove : MonoBehaviour
{
    private float speed = 5f;
    private float angle = 30f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            speed++;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            speed--;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            angle = Mathf.Repeat(angle -15 , 360);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            angle = Mathf.Repeat(angle + 15, 360);
        }

        float radians = angle * Mathf.Deg2Rad;
        Vector3 direcion = new Vector3(Mathf.Cos(radians), 0, Mathf.Sin(radians));
        transform.position += direcion * speed * Time.deltaTime;
    }
}
