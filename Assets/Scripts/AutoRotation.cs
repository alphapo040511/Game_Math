using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoRotation : MonoBehaviour
{

    // Y���� �������� 1�ʸ��� 45�� ȸ��
    void Update()
    {
        transform.Rotate(0, 45 * Time.deltaTime, 0);
    }
}
