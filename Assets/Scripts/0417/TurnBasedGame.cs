using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TurnBasedGame : MonoBehaviour
{
    public TextMeshProUGUI dialogText;

    public float enemyMean = 2f;
    public float enemyHp = 100f;
    public float rareChange = 0.2f;
    public float meanDamage = 20f;
    public float stdDamage = 5f;
    public int maxHitsPerTirn = 5;
    public float hitRate = 0.6f;
    public float critChance = 0.2f;
    public float critDamageRate = 2f;

    private int turn = 0;
    private bool rareItemObtained = false;

    private string[] rewards = { "Gold", "Weapon", "Armor", "Potion" };
    private Queue<string> dialogs = new Queue<string>();

    private bool writing = false;

    private string writeText;

    private Coroutine dialogCoroutine;

    private int _totalEnemy = 0;
    private int _totalKill = 0;
    private int _totalDamage = 0;
    private int _totalHit = 0;
    private int _totalCrit = 0;
    private int _totalAttack = 0;
    private float _maxDmg = float.MinValue;
    private float _minDmg = float.MaxValue;



    private void Start()
    {
        dialogText.text = "텍스트 박스를 클릭하여 시작";
    }

    public void OnClick()
    {
        if (writing)
        {
            StopCoroutine(dialogCoroutine);
            dialogText.text = dialogs.Dequeue();
            writing = false;
            return;
        }

        if(dialogs.Count > 0)
        {
            dialogCoroutine = StartCoroutine(ShowDialog());
        }
        else if (rareItemObtained)
        {
            GameReset();
        }
        else
        {
            NewTurn();
        }
    }

    private IEnumerator ShowDialog()
    {
        writing = true;
        writeText = "";
        dialogText.text = writeText;
        foreach (char c in dialogs.Peek())
        {
            writeText += c;
            dialogText.text = writeText;
            yield return new WaitForSeconds(0.1f);
        }

        writing = false;
        dialogs.Dequeue();
    }

    public void NewTurn()
    {
        turn++;
        rareChange = 0.2f + (turn - 1) * 0.1f;
        SimulateTurn();
    }

    public void SimulateTurn()
    {
        dialogs.Enqueue($"턴 {turn} 시작");
        dialogs.Enqueue($"이번 턴의 레어 아이템 획득 확률 {rareChange * 100:F0}%");

        //몬스터 생성
        int enemyCount = SamplePoisson(enemyMean);
        dialogs.Enqueue($"몬스터 수 : {enemyCount} 마리");
        _totalEnemy += enemyCount;

        //몬스터 공격
        for(int i = 0; i < enemyCount; i++)
        {
            dialogs.Enqueue($"{i + 1} 번째 몬스터 공격");
            //명중 횟수
            int hits = SampleBinomial(maxHitsPerTirn, hitRate);
            _totalAttack += maxHitsPerTirn;
            _totalHit += hits;
            float totalDamage = 0;

            for(int j = 0; j < hits; j++)
            {

                float damage = SampleNormal(meanDamage, stdDamage);

                //베르누이 분포 샘플 치명타
                if(Random.value < critChance)
                {
                    _totalCrit++;
                    damage *= critDamageRate;
                    dialogs.Enqueue($"{damage:F1} 데미지 치명타 공격");
                }
                else
                {
                    dialogs.Enqueue($"{damage:F1} 데미지 공격");
                }

                if(damage > _maxDmg)
                {
                    _maxDmg = damage;
                }
                if(damage < _minDmg)
                {
                    _minDmg = damage;
                }

                totalDamage += damage;
            }

            dialogs.Enqueue($"총 공격 횟수 {hits}, 총 데미지 {totalDamage:F2}");

            if (totalDamage >= enemyHp)
            {
                //적 처치
                string reward = rewards[Random.Range(0, rewards.Length)];
                _totalKill++;

                if(reward == "Weapon" && Random.value < rareChange)
                {
                    dialogs.Enqueue($"레어 무기 획득!");
                    rareItemObtained = true;
                }
                else if (reward == "Armor" && Random.value < rareChange)
                {
                    dialogs.Enqueue($"레어 방어구 획득!");
                    rareItemObtained = true;
                }
                else
                {
                    dialogs.Enqueue($"{reward} 획득!");
                }
            }
            else
            {
                dialogs.Enqueue($"몬스터 처치 실패...");
            }
        }
        dialogs.Enqueue($"턴 종료");

        if (rareItemObtained)
        {
            dialogs.Enqueue($"레어 무기 획득으로 게임 종료");
            dialogs.Enqueue($"진행한 턴 : {turn} 턴");
            dialogs.Enqueue($"등장한 몬스터 : {_totalEnemy} 마리\n처치한 몬스터 : {_totalKill} 마리");
            dialogs.Enqueue($"총 공격 횟수 : {_totalAttack} 회\n공격 적중률 : {(_totalHit / _totalAttack) * 100:F2}%\n치명타 발생률 : {(_totalCrit / _totalAttack) * 100:F2}%");
            dialogs.Enqueue($"최고 데미지 : {_maxDmg:F1}\n최소 데미지 : {_minDmg:F1}");
            dialogs.Enqueue($"클릭하여 재시작");
        }
    }

    public int SamplePoisson(float lambda)
    {
        int k = 0;
        float p = 1f;
        float L = Mathf.Exp(-lambda);
        while(p > L)
        {
            k++;
            p *= Random.value;
        }
        return k - 1;
    }

    public int SampleBinomial(int n, float rate)
    {
        int successes = 0;
        for(int i = 0; i < n; i++)
        {
            if(Random.value < rate)
            {
                successes++;
            }
        }

        return successes;
    }


    float SampleNormal(float mean, float stdDev)
    {
        float u1 = Random.value;
        float u2 = Random.value;
        float z = Mathf.Sqrt(-2.0f * Mathf.Log(u1)) * Mathf.Cos(2.0f * Mathf.PI * u2);
        return mean + stdDev * z;
    }

    private void GameReset()
    {
        turn = 0;
        rareItemObtained = false;
        writing = false;
        _totalEnemy = 0;
        _totalKill = 0; ;
        _totalDamage = 0;
        _totalHit = 0;
        _totalCrit = 0;
        _totalAttack = 0;
        _maxDmg = float.MinValue;
        _minDmg = float.MaxValue;
    }
}
