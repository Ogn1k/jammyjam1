using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class PersistentMusicPlayer : MonoBehaviour
{
    [Header("Audio Settings")]
    [SerializeField] private AudioMixerGroup _musicMixerGroup;
    [SerializeField] private AudioClip[] _tracks;
    [SerializeField][Range(1f, 10f)] private float _fadeDuration = 3f;

    private List<AudioClip> _playlist = new List<AudioClip>();
    private AudioSource _audioSource;
    private int _currentTrackIndex = -1;
    private Coroutine _fadeCoroutine;

    // Singleton pattern
    private static PersistentMusicPlayer _instance;

    void Awake()
    {
        // Проверка существования экземпляра
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(gameObject);

        InitializeAudioSource();
        ShufflePlaylist();
        Application.focusChanged += OnApplicationFocus;

        // Подписка на событие смены сцены
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void Start()
    {
        if (!_audioSource.isPlaying)
        {
            StartCoroutine(PlayFirstTrackWithFade());
        }
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        Application.focusChanged -= OnApplicationFocus;
    }

    private void InitializeAudioSource()
    {
        if (_audioSource == null)
        {
            _audioSource = gameObject.AddComponent<AudioSource>();
            _audioSource.playOnAwake = false;
            _audioSource.loop = false;
            _audioSource.outputAudioMixerGroup = _musicMixerGroup;
            _audioSource.volume = 0f;
            _audioSource.ignoreListenerPause = true;
        }
    }

    private void ShufflePlaylist()
    {
        _playlist = new List<AudioClip>(_tracks);

        // Алгоритм Фишера-Йетса для перемешивания
        for (int i = _playlist.Count - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            AudioClip temp = _playlist[i];
            _playlist[i] = _playlist[randomIndex];
            _playlist[randomIndex] = temp;
        }
    }

    private IEnumerator PlayFirstTrackWithFade()
    {
        if (_playlist.Count == 0) yield break;

        _currentTrackIndex = 0;
        _audioSource.clip = _playlist[_currentTrackIndex];
        _audioSource.Play();

        yield return StartCoroutine(FadeVolume(0f, 1f, _fadeDuration));
        StartCoroutine(TrackEndCheck());
    }

    private IEnumerator TrackEndCheck()
    {
        while (true)
        {
            if (_audioSource.isPlaying)
            {
                float timeLeft = _audioSource.clip.length - _audioSource.time - _fadeDuration;
                if (timeLeft <= 0)
                {
                    yield return StartCoroutine(PlayNextTrackWithFade());
                }
            }
            yield return new WaitForSeconds(0.1f);
        }
    }

    private IEnumerator PlayNextTrackWithFade()
    {
        yield return StartCoroutine(FadeVolume(1f, 0f, _fadeDuration));

        _currentTrackIndex = (_currentTrackIndex + 1) % _playlist.Count;
        _audioSource.Stop();
        _audioSource.clip = _playlist[_currentTrackIndex];
        _audioSource.Play();

        yield return StartCoroutine(FadeVolume(0f, 1f, _fadeDuration));
    }

    private IEnumerator FadeVolume(float startVolume, float endVolume, float duration)
    {
        float timer = 0f;
        while (timer <= duration)
        {
            _audioSource.volume = Mathf.Lerp(startVolume, endVolume, timer / duration);
            timer += Time.deltaTime;
            yield return null;
        }
        _audioSource.volume = endVolume;
    }

    private void OnApplicationFocus(bool hasFocus)
    {
        AudioListener.pause = !hasFocus;
        if (hasFocus && !_audioSource.isPlaying)
        {
            _audioSource.Play();
        }
    }

    // Обработчик события загрузки новой сцены
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Дополнительные действия при смене сцены
    }

    // Для ручного переключения сцен (пример)
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}