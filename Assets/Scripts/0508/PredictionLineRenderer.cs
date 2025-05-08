using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PredictionLineRenderer : MonoBehaviour
{
    public Transform target;        // B

    public Axe axePrefab;

    [Range(1f, 5f)] public float extend = 1.5f;

    public LayerMask enemyLayer;

    public Image coolTimeImage;

    private LineRenderer lr;

    private bool isReady = true;

    private float coolTime = 3f;
    private float currentTime = 0;

    void Awake()
    {
        lr = GetComponent<LineRenderer>();
        lr.positionCount = 2;                       //단순 직선이므로 점 2개
        lr.widthMultiplier = 0.05f;                 //두깨
        lr.material = new Material(Shader.Find("Unlit/Color"))
        {
            color = Color.red
        };
        lr.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!isReady)
        {
            currentTime += Time.deltaTime;
            if (coolTimeImage != null)
            {
                coolTimeImage.fillAmount = Mathf.Min(currentTime / coolTime, 1);
            }
            if (currentTime >= coolTime)
            {
                isReady = true;
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray, out RaycastHit hit, 100f, enemyLayer))
            {
                target = hit.transform;
                lr.enabled = true;
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            target = null;
        }

        if (!target)
        {
            lr.enabled = false;
            return;
        }



        Vector3 a = transform.position;
        Vector3 b = target.position;
        Vector3 pred = Vector3.LerpUnclamped(a, b, extend);
        lr.SetPosition(0, a);
        lr.SetPosition(1, pred);

        
        if (Input.GetKeyDown(KeyCode.Space) && isReady)
        {
            isReady = false;
            currentTime = 0f;

            Axe axe = Instantiate(axePrefab, transform.position, Quaternion.identity);
            axe.LockOn(target);
            target = null;
        }
    }
}
