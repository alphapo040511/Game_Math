using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IMovedObject : MonoBehaviour
{
    public enum ObjectStats
    {
        Idel,
        CC,
        Airborne
    }

    protected ObjectStats currentState;

    public void ChangeStats(ObjectStats type)
    {
        currentState = type;
    }

    public void ChangeStats(ObjectStats type, float time)
    {
        //지금은 굳이 없어도 되겠다 싶어용
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.CompareTag("Plane"))
        {
            ChangeStats(ObjectStats.Idel);
        }
    }
}
