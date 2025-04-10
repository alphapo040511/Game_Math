using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CriticalSimulator : MonoBehaviour
{
    public TextMeshProUGUI attackCountText;
    public TextMeshProUGUI criCountText;
    public TextMeshProUGUI criPercentText;

    private float criPoint = 10;
    public int attackCount;
    private int criCount;
    private float criPercent;

    public void Attack()
    {
        attackCount++;

        if (((float)criCount + 1 / attackCount) > 10)
        {
            float random = Random.Range(0f, 100f);
            if (random <= criPoint)
            {
                criCount++;
            }
        }
        else
        {
            criCount++;
        }

        criPercent = (float)criCount / attackCount;

        attackCountText.text = $"°ø°İ È½¼ö - {attackCount}";
        criCountText.text = $"Ä¡¸íÅ¸ È½¼ö - {criCount}";
        criPercentText.text = $"Ä¡¸íÅ¸ ¹ß»ı·ü - {criPercent * 100:F2}%";
    }

    public bool Attack(float percent)
    {
        attackCount++;

        if (((float)criCount + 1 / attackCount) > 10)
        {
            float random = Random.Range(0f, 100f);
            if (random <= percent)
            {
                criCount++;
                return true;
            }
        }
        else
        {
            criCount++;
            return true;
        }

        return false;
    }

    public void ResetCount()
    {
        attackCount = 0;
        criCount = 0;
    }
}
