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
        statusText.text = $"���� : {weaponName}\n�⺻ ������ : {defalutDamage}\nġ��Ÿ Ȯ�� : {criPercent * 100}%\nġ��Ÿ ������ : {CriDamage * 100}%";
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

        damageRecode.text = $"�ּ� ������ : {min}\n�ִ� ������ : {max}\n�� ������ : {sum:F2}\n���� Ƚ�� : {count}";
    }

    public void LevelTextUpdate(int level)
    {
        levelText.text = $"Level {level}";
    }
    public void RecodeReset()
    {
        damageRecode.text = $"�ּ� ������ : \n�ִ� ������ : \n�� ������ : \n���� Ƚ�� : ";
    }
}
