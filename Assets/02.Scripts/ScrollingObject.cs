using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingObject : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 10f; // �̵� �ӵ�
    

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.instance.isGameover)
        {
            // ���� ������Ʈ�� ���� �ӵ��� �������� �����̵��ϴ� ó��
            transform.Translate(Vector3.left * speed * Time.deltaTime);
            //transform.position = transform.position + Vector3.left * Time.deltaTime * speed;
        }
    }
}
