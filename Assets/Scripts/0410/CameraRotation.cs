using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    public Transform player;

    public float rotationSpeed = 5f;    // ȸ�� �ӵ�

    private float yaw = 0f;             // ���� ȸ��
    private float pitch = 0f;           // ���� ȸ��

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;                       //���콺 Ŀ���� ���
    }

    void Update()
    {
        transform.position = player.position + Vector3.up * 0.5f;

        float mouseX = Input.GetAxis("Mouse X");                        //���콺�� ���� ���� �Է� �޾ƿ�
        float mouseY = Input.GetAxis("Mouse Y");                        //���콺�� ���� ���� �Է� �޾ƿ�

        yaw += mouseX * rotationSpeed * Time.deltaTime;                 // ���� ȸ��

        pitch -= mouseY * rotationSpeed * Time.deltaTime;               // ���� ȸ��, -�� ���� ����

        pitch = Mathf.Clamp(pitch, -90f, 90f);                          // pitch�� 90�� �̻�, -90�� ���Ϸ� ȸ������ �ʵ��� ����

        transform.rotation = Quaternion.Euler(pitch, yaw, 0f);          // ȸ�� ����
    }
}
