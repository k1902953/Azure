using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterSelection : MonoBehaviour
{
    public GameObject[] characters;
    public int selectedCharacter;
    public TMPro.TMP_Text selectedTxt;
    public Button selectBtn;

    void Start()
    {
        characters[selectedCharacter].SetActive(false);
        selectedCharacter = PlayerPrefs.GetInt("selectCharacter");
        characters[selectedCharacter].SetActive(true);
    }

    public void Next()
    {
        characters[selectedCharacter].SetActive(false);
        selectedCharacter = (selectedCharacter + 1) % characters.Length;
        characters[selectedCharacter].SetActive(true);

        if(PlayerPrefs.GetInt("selectCharacter") == selectedCharacter)
        {
            selectedTxt.text = "SELECTED";
            selectBtn.interactable = false;
        }
        else
        {
            selectedTxt.text = "SELECT";
            selectBtn.interactable = true;
        }
    }

    public void Previous()
    {
        characters[selectedCharacter].SetActive(false);
        selectedCharacter--;
        if (selectedCharacter < 0)
        {
            selectedCharacter += characters.Length;
        }
        characters[selectedCharacter].SetActive(true);

        if (PlayerPrefs.GetInt("selectCharacter") == selectedCharacter)
        {
            selectedTxt.text = "SELECTED";
            selectBtn.interactable = false;
        }
        else
        {
            selectedTxt.text = "SELECT";
            selectBtn.interactable = true;
        }
    }

    public void Selected()
    {
        PlayerPrefs.SetInt("selectCharacter", selectedCharacter);
        selectedTxt.text = "SELECTED";
        selectBtn.interactable = false;
    }
}
