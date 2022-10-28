using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; // �̱����� �Ҵ��� ���� ����

    public bool isGameover = false; // ���ӿ��� ����
    public GameObject gameoverUI;
    // ���� ���� �� Ȱ��ȭ�� UI���� ������Ʈ
    public int score = 0; // ���� ����
    public TextMeshProUGUI scoreText; // ������ ����� UI �ؽ�Ʈ
    public GameObject[] heartimages; // ��Ʈ �̹��� �迭
    private int currentIndex = 0; // ��Ʈ �̹��� ���� ����
    //****//****//****//****//****//****
    public GameObject menu_Panel;
    public GameObject start_Count_Panel;

    public TextMeshProUGUI count_text;

    bool isStart = false;

    float count_start;

    void Awake()
    {
        // �̱��� ���� instance�� ��� �ִ°�?
        if (instance == null)
        {
            // instance�� ����ִٸ�(null) �װ��� �ڱ� �ڽ��� �Ҵ�
            instance = this;
        }
        else
        {
            // instance�� �̹� �ٸ� GameManager ������Ʈ�� �Ҵ�Ǿ� �ִ� ���
            // �ڽ��� ���� ������Ʈ�� �ı�
            Debug.LogWarning("���� �� �� �̻��� ���� �Ŵ����� �����մϴ�!");
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameover && Input.GetMouseButtonDown(0))
        {
            // ���ӿ��� ���¿��� ���콺 ���� ��ư�� Ŭ���ϸ�
            // ���� �� �����
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if (isStart)
        {
            start_Count_Panel.SetActive(true);
            count_start -= Time.unscaledDeltaTime;
            StartCoroutine("Start_Timer");
            count_text.text = Mathf.Floor(count_start).ToString();
        }
    }


    public void MenuOpen()
    {
        menu_Panel.SetActive(true);
        Time.timeScale = 0;
        count_start = 4f;
    }

    public void MenuClose()
    {
        menu_Panel.SetActive(false);
        isStart = true;
    }

    public void Restart()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

    public void Exit_MainMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    IEnumerator Start_Timer()
    {
        yield return new WaitForSecondsRealtime(3f);
        isStart = false;
        start_Count_Panel.SetActive(false);
        Time.timeScale = 1;
    }

    // ������ ���� ��Ű�� �޼���
    public void AddScore(int newScore)
    {
        // ���ӿ����� �ƴ϶��
        if (!isGameover)
        {
            // ������ ����
            score += newScore;
            scoreText.text = "Score : " + score;
        }
    }

    // �÷��̾� ĳ���� ��� �� ���ӿ����� �����ϴ� �޼���
    public void OnPlayerDead()
    {
        isGameover = true;
        gameoverUI.SetActive(true);
    }

    // ����� ���̴� ȿ���� ������ �޼���
    public void reduceHeart()
    {
        heartimages[currentIndex].SetActive(false);
        currentIndex++;
    }

    // ����� �ø��� ȿ���� ������ �޼���
    public void increseHeart()
    {
        currentIndex--;
        heartimages[currentIndex].SetActive(true);
    }
}
