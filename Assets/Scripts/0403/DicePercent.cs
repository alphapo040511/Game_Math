using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DicePercent : MonoBehaviour
{
    public Image ber;
    public TextMeshProUGUI tmp;
    private int number;

    public void OnDisable()
    {
        DiceSimulator.instance.rolled -= UpdateGraph;
    }

    public void Init(int index)
    {
        number = index;
        ber.transform.localScale = new Vector3(1, 0, 1);
        DiceSimulator.instance.rolled += UpdateGraph;
    }

    public void UpdateGraph(int[] list, int counts)
    {
        float percent = (float)list[number] / counts;
        ber.transform.localScale = new Vector3(1, percent, 1);
        tmp.text = $"[{number + 1}] {list[number]}¹ø\n{percent * 100:F2}%";
    }
}
