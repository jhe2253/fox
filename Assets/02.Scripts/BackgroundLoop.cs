using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundLoop : MonoBehaviour
{
    // Start is called before the first frame update
    // ���� ������ �̵��� ����� ������ ������ ���ġ�ϴ� ��ũ��Ʈ
    private float width;
    private void Awake()
    {
        // ���� ���̸� �����ϴ� ó��
        // BoxCollider2D ������Ʈ�� Size �ʵ��� x ���� ���� ���̷� ���
        BoxCollider2D backgroundCollider = GetComponent<BoxCollider2D>();
        width = backgroundCollider.size.x;
        //width = GetComponent<BoxCollider2D>().size.x;
    }

    // Update is called once per frame
    void Update()
    {
        // ���� ��ġ�� �������� �������� width �̻� �̵����� �� ��ġ�� ���ġ
        if(transform.position.x <= -width)
        {
            Reposition();
        }
    }

    // ��ġ�� ���ġ�ϴ� �޼���
    private void Reposition()
    {
        // ���� ��ġ���� ���������� ���� ���� * 2��ŭ �̵�
        Vector2 offset = new Vector2(width * 2f, 0);
        transform.position = (Vector2)transform.position + offset;
        //transform.position = (Vector2)transform.position + new Vector2(width * 2, 0);
    }
}
