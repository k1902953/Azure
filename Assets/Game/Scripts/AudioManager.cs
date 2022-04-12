using UnityEngine.Audio;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;


public class AudioManager : MonoBehaviour
{
    public AudioSource Audio;
    //public AudioClip levelAudioClip;
    public GameObject switchon;
    public GameObject switchon2;
    public static AudioManager instance;
    public AudioClip[] sounds;
    private GameObject temp;
    public GameObject musicOnBtn;
    public GameObject musicOffBtn;
    public GameObject soundOnBtn;
    public GameObject soundOffBtn;

    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("MainCamera");
        temp = GameObject.Find("Main Camera");
        if (objs.Length > 1)
        {
            Destroy(Audio.gameObject);
        }
        DontDestroyOnLoad(Audio.gameObject);

        if (PlayerPrefs.GetInt("music") == 0)
        {
            if (SceneManager.GetActiveScene().buildIndex == 0)
            {
                musicOnBtn.gameObject.SetActive(true);
                musicOffBtn.gameObject.SetActive(false);
            }
            Audio.Play();
        }
        else
        {
            if (SceneManager.GetActiveScene().buildIndex == 0)
            {
                musicOffBtn.gameObject.SetActive(true);
                musicOnBtn.gameObject.SetActive(false);
            }
            Audio.Pause();
        }
        
           
        if (PlayerPrefs.GetInt("sound") == 0)
        {
            if (SceneManager.GetActiveScene().buildIndex == 0)
            {
                soundOnBtn.gameObject.SetActive(true);
                soundOffBtn.gameObject.SetActive(false);
            }
        }
        else
        {
            if (SceneManager.GetActiveScene().buildIndex == 0)
            {
                soundOffBtn.gameObject.SetActive(true);
                soundOnBtn.gameObject.SetActive(false);
            }
        }
        
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
        }
        else
        {
            Audio.clip = sounds[1];
        }

        if (PlayerPrefs.GetInt("music") == 0)
        {
            Audio.Play();
        }


    }

    public void Music()
    {
        
        Audio = temp.GetComponent<AudioSource>();
        if (switchon.activeSelf)
        {
            Audio.Play();
            PlayerPrefs.SetInt("music", 0);
        }
        else
        {
            Audio.Pause();
            PlayerPrefs.SetInt("music", 1);
        }
    }

    public void SoundFX()
    {
        if (switchon2.activeSelf)
        {
            PlayerPrefs.SetInt("sound", 0);
        }
        else
        {
            PlayerPrefs.SetInt("sound", 1);
        }
    }
}
