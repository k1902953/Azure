using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public int score;
    public int level;
    public int coinPoints = 10;
    public static GameManager inst;
    public static bool gameover;
    public static bool gamewon;
    public static bool gameIsPaused;
    public GameObject heart1, heart2, heart3;
    public static int health;
    public GameObject pausePanel;
    public GameObject gameOverPanel;
    public GameObject gameWonPanel;
    public TMPro.TMP_Text scoreTxt;
    public TMPro.TMP_Text pointTxt;
    public TMPro.TMP_Text levelTxt;
    public TMPro.TMP_Text timerTxt;
    private float startTimer;
    public static bool finishTimer;
    public static int minutes;
    int newRating;
    public static string[] prefsRatings = new string[2];
    

    public void incScore()
    {
        score += coinPoints;
        scoreTxt.text = ""+score;
    }

    void Awake()
    {
        prefsRatings[0] = "rating1";
        prefsRatings[1] = "rating2";
    }

    void Start()
    {
        if (SceneManager.GetActiveScene().name == "Endless")
        {
            health = 3;
            heart1.gameObject.SetActive(true);
            heart2.gameObject.SetActive(true);
            heart3.gameObject.SetActive(true);
            startTimer = Time.time;
        }
        level = SceneManager.GetActiveScene().buildIndex - 1;
        inst = this;
        gameIsPaused = false;
        gameover = false;
        gamewon = false;
        finishTimer = false;
        if (level <= 2)
        {
            levelTxt.text = "" + level;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "Endless")
        {
            switch (health)
            {
                case 3:
                    heart1.gameObject.SetActive(true);
                    heart2.gameObject.SetActive(true);
                    heart3.gameObject.SetActive(true);
                    break;
                case 2:
                    heart1.gameObject.SetActive(true);
                    heart2.gameObject.SetActive(true);
                    heart3.gameObject.SetActive(false);
                    break;
                case 1:
                    heart1.gameObject.SetActive(true);
                    heart2.gameObject.SetActive(false);
                    heart3.gameObject.SetActive(false);
                    break;
                case 0:
                    heart1.gameObject.SetActive(false);
                    heart2.gameObject.SetActive(false);
                    heart3.gameObject.SetActive(false);
                    break;
            }
            if (finishTimer)
            {
                return;
            }
            float t = Time.time - startTimer;
            minutes = ((int)t / 60);
            string seconds = (t % 60).ToString("f2");

            timerTxt.text = minutes + ":" + seconds;

        }
    }

    public void PauseGame()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    public void ResumeGame()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }

    public void GameWon()
    {
        if (gamewon)
        {

            if (SceneManager.GetActiveScene().name == "Endless")
            {
                finishTimer = true;
                timerTxt.color = Color.yellow;
            }
            pointTxt.text = "" + score;
            //PlayerPrefs.SetInt("coinsCollected", (PlayerPrefs.GetInt("coinsCollected") + score));
            GetComponent<Stars>().StarsAwarded(score);
            gameWonPanel.SetActive(true);

            if (Mainmenu.nextScene > PlayerPrefs.GetInt("levelAt"))
            {
                PlayerPrefs.SetInt("levelAt", Mainmenu.nextScene);
            }

            

            newRating = Stars.rating;

            switch (level)
            {
                case 2:
                    if (newRating > PlayerPrefs.GetInt(prefsRatings[1]))
                    {
                        PlayerPrefs.SetInt(prefsRatings[1], newRating);
                    }
                    break;
                case 1:
                    if (newRating > PlayerPrefs.GetInt(prefsRatings[0]))
                    {
                        PlayerPrefs.SetInt(prefsRatings[0], newRating);
                    }
                    break;
            }
        }
    }

    public void GameOver()
    {
        if (gameover)
        {
            gameOverPanel.SetActive(true);
        }

    }
}
