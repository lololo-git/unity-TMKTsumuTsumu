using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    //BGM
    [SerializeField] private AudioSource bgmSource = default;

    [SerializeField] private AudioClip[] bgmClips = default;

    //SE
    [SerializeField] private AudioSource seSource = default;

    [SerializeField] private AudioClip[] seClips = default;

    public enum BGM
    {
        Title,
        Main,
    }

    public enum SE
    {
        Touch,
        Destroy,
    }

    // Singleton
    public static SoundManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayBGM(BGM bgm)
    {
        bgmSource.clip = bgmClips[(int)bgm];
        bgmSource.Play();
    }

    public void PlaySE(SE se)
    {
        seSource.PlayOneShot(seClips[(int)se]);
    }
}