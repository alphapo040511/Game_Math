using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private CriticalSimulator criSimul;
    
    public WeaponData weaponData;
    private int level = 1;
    private float defaultAD;
    private float defatltDamage;

    private float minDamage = float.MaxValue;
    private float maxDamage = float.MinValue;
    private float sumDamage;

    private void Start()
    {
        criSimul = GetComponent<CriticalSimulator>();
        if (criSimul != null)
        {
            criSimul.ResetCount();
        }

        AttackSimulatorUI.instance.RecodeReset();
        StatusUpdate();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Minus))
        {
            ChangeLevel(level - 1);
        }
        if (Input.GetKeyDown(KeyCode.Equals))
        {
            ChangeLevel(level + 1);
        }


        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if(Input.GetMouseButtonDown(0))
        {
            if(Physics.Raycast(ray, out hit, 5f))
            {
                Dummy dummy = hit.collider.GetComponent<Dummy>();
                if (dummy != null)
                {
                    float damage = GetDamage();
                    dummy.Hit(damage);

                    sumDamage += damage;

                    if (minDamage > damage)
                    {
                        minDamage = damage;
                    }
                    else if(maxDamage < damage)
                    {
                        maxDamage = damage;
                    }

                    AttackSimulatorUI.instance.RecodeTextUpdate(minDamage, maxDamage, sumDamage, criSimul.attackCount);
                }
            }
        }
    }

    private float GetDamage()
    {
        bool isCri = criSimul.Attack(weaponData.criticalPercent);

        return defatltDamage * (isCri ? weaponData.criticalDamage : 1);
    }

    public void NewWeapon(WeaponData data)
    {
        weaponData = data;
        minDamage = float.MaxValue;
        maxDamage = float.MinValue;
        StatusUpdate();
        criSimul.ResetCount();
        AttackSimulatorUI.instance.RecodeReset();
    }

    public void ChangeLevel(int i)
    {
        Debug.Log(i);

        if (i <= 0)
        {
            return;
        }

        level = i;
        StatusUpdate();
        AttackSimulatorUI.instance.LevelTextUpdate(level);
    }

    private void StatusUpdate()
    {
        defaultAD = level * 20;
        defatltDamage = defaultAD * weaponData.dagameValue;
        AttackSimulatorUI.instance.StatusUpdate(weaponData.weaponName, defatltDamage, weaponData.criticalPercent, weaponData.criticalDamage);
    }
}
