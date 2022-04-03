using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadCharacter : MonoBehaviour
{
    public GameObject[] characterPref;
    public Transform rezPoint;

    void Start()
    {
        int selectedCharacter = PlayerPrefs.GetInt("selectCharacter");
        GameObject prefab = characterPref[selectedCharacter];
        GameObject clone = Instantiate(prefab, rezPoint.position, Quaternion.identity);

    }

}
