using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusicController : MonoBehaviour
{
    public static BackgroundMusicController Instance { get; set; }
    private AudioSource audioSourceWorld;
    private AudioSource audioSourceHive;
    public bool isWave;

    [SerializeField] private AudioClip world1;
    [SerializeField] private AudioClip world2;
    [SerializeField] private AudioClip hive1;
    [SerializeField] private AudioClip hive2;    
    [SerializeField] private AudioClip wave1;
    [SerializeField] private AudioClip wave2;

    void Awake()
    {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
        } else {
            Instance = this;
        }
    }

    void Start()
    {
        audioSourceWorld = gameObject.AddComponent<AudioSource>();
        audioSourceHive = gameObject.AddComponent<AudioSource>();
        isWave = false;

        //PlayWorldMusic();
        SetClip(audioSourceWorld, world1);
        SetClip(audioSourceHive, hive1);
        audioSourceWorld.Play();
    }
    
    public void PlayMusic(AudioSource audioSource)
    {
        if (!audioSource.isPlaying)
        {
        audioSource.Play();
        }
    }

    // USE THIS
    public void SwapSource(AudioSource oldSource, AudioSource newSource)
    {
        StopAllCoroutines();
        StartCoroutine(FadeSources(oldSource, newSource));
    }

    // USE THIS
    public void DipClip(AudioSource source, AudioClip newClip)
    {
        StopAllCoroutines();
        StartCoroutine(DipFadeClip(source, newClip));        
    }

    public void SetClip(AudioSource audioSource, AudioClip newClip)
    {
        audioSource.clip =  newClip;
    }

    // DONT USE THIS, THIS MUST BE CALLED THRU SwapSource METHOD ABOVE
    private IEnumerator FadeSources(AudioSource oldSource, AudioSource newSource)
    {
        float timeToFade = 1.0f;
        float timeElapsed = 0;
        
        while (timeElapsed < timeToFade)
        {
            newSource.volume = Mathf.Lerp(0, 1, timeElapsed / timeToFade);
            oldSource.volume = Mathf.Lerp(1, 0, timeElapsed / timeToFade);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        oldSource.Stop();
    }

    // DONT USE THIS, THIS MUST BE CALLED THRU DipClip METHOD ABOVE
    private IEnumerator DipFadeClip(AudioSource source, AudioClip newClip)
    {
        Debug.Log("dip fade called");
        float timeToFade = 2.0f;
        float timeElapsed = 0;
        
        while (timeElapsed < timeToFade)
        {    
            source.volume = Mathf.Lerp(1, 0, timeElapsed / timeToFade);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        timeElapsed = 0;
        source.Stop();
        SetClip(source, newClip);
        source.Play();
        while (timeElapsed < timeToFade)
        {    
            source.volume = Mathf.Lerp(0, 1, timeElapsed / timeToFade);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
    }

    public void PlayMusicFromTime(AudioSource audioSource, float sec)
    {
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
            audioSource.time = sec;
        }
    }

    public void HiveToWorld()
    {
        if (isWave == false)
        {
            SetClip(audioSourceWorld, world1);
            PlayMusicFromTime(audioSourceWorld, world1.length * .1f);
            SwapSource(audioSourceHive, audioSourceWorld);
        }         
    }

    public void WorldToHive()
    {
        if (isWave == false)
        {
            SetClip(audioSourceHive, hive1);
            PlayMusicFromTime(audioSourceHive, hive1.length * 0.2f);
            SwapSource(audioSourceWorld, audioSourceHive);        
        }
    }

    public void WaveMusicOn()
    {        
        // SetClip(audioSourceHive, wave1);
        isWave = true;
        if (ScreenManager.Instance.inHive == true)
        {
            SetClip(audioSourceWorld, wave1);
            audioSourceWorld.Play();
            SwapSource(audioSourceHive, audioSourceWorld);
        }
        else
        {
            SetClip(audioSourceHive, wave1);
            audioSourceHive.Play();
            SwapSource(audioSourceWorld, audioSourceHive);            
        }
        // audioSourceHive.Stop();
        
    }

    public void WaveMusicOff()
    {
        isWave = false;
        if (audioSourceHive.isPlaying == true)
        {
            
            if (ScreenManager.Instance.inHive == true)
            {
                // Debug.Log("case 1");
                DipClip(audioSourceHive, hive1);
                // SetClip(audioSourceWorld, hive1);
                // audioSourceWorld.Play();
                // SwapSource(audioSourceHive, audioSourceWorld);
                // SetClip(audioSourceHive, hive1);
                // WorldToHive();
            }
            else
            {
                // Debug.Log("case 2");
                HiveToWorld();
            }   
        }
        else
        {
            if (ScreenManager.Instance.inHive == false)
            {
                // Debug.Log("case 3");
                DipClip(audioSourceWorld, world1);
            }
            else
            {
                // Debug.Log("case 4");
                WorldToHive();
            }            
        }

        // if (ScreenManager.Instance.inHive == true)
        // {
        //     SetClip(audioSourceHive, wave1);
        //     HiveToWorld();
        // }
        // else
        // {
        //     SetClip(audioSourceWorld, wave1);
        //     WorldToHive();
        // }
    }

}