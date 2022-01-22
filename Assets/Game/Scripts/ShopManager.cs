using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public int[,] shopItems = new int[5,5];
    private int coins;
    public TMPro.TMP_Text CointTxt;

    void Start()
    {
        coins = PlayerPrefs.GetInt("coinsCollected");
        CointTxt.text = "" + coins;

        //ID
        shopItems[1, 1] = 1;
        shopItems[1, 2] = 2;
        shopItems[1, 3] = 3;
        shopItems[1, 4] = 4;

        //Price
        shopItems[2, 1] = 10;
        shopItems[2, 2] = 20;
        shopItems[2, 3] = 30;
        shopItems[2, 4] = 40;

        //Quantity
        shopItems[3, 1] = 0;
        shopItems[3, 2] = 0;
        shopItems[3, 3] = 0;
        shopItems[3, 4] = 0;
    }

    // Update is called once per frame
    public void Buy()
    {
        GameObject ButtonRef = GameObject.FindGameObjectWithTag("Event").GetComponent<EventSystem>().currentSelectedGameObject;

        if(coins >= shopItems[2, ButtonRef.GetComponent<ItemInfo>().ItemID])
        {
            PlayerPrefs.SetInt("coinsCollected", (PlayerPrefs.GetInt("coinsCollected") - shopItems[2, ButtonRef.GetComponent<ItemInfo>().ItemID]));
            shopItems[3, ButtonRef.GetComponent<ItemInfo>().ItemID]++;
            CointTxt.text = "" + PlayerPrefs.GetInt("coinsCollected");
            ButtonRef.GetComponent<ItemInfo>().QuantityTxt.text = shopItems[3, ButtonRef.GetComponent<ItemInfo>().ItemID].ToString();
        }

        
    }
}
