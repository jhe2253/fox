using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public AudioClip deathClip; // ��� �� ����� ����� Ŭ��
    public float jumpForce = 700f; // ���� ��
    public int health = 3;
    public GameObject[] hearts;


    private int jumpCount = 0; // ���� ���� Ƚ��
    private bool isGrounded = false; // �ٴڿ� ��Ҵ��� ��Ÿ��
    private bool isDead = false; // ��� ����

    private Rigidbody2D playerRigidbody; // ����� ������ٵ� ������Ʈ
    private Animator animator; // ����� �ִϸ����� ������Ʈ
    private AudioSource playerAudio; // ����� ����� �ҽ� ������Ʈ

    
    
    // Start is called before the first frame update
    void Start()
    {
        // �ʱ�ȭ
        // ���� ������Ʈ�κ��� ����� ������Ʈ���� ������ ������ �Ҵ�
        playerRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        //����� �Է��� �����ϰ� ������ ó��
        if (isDead)
        {
            // ��� �� ó���� �� �̻� �������� �ʰ� ����
            return;
        }
        // ���콺 ���� ��ư�� �������� && �ִ� ���� Ƚ��(2)�� �������� �ʾҴٸ�
        if (Input.GetMouseButtonDown(0) && jumpCount < 2)
        {
            //���� Ƚ�� ����
            jumpCount++;
            // ���� ������ �ӵ��� ����������(0,0)�� ����
            playerRigidbody.velocity = Vector2.zero;
            // ������ٵ� �������� �� �ֱ�
            playerRigidbody.AddForce(new Vector2(0, jumpForce));
            // ����� �ҽ� ���
            playerAudio.Play();
        }
        else if (Input.GetMouseButtonUp(0) && playerRigidbody.velocity.y > 0)
        {
            //���콺 ���� ��ư���� ���� ���� ���� && �ӵ��� y���� ������(���� ��� ��)
            // ���� �ӵ��� �������� ����
            playerRigidbody.velocity = playerRigidbody.velocity * 0.5f;
        }

        // �ִϸ������� Grounded �ĸ����͸� isGrounded ������ ����
        animator.SetBool("Grounded", isGrounded);
    }

    private void Die()
    {
        //���ó��
        // �ִϸ������� Die Ʈ��Ŀ �Ķ���͸� ��
        animator.SetTrigger("Die");
        // ����� �ҽ��� Ȱ��� ����� Ŭ���� deathClip���� ����
        playerAudio.clip = deathClip;
        // ��� ȿ���� ���
        playerAudio.Play();

        // �ӵ��� ����(0,0)�� ����
        playerRigidbody.velocity = Vector2.zero;
        // ��� ���¸� true�� ����
        isDead = true;

        // ���� �Ŵ����� ���ӿ��� ó�� ����
        GameManager.instance.OnPlayerDead();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Ʈ���� �ݶ��̴��� ���� ��ֹ����� �浹�� ����
        if(other.tag == "Dead" && !isDead)
        {
            //�浹�� ���� �±װ� Dead �̸� ���� ������� �ʾҴٸ� Die()����
            Die();
        }
        if (other.tag == "obstacle"&&!isDead)
        {
            if (health >= 1)
            {
                health -= 1;
                hearts[health].SetActive(false);
                if(health <= 0)
                {
                    Die();
                }
            }


        }
        if(other.tag == "coin")
        {
            other.gameObject.SetActive(false);
        }


    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        // �ٴڿ� ������� �����ϴ� ó��
        // � �ݶ��̴��� �������, �浹 ǥ���� ������ ���� ������
        if (collision.contacts[0].normal.y > 0.7f)
        {
            // isGrounded�� true�� �����ϰ�, ���� ���� Ƚ���� 0���� ����
            isGrounded = true;
            jumpCount = 0;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // �ٴڿ��� ������� �����ϴ� ó��
        // � �ݶ��̴����� ������ ��� isGrounded�� false�� ����
        isGrounded = false;
    }
}
