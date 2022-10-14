using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class MainMenu : MonoBehaviour
{
    [SerializeField] AudioMixer musicMixer;
    [SerializeField] Slider slider;
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] GameObject retryPanel;
    [SerializeField] TextMeshProUGUI retryText;

    public static MainMenu instance;

    public int score = 0;

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
        ChangeMusicVolume(PlayerPrefs.GetFloat("Volume1"));
    }

    public void ChangeMusicVolume(float vol)
    {
        musicMixer.SetFloat("MusicVolume", Mathf.Log10(vol)*20);
        UpdateMusicText();
        PlayerPrefs.SetFloat("Volume1", vol);
    }

    public void UpdateMusicSlider()
    {
        float currentVolume;
        musicMixer.GetFloat("MusicVolume", out currentVolume);
        slider.value = Mathf.Pow(10,currentVolume/20);
    }

    public void UpdateMusicText()
    {
        float currentVolume;
        musicMixer.GetFloat("MusicVolume", out currentVolume);
        text.text = "ÇÂÓÊ: "+ Mathf.Round(Mathf.Pow(10, currentVolume / 20) * 100) +"%";
    }

    public void Retry(string text)
    {
        score = 0;
        retryPanel.SetActive(true);
        retryText.text = text;
    }


    //private void Awake()
    //{
    //    if (instance == null)
    //    {
    //        instance = this;
    //    }
    //    else if (instance != this)
    //    {
    //        Destroy(gameObject);
    //    }

    //    DontDestroyOnLoad(gameObject);

    //}

    public void LoadNextLevel(int buildIndex)
    {
        SceneManager.LoadScene(buildIndex);
    }
}
