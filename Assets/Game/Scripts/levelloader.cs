using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class levelloader : MonoBehaviour
{
    public static Button[] LevelBtns = new Button[2];

    public TMPro.TMP_Text coinsCollectedTxt;

    void Start()
    {
        for (int i = 0; i < LevelBtns.Length; i++)
        {
            Button _btn = GameObject.Find("Level" + (i + 1)).GetComponent<Button>();
            LevelBtns[i] = _btn;
        }

        int levelAt = PlayerPrefs.GetInt("levelAt", 2);
        
        for (int i = 0; i < LevelBtns.Length; i++)
        {
            if (i+2 > levelAt)
            {
                LevelBtns[i].interactable = false;
            }
        }
        coinsCollectedTxt.text = "" + PlayerPrefs.GetInt("coinsCollected");
        
    }

    public void DeleteProgress()
    {
        PlayerPrefs.DeleteAll();
        Debug.Log("all prefs deleted.");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
