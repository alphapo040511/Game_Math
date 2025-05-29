using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionStatus : MonoBehaviour
{
    private int hp = 10;

    public void TakeDamage()
    {
        if(--hp <= 0)
        {
            Destroy(gameObject);
        }
    }
}
