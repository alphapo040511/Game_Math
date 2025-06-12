using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionStatus : MonoBehaviour
{
    private int hp = 10;

    public void TakeDamage(int damage = 1)
    {
        hp -= damage;
        if(hp <= 0)
        {
            Destroy(gameObject);
        }
    }
}
