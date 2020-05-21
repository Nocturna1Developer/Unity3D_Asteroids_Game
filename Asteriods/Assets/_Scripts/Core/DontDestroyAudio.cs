using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyAudio : MonoBehaviour
{
    private static DontDestroyAudio instance;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        if (!instance.GetComponent<AudioSource>().isPlaying)
            GetComponent<AudioSource>().Play();
    }

    public void turnMusicOff()
    {
        if (instance != null)
        {
            //if (instance.audio.isPlaying)
            //    instance.audio.Stop();
            Destroy(this.gameObject);
            instance = null;
        }
    }

    void OnApplicationQuit()
    {
        instance = null;
    }
}