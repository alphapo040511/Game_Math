using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProjectileTargeting : MonoBehaviour
{
    public BezierProjectile projectile;

    public GameObject rangeImage;

    public LayerMask enemyLayer;
    public float radius = 3f;

    public Image cooTimeImage;
    public TextMeshProUGUI coolTimeText;
    public GameObject[] levelIcon = new GameObject[5];

    private int level = 0;

    private int[] projectileCount = new int[5] {5, 8, 10, 12, 15};

    private float currentTime = 0f;
    private float[] coolTime = new float[5] { 5f, 4f, 3f, 2f, 1f };
    private bool coolDown = false;

    // Update is called once per frame
    void Update()
    {
        if(coolDown)
        {
            CoolTimer();
        }

        if (Input.GetKey(KeyCode.LeftControl))
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                level++;
                //레벨 업
                for(int i = 0; i < 5; i++)
                {
                    levelIcon[i].SetActive(i <= level);
                    Debug.Log($"레벨+1 (현재 레벨 {level + 1})");
                }
            }
        }
        else
        {

            if (Input.GetKeyDown(KeyCode.Q))
            {
                rangeImage.SetActive(true);
            }

            if (Input.GetKeyUp(KeyCode.Q))
            {
                rangeImage.SetActive(false);
                if (!coolDown)
                {
                    Collider[] targets = Physics.OverlapSphere(transform.position, radius, enemyLayer);

                    if (targets.Length > 0)
                    {
                        List<MinionStatus> minions = new List<MinionStatus>();

                        foreach (var target in targets)
                        {
                            MinionStatus minion = target.GetComponent<MinionStatus>();
                            if (minion != null)
                            {
                                minions.Add(minion);
                            }
                        }

                        if (minions.Count > 0)
                        {
                            StartCoroutine(Fire(minions));
                        }
                    }

                    coolDown = true;
                    currentTime = coolTime[level];
                    coolTimeText.enabled = true;
                }
            }
        }
    }

    private void CoolTimer()
    {
        currentTime -= Time.deltaTime;

        cooTimeImage.fillAmount = (coolTime[level] - currentTime) / coolTime[level];
        coolTimeText.text = (currentTime).ToString("F0");

        if (currentTime <= 0)
        {
            coolDown = false;
            coolTimeText.enabled = false;
        }
    }



    IEnumerator Fire(List<MinionStatus> minions)
    {
        for(int i = 0; i < projectileCount[level]; i++)
        {
            BezierProjectile temp = Instantiate(projectile, transform.position, Quaternion.identity);
            temp.Init(transform.position, minions[Random.Range(0, minions.Count)]);
            yield return new WaitForSeconds(0.05f);
        }
    }
}
