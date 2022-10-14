using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioMixer musicMixer;
    [SerializeField] private Slider slider;
    [SerializeField] private TextMeshProUGUI text;

    public static AudioManager instance;

    #region MONO
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        ChangeMusicVolume(PlayerPrefs.GetFloat("MusicVolume"));
    }
    #endregion

    public void ChangeMusicVolume(float vol)
    {
        musicMixer.SetFloat("MusicVolume", Mathf.Log10(vol) * 20);
        PlayerPrefs.SetFloat("MusicVolume", vol);
        UpdateMusicText();
    }

    public void UpdateMusicSlider()
    {
        musicMixer.GetFloat("MusicVolume", out float currentVolume);
        slider.value = Mathf.Pow(10, currentVolume / 20);
    }

    public void UpdateMusicText()
    {
        musicMixer.GetFloat("MusicVolume", out float currentVolume);
        text.text = "ÇÂÓÊ: " + Mathf.Round(Mathf.Pow(10, currentVolume / 20) * 100) + "%";
    }

}
