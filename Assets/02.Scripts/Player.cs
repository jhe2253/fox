using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private bool isDead = false; // 사망상태
    private int hitCount = 0; // 장애물 혹은 적에 부딪힌 횟수

    private Rigidbody2D myrigid; // 사용할 리지드바디 컴포넌트
    private Animator animator; // 사용할 애니메이터 컴포넌트
    private SpriteRenderer spriteRenderer; // 사용할 스프라이트 컴포넌트

    public float jumppow = 350f;

    public GameObject[] potionimages; // 포션 이미지 배열
    public bool isInvincible;

    private void Start()
    {
        myrigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (Time.timeScale != 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                myrigid.velocity = new Vector2(myrigid.velocity.x, 0);
                myrigid.AddForce(Vector2.up * jumppow);
            }
        }
    }

    private void Die()
    {
        // 애니메이터의 Die 트리거 파라미터를 셋
        animator.SetTrigger("Die");
        // 플레이어를 약간 뒤쪽으로 밀리게 함
        myrigid.AddForce(new Vector2(-200f, 300f));
        // 속도를 제로(0, 0)로 변경
        myrigid.velocity = Vector2.zero;
        // 사망 상태를 true로 변경
        isDead = true;

        // 게임 매니저의 게임오버 처리 실행
        GameManager.instance.OnPlayerDead();
    }

    // 트리거 작용 Switch문
    private void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.tag)
        {
            case "Dead":
                if (!isDead)
                {
                    Die();
                }
                break;
            case "Enemies":
                if (isInvincible)
                {
                    return;
                }

                if (hitCount < 3 && !isDead)
                {
                    GameManager.instance.reduceHeart();
                    hitCount++;
                    isInvincible = true;
                    StartCoroutine(Hit());
                }

                if (hitCount >= 3 && !isDead)
                {
                    Die();
                }
                break;
            case "Cherry":
                //GameManager.instance.AddScore(10);
                other.gameObject.SetActive(false);
                break;
            case "Gem":
                //GameManager.instance.AddScore(10);
                other.gameObject.SetActive(false);
                break;
            case "Red":
                if (hitCount >= 1 && hitCount <= 3)
                {
                    GameManager.instance.increseHeart();
                    hitCount--;
                }
                other.gameObject.SetActive(false);
                potionimages[0].SetActive(true);
                break;
            case "Blue":
                other.gameObject.SetActive(false);
                potionimages[1].SetActive(true);
                break;
            case "Purple":
                other.gameObject.SetActive(false);
                potionimages[2].SetActive(true);
                break;
            case "Green":
                other.gameObject.SetActive(false);
                potionimages[3].SetActive(true);
                transform.localScale = new Vector3(10f, 10f, 10f);
                isInvincible = true;
                StartCoroutine(Invincible());
                break;
            case "White":
                other.gameObject.SetActive(false);
                potionimages[4].SetActive(true);
                break;
        }
        PotionCheck();
    }
    private void PotionCheck()
    {
        for (int i = 0; i < potionimages.Length; i++)
        {
            if (potionimages[i].activeSelf == false) return;
        }

        Event();

        for (int i = 0; i < potionimages.Length; i++)
        {
            potionimages[i].SetActive(false);
        }
    }

    private void Event()
    {
        Debug.Log("포션 전체 획득완료!");
    }

    IEnumerator Invincible()
    {
        int countTime = 0;
        yield return new WaitForSeconds(4.0f);
        while (countTime < 10)
        {
            if (countTime % 2 == 0)
                spriteRenderer.color = new Color32(255, 255, 255, 90);
            else
                spriteRenderer.color = new Color32(255, 255, 255, 180);

            yield return new WaitForSeconds(0.2f);
            countTime++;
        }
        spriteRenderer.color = new Color32(255, 255, 255, 255);
        yield return null;
        transform.localScale = new Vector3(5f, 5f, 5f);
        isInvincible = false;
    }

    IEnumerator Hit()
    {
        int countTime = 0;
        while (countTime < 10)
        {
            if (countTime % 2 == 0)
                spriteRenderer.color = new Color32(255, 255, 255, 90);
            else
                spriteRenderer.color = new Color32(255, 255, 255, 180);

            yield return new WaitForSeconds(0.2f);
            countTime++;
        }
        spriteRenderer.color = new Color32(255, 255, 255, 255);
        yield return null;
        isInvincible = false;
    }
}
