using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DanceSelection : MonoBehaviour
{
    public TMPro.TMP_Text selectedTxt;
    public Button selectBtn;
    public Animator animator;
    public int selectedDance = 0;

    void Start()
    {
        selectedDance = PlayerPrefs.GetInt("selectDance");
        animator.SetTrigger("startAni");
        animator.SetInteger("danceSelection", selectedDance);
        
    }

    public void Next()
    {
        selectedDance = (selectedDance + 1) % 3;
        animator.SetTrigger("startAni");
        animator.SetInteger("danceSelection", selectedDance);

        if (PlayerPrefs.GetInt("selectDance") == selectedDance)
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
        selectedDance--;
        if (selectedDance < 0)
        {
            selectedDance += 3;
        }
        animator.SetTrigger("startAni");
        animator.SetInteger("danceSelection", selectedDance);

        if (PlayerPrefs.GetInt("selectDance") == selectedDance)
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
        PlayerPrefs.SetInt("selectDance", selectedDance);
        selectedTxt.text = "SELECTED";
        selectBtn.interactable = false;
    }

}
