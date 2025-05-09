using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioMixer audioMixer;
    public AudioSource musicSource;
    public AudioSource effectsSource;

    public AudioClip mainMenuMusic;
    public AudioClip levelMusic;
    public AudioClip clickSound;

    private string currentMusic = "";

    void Awake()
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

    void Start()
    {
        PlayMainMenuMusic();
    }

    public void PlayMainMenuMusic()
    {
        PlayMusic(mainMenuMusic);
    }

    public void PlayLevelMusic()
    {
        PlayMusic(levelMusic);
    }

    private void PlayMusic(AudioClip musicClip)
    {
        if (musicSource == null || musicClip == null)
            return;

        if (currentMusic == musicClip.name)
            return;

        musicSource.clip = musicClip;
        musicSource.Play();
        currentMusic = musicClip.name;
    }

    public void PlaySoundEffect(AudioClip soundClip)
    {
        if (effectsSource != null && soundClip != null)
        {
            effectsSource.PlayOneShot(soundClip);
        }
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "MainMenu")
        {
            PlayMainMenuMusic();
        }
        else if (scene.name.StartsWith("level"))
        {
            PlayLevelMusic();
        }
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
