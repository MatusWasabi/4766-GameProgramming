using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;


public class MusicAudioController : MonoBehaviour
{
    public static MusicAudioController instance { get; private set; }
    [SerializeField] private AudioSource backgroundSource;
    [SerializeField] private AudioClip mainMenuMusic;
    [SerializeField] private AudioClip levelMusic;
    [SerializeField] private int oldLevel;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            
        }
        else
        {
            oldLevel = SceneManager.GetActiveScene().buildIndex;
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void OnLevelWasLoaded(int level)
    {

        if (backgroundSource != null) { backgroundSource.DOKill(); }
        
        if (level == 0 && oldLevel != 0) 
        {
            backgroundSource.volume = 0;
            backgroundSource.DOFade(0.5f, 10).SetEase(Ease.InSine);
            backgroundSource.clip = mainMenuMusic;
            oldLevel = level;
        }
        else if (level == 1 && oldLevel == 0)
        {
            backgroundSource.volume = 0;
            backgroundSource.DOFade(0.5f, 10).SetEase(Ease.InSine);
            backgroundSource.clip = levelMusic;
            oldLevel = level;
        }
        if (backgroundSource.isActiveAndEnabled) { backgroundSource.Play(); }


    }
}
