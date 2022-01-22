using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class Stars : MonoBehaviour
{
    public GameObject[] stars;
    public Sprite wonStar;
    public Sprite noStar;
    private int coinsCount;
    public static int rating = 0;
    private Image imgComponent;
    public TMPro.TMP_Text bonusTxt;
    private int bonusPoints;

    // Start is called before the first frame update
    void Start()
    {
        coinsCount = GameObject.FindGameObjectsWithTag("Coin").Length;
    }
    
    public void StarsAwarded(int points)
    {
        if (SceneManager.GetActiveScene().name == "Endless")
        {
            
            if(GameManager.minutes == 1)
            {
                //one star awarded
                imgComponent = stars[0].GetComponent<Image>();
                imgComponent.sprite = wonStar;
                imgComponent = stars[1].GetComponent<Image>();
                imgComponent.sprite = noStar;
                imgComponent = stars[2].GetComponent<Image>();
                imgComponent.sprite = noStar;
                bonusPoints = 100;
            }
            else if (GameManager.minutes == 2)
            {
                //two star awarded
                imgComponent = stars[0].GetComponent<Image>();
                imgComponent.sprite = wonStar;
                imgComponent = stars[1].GetComponent<Image>();
                imgComponent.sprite = wonStar;
                imgComponent = stars[2].GetComponent<Image>();
                imgComponent.sprite = noStar;
                bonusPoints = 200;
            }
            else if (GameManager.minutes > 3 )
            {
                //three star awarded 
                imgComponent = stars[0].GetComponent<Image>();
                imgComponent.sprite = wonStar;
                imgComponent = stars[1].GetComponent<Image>();
                imgComponent.sprite = wonStar;
                imgComponent = stars[2].GetComponent<Image>();
                imgComponent.sprite = wonStar;
                if (points <= 150)
                {
                    bonusPoints = 300;
                }
                else
                {
                    //bonus will be guarantee 300 and above so its better that the 1 and 2 star bonus
                    bonusPoints = points * 2;
                }
                
            }
            else
            {
                imgComponent = stars[0].GetComponent<Image>();
                imgComponent.sprite = noStar;
                imgComponent = stars[1].GetComponent<Image>();
                imgComponent.sprite = noStar;
                imgComponent = stars[2].GetComponent<Image>();
                imgComponent.sprite = noStar;
                bonusPoints = 0;
            }
        }
        else //all levels that are not endless
        {
            int coinsLeft = GameObject.FindGameObjectsWithTag("Coin").Length;
            int coinsCollected = coinsCount - coinsLeft;
            float percent = float.Parse(coinsCollected.ToString()) / float.Parse(coinsCount.ToString()) * 100.0f;
            if (percent >= 30.0f && percent < 60.0f)
            {
                //one star awarded
                rating = 1;
                imgComponent = stars[0].GetComponent<Image>();
                imgComponent.sprite = wonStar;
                imgComponent = stars[1].GetComponent<Image>();
                imgComponent.sprite = noStar;
                imgComponent = stars[2].GetComponent<Image>();
                imgComponent.sprite = noStar;
                bonusPoints = 10;
            }
            else if (percent >= 60.0f && percent < 85.0f)
            {
                //two star awarded
                rating = 2;
                imgComponent = stars[0].GetComponent<Image>();
                imgComponent.sprite = wonStar;
                imgComponent = stars[1].GetComponent<Image>();
                imgComponent.sprite = wonStar;
                imgComponent = stars[2].GetComponent<Image>();
                imgComponent.sprite = noStar;
                bonusPoints = (points / 2 + 20);
            }
            else if (percent > 85.0f)
            {
                //three star awarded
                rating = 3;
                imgComponent = stars[0].GetComponent<Image>();
                imgComponent.sprite = wonStar;
                imgComponent = stars[1].GetComponent<Image>();
                imgComponent.sprite = wonStar;
                imgComponent = stars[2].GetComponent<Image>();
                imgComponent.sprite = wonStar;
                bonusPoints = points * 2;
            }
            else
            {
                rating = 0;
                imgComponent = stars[0].GetComponent<Image>();
                imgComponent.sprite = noStar;
                imgComponent = stars[1].GetComponent<Image>();
                imgComponent.sprite = noStar;
                imgComponent = stars[2].GetComponent<Image>();
                imgComponent.sprite = noStar;
                bonusPoints = 0;
            }
        }
        PlayerPrefs.SetInt("coinsCollected", (PlayerPrefs.GetInt("coinsCollected") + bonusPoints + points));
        bonusTxt.text = "" + bonusPoints;
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

