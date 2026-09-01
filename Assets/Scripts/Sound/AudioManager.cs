using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [SerializeField] private AudioSource bgmSource;
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioSource uiSource;

    [Header("Sounds")]

    public AudioClip bgm;
    public AudioClip StartAlter;
    public AudioClip AlterUIEnter;
    public AudioClip AlterUIClick;
    public AudioClip QuitAlter;
    public AudioClip UIEnter;
    public AudioClip UIClick;
    public AudioClip Operating;
    public AudioClip Clash;
    public AudioClip RevealSkill;
    public AudioClip BlockSound;
    public AudioClip ExcuteSkill;
    public AudioClip Victory;
    public AudioClip Defeat;
    public AudioClip ClashBreak;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        DontDestroyOnLoad(gameObject);
    }

    public void PlayBGM()
    {
        if (bgmSource.isPlaying) return;

        bgmSource.clip = bgm;
        bgmSource.loop = true;
        bgmSource.Play();
    }

    public void StopBGM()
    {
        bgmSource.Stop();
    }

    public void PlaySFX(AudioClip clip)
    {
        if (clip == null)
            return;

        sfxSource.PlayOneShot(clip);
    }

    public void PlayUI(AudioClip clip)
    {
        if (clip == null)
            return;

        uiSource.PlayOneShot(clip);
    }
}
