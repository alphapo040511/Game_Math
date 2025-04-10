using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FloatingText : MonoBehaviour
{
    public Transform targetPosition;
    public TextMeshProUGUI tmp;

    private bool isMove = false;

    Vector3 pos;

    // Update is called once per frame
    private void Start()
    {
        pos = targetPosition.position;
        pos.y += 0.5f;
    }

    void Update()
    {
        if (isMove)
        {
            pos.y += Time.deltaTime; ;
        }

        Vector2 screenPos = Camera.main.WorldToScreenPoint(pos);
        transform.position = screenPos;
    }

    public void DamageInit(Transform target,float text, bool isMove)
    {
        tmp.color = Color.red;
        tmp.fontSize = 50;
        targetPosition = target;
        tmp.text = text.ToString("F0");
        isMove = true;
        Destroy(this.gameObject, 1f);
    }
}
