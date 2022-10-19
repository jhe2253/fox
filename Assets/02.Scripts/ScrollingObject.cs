using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingObject : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 10f; // 이동 속도
    

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.instance.isGameover)
        {
            // 게임 오브젝트를 일정 속도로 왼쪽으로 평행이동하는 처리
            transform.Translate(Vector3.left * speed * Time.deltaTime);
            //transform.position = transform.position + Vector3.left * Time.deltaTime * speed;
        }
    }
}
