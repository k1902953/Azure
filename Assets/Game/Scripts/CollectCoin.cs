using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectCoin : MonoBehaviour
{
    public AudioSource coinMP3;
    void OnTriggerEnter(Collider other)
    {
        coinMP3.Play();
        this.gameObject.SetActive(false);
        GameManager.inst.incScore();
        Destroy(gameObject);
    }
}
