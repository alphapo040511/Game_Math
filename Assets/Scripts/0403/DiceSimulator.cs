using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class DiceSimulator : MonoBehaviour
{
    public static DiceSimulator instance;
    private void Awake()
    {
        instance = this;
    }

    public Transform graphParent;
    public GameObject graph;
    public TextMeshProUGUI diceNumberText;
    public Action<int[], int> rolled;
    public TMP_InputField inputField;

    private int[] trials;
    private int faceCount = 6;
    private int count = 0;

    //int[] counts = new int[6];

    //public int trials = 100;

    // Start is called before the first frame update
    void Start()
    {
        //for(int i = 0; i < trials; i++)
        //{
        //    int result = Random.Range(1, 7);
        //    counts[result - 1]++;
        //}

        //for(int i = 0; i < counts.Length; i++)
        //{
        //    float percent = (float)counts[i] / trials * 100f;
        //    Debug.Log($"{i + 1} : {counts[i]} 회 ({percent:F2}%)");
        //}

        NewDice();
    }

    public void NewDice()
    {
        faceCount = Int32.Parse(inputField.text);
        trials = new int[faceCount];
        count = 0;
        foreach(Transform i in graphParent)
        {
            Destroy(i.gameObject);
        }

        for(int i = 0; i < faceCount; i++)
        {
            GameObject temp = Instantiate(graph, graphParent);
            temp.GetComponent<DicePercent>().Init(i);
        }
    }

    public void Roll()
    {
        int number = UnityEngine.Random.Range(0, faceCount);
        trials[number]++;
        count++;
        diceNumberText.text = (number + 1).ToString();
        rolled.Invoke(trials, count);
    }
}
