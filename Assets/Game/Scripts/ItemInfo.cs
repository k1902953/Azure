using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemInfo : MonoBehaviour
{

    public int ItemID;
    public TMPro.TMP_Text PriceTxt;
    public TMPro.TMP_Text QuantityTxt;
    public GameObject ShopManager;

    void Update()
    {
        PriceTxt.text = "" + ShopManager.GetComponent<ShopManager>().shopItems[2, ItemID].ToString();
        QuantityTxt.text = "" + ShopManager.GetComponent<ShopManager>().shopItems[3, ItemID].ToString();
    }
}
