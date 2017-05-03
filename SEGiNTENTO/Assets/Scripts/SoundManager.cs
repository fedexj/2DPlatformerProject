using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour
{
    public AudioSource FXSource;  
    public AudioSource musicSource;
    public AudioSource FXWalk;
    public static SoundManager instance = null;

    public float lowPitchRange = .95f;
    public float hiPitchRange = 1.05f;

    void Awake()
    {
        if (instance==null)
        {
            instance = this;
        }
        else if (instance!=this)
        { Destroy(gameObject); }

        DontDestroyOnLoad(gameObject);
    }

    public void playSingle(AudioClip Clip)
    {
        FXSource.clip = Clip;
        FXSource.Play();
    }

    public void playWalk(AudioClip Clip)
    {
        FXWalk.clip = Clip;
        FXWalk.Play();
    }
}
