using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectCoin : MonoBehaviour
{
    private AudioSource coinMP3;

    void Start()
    {
        GameObject coin  = GameObject.Find("/GameManager/coinCollect");
        coinMP3 = coin.GetComponent<AudioSource>();
    }
    void OnTriggerEnter(Collider collisionInfo)
    {
        //if (collisionInfo.gameObject.GetComponent<Spikes_0_0>() != null)
        //{
        //    Destroy(gameObject);
        //}

        if (collisionInfo.tag == "Player")
        {
            if (PlayerPrefs.GetInt("sound") == 0)
            {
                coinMP3.Play();
            }
            this.gameObject.SetActive(false);
            GameManager.inst.incScore();
            Destroy(gameObject);
        }
    }
}
