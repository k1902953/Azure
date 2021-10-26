using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Mainmenu : MonoBehaviour
{
    public AudioSource Audio;
    public GameObject switchon;
    public GameObject loadingScreen;
    public Slider slider;
    public TextMeshProUGUI progressTxt;

    private void Start()
    {
        //Audio = GetComponent<AudioSource>();
    }
  
    public void PlayGame()
    {
        StartCoroutine(LoadAsynchronously());
    }

    IEnumerator LoadAsynchronously()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync("level1");
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
        SceneManager.LoadScene("mainmenu");
    }

    public void Music()
    {
        if (switchon.activeSelf)
        {
            Audio.Play();
            Debug.Log("ture");
        }
        else
        {
            Audio.Pause();
            Debug.Log("false");
        }
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }
}
