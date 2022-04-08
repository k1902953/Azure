using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Mainmenu : MonoBehaviour
{
    public Button storeBtn;
    public GameObject loadingScreen;
    public Slider slider;
    public TextMeshProUGUI progressTxt;
    public int level;
    public static int nextScene;
    int newRating;
    public GameObject[] stars;
    private Image imgComp;
    public Sprite wonStar;
    public Sprite noStar;

    private void Start()
    {

        nextScene = SceneManager.GetActiveScene().buildIndex + 1;
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            if (PlayerPrefs.GetInt("levelAt") > 2)
            {
                storeBtn.interactable = true;
            }
            else
            {
                storeBtn.interactable = false;
            }
        }else if(SceneManager.GetActiveScene().buildIndex == 1)
        {
            StarsDisplayed("rating1", 0, 3);
            StarsDisplayed("rating2", 3, 6);
            StarsDisplayed("rating3", 6, 9);
        }

        for (int i = 0; i < GameManager.prefsRatings.Length; i++)
        {
            PlayerPrefs.GetInt(GameManager.prefsRatings[i]);
        }
        
    }

    public void PlayGame(int index)
    {
        
        StartCoroutine(LoadAsynchronously("level" + index));
        
    }

    public void LevelSelection()
    {
        //PlayerData data = SaveSystem.LoadPlayer();

        //StartCoroutine(LoadAsynchronously("LevelSelection"));
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);

    }

    public void StarsDisplayed(string rating, int start, int end)
    {
        switch (PlayerPrefs.GetInt(rating))
        {
            case 3:
                for (int i = start; i < end; i++)
                {
                    imgComp = stars[i].GetComponent<Image>();
                    imgComp.sprite = wonStar;
                }
                break;
            case 2:

                for (int i = start; i < end; i++)
                {
                    if(i == 2 || i == 5)
                    {
                        imgComp = stars[i].GetComponent<Image>();
                        imgComp.sprite = noStar;
                        break;
                    }
                    imgComp = stars[i].GetComponent<Image>();
                    imgComp.sprite = wonStar;
                }
                break;
            case 1:
                for (int i = start; i < end; i++)
                {
                    if (i == 0 || i == 3)
                    {
                        imgComp = stars[i].GetComponent<Image>();
                        imgComp.sprite = wonStar;
                        break;
                    }
                    imgComp = stars[i].GetComponent<Image>();
                    imgComp.sprite = noStar;
                }
                break;
            case 0:
                for (int i = start; i < end; i++)
                {
                    imgComp = stars[i].GetComponent<Image>();
                    imgComp.sprite = noStar;
                }
                break;
        }
    }

    public void EndlessGame()
    {
        StartCoroutine(LoadAsynchronously("Endless"));
    }

    IEnumerator LoadAsynchronously(string level)
    {
        //Audio.Pause();
        AsyncOperation operation = SceneManager.LoadSceneAsync(level);
        
        loadingScreen.SetActive(true);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            slider.value = progress;
            progressTxt.text = progress * 100f + "%";
            yield return null;
        }
    }

    public void MainMenu()
    {
        StartCoroutine(LoadAsynchronously("mainmenu"));
        Time.timeScale = 1f;
    }
    
    public void Replay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Next()
    {
        if (SceneManager.GetActiveScene().buildIndex == 3)
        {
            
            //StartCoroutine(LoadAsynchronously("level2"));
            PlayGame(nextScene-1);
        }
        //SceneManager.LoadScene(nextScene);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }
}
