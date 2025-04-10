using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class AttackSimulatorUI : MonoBehaviour
{
    public static AttackSimulatorUI instance;

    private void Awake()
    {
        instance = this;
    }

    public Transform canvas;

    public TextMeshProUGUI statusText;

    public TextMeshProUGUI damageRecode;

    public FloatingText Prefab;

    public TextMeshProUGUI levelText;

    public void DamageFloatingUI(Transform target, float damage)
    {
        FloatingText text = Instantiate(Prefab, canvas);
        text.DamageInit(target, damage, true);
    }

    public void StatusUpdate(string weaponName, float defalutDamage, float criPercent, float CriDamage)
    {
        statusText.text = $"무기 : {weaponName}\n기본 데미지 : {defalutDamage}\n치명타 확률 : {criPercent * 100}%\n치명타 데미지 : {CriDamage * 100}%";
    }

    public void RecodeTextUpdate(float min, float max, float sum, int count)
    {
        string minDaamage = "";
        string maxDamage = "";
        if(min != float.MaxValue)
        {
            minDaamage = min.ToString("F0");
        }

        if (max != float.MinValue)
        {
            maxDamage = max.ToString("F0");
        }

        damageRecode.text = $"최소 데미지 : {min}\n최대 데미지 : {max}\n총 데미지 : {sum:F2}\n공격 횟수 : {count}";
    }

    public void LevelTextUpdate(int level)
    {
        levelText.text = $"Level {level}";
    }
    public void RecodeReset()
    {
        damageRecode.text = $"최소 데미지 : \n최대 데미지 : \n총 데미지 : \n공격 횟수 : ";
    }
}
