using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    AudioSource audioSource;

    private static MusicPlayer _instance;

    public static MusicPlayer Instance { get { return _instance; } }

    private void Awake()//singleton
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    void Start()
    {
        DontDestroyOnLoad(this);
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = PlayerPrefsController.GetMasterVolume();//ovo cuva kako uredin u optionsu
    }

    public void SetVolume(float volume)
    {
        audioSource.volume = volume;
    }
}
