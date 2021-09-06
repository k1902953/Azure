using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public int score;
    public static GameManager inst;
    public static bool gameover;
    public GameObject gameOverPanel;
    public TMPro.TMP_Text scoreTxt;

    public void incScore()
    {
        score += 10;
        scoreTxt.text = ""+score;
    }

    void Start()
    {
        inst = this;
        gameover = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameOver()
    {
        if (gameover)
        {
            gameOverPanel.SetActive(true);
        }

    }
}
