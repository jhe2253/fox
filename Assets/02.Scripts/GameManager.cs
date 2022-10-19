using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static GameManager instance; // 싱글턴을 할당할 전역 변수

    public bool isGameover = false; // 게임오버 상태
    public Text scoreText; // 점수를 출력할 UI 텍스트
    public Text coinsText;

    public GameObject gameoverUi; // 게임오버 시 활성화할 UI 게임 오브젝트

    private int score = 0; // 게임 점수
    private int coin = 0;


    // 게임 시작과 동시에 싱글턴을 구성
    private void Awake()
    {
        // 싱글턴 변수 instance가 비어 있는가?
        if(instance == null)
        {
            // instance가 비어 있다면(null) 그곳에 자기 자신을 할당
            instance = this;
        }
        else
        {
            // instance에 이미 다른 GameManager 오브젝트가 할당되어 있는 경우

            // 씬에 두개 이상의 GameManger 오브젝트가 존제한다는 의미
            // 싱글턴 오브젝트는 하나만 존재해야 하므로 자신의 게임 오브젝트를 파괴
            Debug.LogWarning("씬에 두개 이상의 게임 매너저가 존재합니다!");
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // 게임오버 상태에서 게임을 재시작할 수 있게 하는 처리
        if(isGameover && Input.GetMouseButtonDown(0))
        {
            // 게임오버 상태에서 마우스 왼족 버튼을 클릭하면 현재 씬 재시작
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
    // 점수를 증가시키는 메서드
    public void AddScore(int newScore)
    {
        if (!isGameover)
        {
            // 점수를 증가
            score += newScore;
            scoreText.text = "Score : " + score;
        }
    }

    public void coi(int newcoins)
    {
        if (!isGameover)
        {
            coin += newcoins;
            coinsText.text = "" + coin;
        }
    }

    // 플레이어 캐릭터 사밍시 게임오버를 실행하는 메서드
    public void OnPlayerDead()
    {
        isGameover = true;
        gameoverUi.SetActive(true);
    }
}
