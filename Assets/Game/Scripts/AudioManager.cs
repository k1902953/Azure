using UnityEngine.Audio;
using UnityEngine;
using System;


public class AudioManager : MonoBehaviour
{
    public AudioSource Audio;
    //public AudioClip levelAudioClip;
    public GameObject switchon;
    public static AudioManager instance;
    public AudioClip[] sounds;
    private GameObject temp;

    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("MainCamera");
        temp = GameObject.Find("Main Camera");
        if (objs.Length > 1)
        {
            Destroy(Audio.gameObject);
        }
        DontDestroyOnLoad(Audio.gameObject);
    }

    void Update()
    {
        
    }

    public void Switch()
    {
        
        Audio = temp.GetComponent<AudioSource>();
        if (Audio.clip == sounds[1])
        {
            Audio.clip = sounds[0];
            Audio.Play();
        }
        else
        {
            Audio.clip = sounds[1];
            Audio.Play();
        }
        
    }

    public void Music()
    {
        
        Audio = temp.GetComponent<AudioSource>();
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
}
