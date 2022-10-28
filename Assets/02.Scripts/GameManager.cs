using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; // 싱글턴을 할당할 전역 변수

    public bool isGameover = false; // 게임오버 상태
    public GameObject gameoverUI;
    // 게임 오버 시 활성화할 UI게임 오브젝트
    public int score = 0; // 게임 점수
    public TextMeshProUGUI scoreText; // 점수를 출력할 UI 텍스트
    public GameObject[] heartimages; // 하트 이미지 배열
    private int currentIndex = 0; // 하트 이미지 현재 순서
    //****//****//****//****//****//****
    public GameObject menu_Panel;
    public GameObject start_Count_Panel;

    public TextMeshProUGUI count_text;

    bool isStart = false;

    float count_start;

    void Awake()
    {
        // 싱글턴 변수 instance가 비어 있는가?
        if (instance == null)
        {
            // instance가 비어있다면(null) 그곳에 자기 자신을 할당
            instance = this;
        }
        else
        {
            // instance에 이미 다른 GameManager 오브젝트가 할당되어 있는 경우
            // 자신의 게임 오브젝트를 파괴
            Debug.LogWarning("씬에 두 개 이상의 게임 매니저가 존재합니다!");
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameover && Input.GetMouseButtonDown(0))
        {
            // 게임오버 상태에서 마우스 왼쪽 버튼을 클릭하면
            // 현재 씬 재시작
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

    // 점수를 증가 시키는 메서드
    public void AddScore(int newScore)
    {
        // 게임오버가 아니라면
        if (!isGameover)
        {
            // 점수를 증가
            score += newScore;
            scoreText.text = "Score : " + score;
        }
    }

    // 플레이어 캐릭터 사망 시 게임오버를 실행하는 메서드
    public void OnPlayerDead()
    {
        isGameover = true;
        gameoverUI.SetActive(true);
    }

    // 생명력 줄이는 효과를 보여줄 메서드
    public void reduceHeart()
    {
        heartimages[currentIndex].SetActive(false);
        currentIndex++;
    }

    // 생명력 늘리는 효과를 보여줄 메서드
    public void increseHeart()
    {
        currentIndex--;
        heartimages[currentIndex].SetActive(true);
    }
}
