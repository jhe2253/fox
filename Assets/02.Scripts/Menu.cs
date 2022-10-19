using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject menuPanel;
    public GameObject Play;

    public void step()
    {
        Time.timeScale = 0; //게임 일시정지
        menuPanel.SetActive(true);

    }
    public void go()
    {

        Time.timeScale = 1;
        menuPanel.SetActive(false);
    }

    public void Re()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
    }
    public void Quit()
    {
        Application.Quit();
    }
}
