using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BouncingBomb : MonoBehaviour
{
    public LayerMask ground;
    public Bomb bomb;

    public GameObject rangeImage;
    public GameObject hitRangeImage;

    public LayerMask enemyLayer;
    public float radius = 5f;

    public Image cooTimeImage;
    public TextMeshProUGUI coolTimeText;
    public GameObject[] levelIcon = new GameObject[5];

    private int level = 0;

    private int[] damage = new int[5] { 5, 6, 7, 8, 10 };

    private float currentTime = 0f;
    private float[] coolTime = new float[5] { 5f, 4f, 3f, 2f, 1f };
    private bool coolDown = false;

    private Vector3 targetPos;

    // Update is called once per frame
    void Update()
    {
        if (coolDown)
        {
            CoolTimer();
        }

        if (Input.GetKey(KeyCode.LeftControl))
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                level = Mathf.Min(4, level + 1);
                //레벨 업
                for (int i = 0; i < 5; i++)
                {
                    levelIcon[i].SetActive(i <= level);
                    Debug.Log($"레벨+1 (현재 레벨 {level + 1})");
                }
            }
        }
        else
        {

            if (Input.GetKeyDown(KeyCode.W))
            {
                rangeImage.SetActive(true);
                hitRangeImage.SetActive(true);
            }

            if(Input.GetKey(KeyCode.W))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, Mathf.Infinity, ground))
                {
                    GetTargetPos(hit.point); 
                }

                hitRangeImage.transform.position = targetPos + Vector3.up * 0.1f;
            }

            if (Input.GetKeyUp(KeyCode.W))
            {
                rangeImage.SetActive(false);
                hitRangeImage.SetActive(false);
                if (!coolDown)
                {
                    Fire();
                    coolDown = true;
                    currentTime = coolTime[level];
                    coolTimeText.enabled = true;
                }
            }
        }
    }

    private void GetTargetPos(Vector3 point)
    {
        float distance = Vector3.Distance(transform.position, point);
        if(distance > radius)
        {
            targetPos = transform.position + point.normalized * radius;
        }
        else
        {
            targetPos = point;
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


    private void Fire()
    {
        Bomb temp = Instantiate(bomb, transform.position, Quaternion.identity);
        temp.Init(targetPos, damage[level]);
    }
}
