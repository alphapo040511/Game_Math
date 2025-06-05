using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BillardsScoreManager : MonoBehaviour
{
    public static BillardsScoreManager Instance { get; private set; }

    public List<TextMeshProUGUI> scoreTexts = new List<TextMeshProUGUI>();

    private Dictionary<int, int> scores = new Dictionary<int, int>();

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        scores = new Dictionary<int, int>
        {
            {0,0 },
            {1,0 }
        };
    }

    public void GetPoint(int playerIndex)
    {
        if (scores.ContainsKey(playerIndex))
        {
            scores[playerIndex] += 1;
            scoreTexts[playerIndex].text = $"플레이어 {playerIndex + 1} 점수\n{scores[playerIndex].ToString()}";
        }
    }

    public void CanclePoint(int playerIndex)
    {
        if (scores.ContainsKey(playerIndex))
        {
            scores[playerIndex] -= 1;
            scoreTexts[playerIndex].text = $"플레이어 {playerIndex + 1} 점수\n{scores[playerIndex].ToString()}";
        }
    }
}
