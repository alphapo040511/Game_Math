using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BilliardsPlayer : MonoBehaviour
{
    public List<BillardsBall> playebleBalls = new List<BillardsBall>();

    public List<Rigidbody> ballsRigidbody = new List<Rigidbody>();

    public TextMeshProUGUI turnText;

    public int currentPlayer = 0;

    private float checkingTimer = 0;

    private LineRenderer line;

    [SerializeField] private bool hit = false;

    [SerializeField] private Vector3 startPosition = Vector3.zero;
    [SerializeField] private Vector3 curretPosition = Vector3.zero;

    private void Awake()
    {
        line = GetComponent<LineRenderer>();
    }

    private void Start()
    {
        turnText.text = $"{currentPlayer + 1} 플레이어 턴";
    }

    // Update is called once per frame
    void Update()
    {

        if (!hit)
        {
            if(Input.GetMouseButtonDown(0))
            {
                startPosition = Input.mousePosition;
                line.enabled = true;
            }

            Vector3 dir = (startPosition - curretPosition).normalized;       //현재 위치에서 커서 방향으로 가는 벡터
            float distance = Vector3.Distance(startPosition, curretPosition) * 0.02f;   //발사할 힘
            distance = Mathf.Clamp(distance, 0, 8f);

            if (Input.GetMouseButton(0))
            {
                curretPosition = Input.mousePosition;
                Vector3 ballPos = playebleBalls[currentPlayer].transform.position;
                line.SetPosition(0, ballPos);
                line.SetPosition(1, ballPos + new Vector3(dir.x, 0, dir.y) * distance / 2);
            }


            if(Input.GetMouseButtonUp(0))
            {
                playebleBalls[currentPlayer].HitBall(new Vector3(dir.x, 0, dir.y), distance);
                hit = true;
                checkingTimer = Time.time;
                line.enabled = false;
            }
        }
    }

    private void LateUpdate()
    {
        if (hit && checkingTimer + 1f < Time.time)
        {
            for (int i = 0; i < ballsRigidbody.Count; i++)       //모든 공이 정지 해있는지 체크
            {
                if (ballsRigidbody[i].velocity.magnitude > 0.1f)
                {
                    return;
                }
                else
                {
                    ballsRigidbody[i].velocity = Vector3.zero;
                }
            }

            //모든 공이 멈춰 있다면 다음으로
            hit = false;
            currentPlayer = (int)Mathf.Repeat(currentPlayer + 1, 2);
            turnText.text = $"{currentPlayer + 1} 플레이어 턴";
        }
    }
}
