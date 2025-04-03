using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GachaSimulator : MonoBehaviour
{
    public float cRank = 40f;
    public float bRank = 30f;
    public float aRank = 20f;
    public float sRank = 10f;

    public Color cRankColor;
    public Color bRankColor;
    public Color aRankColor;
    public Color sRankColor;

    public TextMeshProUGUI[] cards = new TextMeshProUGUI[10];
    public Image[] images = new Image[10];

    void Start()
    {
        ResetCard();
    }

    public void Once()
    {
        ResetCard();
        GetRank(0);
    }

    public void TenTimes()
    {
        ResetCard();

        StopAllCoroutines();
        StartCoroutine(DrawCards());
    }

    IEnumerator DrawCards()
    {
        yield return new WaitForSeconds(0.15f);
        for (int i = 0; i < 9; i++)
        {
            GetRank(i);
            yield return new WaitForSeconds(0.1f);
        }
        LastOne();
    }

    private void ResetCard()
    {
        for(int i = 0; i < 10; i++)
        {
            cards[i].text = "";
            images[i].color = Color.clear;
        }
    }

    private void GetRank(int index)
    {
        float random = UnityEngine.Random.Range(0f, cRank + bRank + aRank + sRank);

        if(random <= cRank)
        {
            cards[index].text = "C";
            images[index].color = cRankColor;
        }
        else if(random <= cRank + bRank)
        {
            cards[index].text = "B";
            images[index].color = bRankColor;
        }
        else if (random <= cRank + bRank + aRank)
        {
            cards[index].text = "A";
            images[index].color = aRankColor;
        }
        else
        {
            cards[index].text = "S";
            images[index].color = sRankColor;
        }
    }

    private void LastOne()
    {
        float random = UnityEngine.Random.Range(0f, aRank + sRank);

        if (random <= aRank)
        {
            cards[9].text = "A";
            images[9].color = aRankColor;
        }
        else
        {
            cards[9].text = "S";
            images[9].color = sRankColor;
        }
    }
}
