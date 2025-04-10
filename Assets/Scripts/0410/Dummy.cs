using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dummy : MonoBehaviour
{
    public void Hit(float damage)
    {
        AttackSimulatorUI.instance.DamageFloatingUI(transform, damage);
    }
}
