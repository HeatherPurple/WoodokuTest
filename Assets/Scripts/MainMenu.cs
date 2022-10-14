using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject retryPanel;
    [SerializeField] private TextMeshProUGUI retryText;
    private int movesLeft = 1;

    public int Score { get; set; }

    public static MainMenu instance;

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
    #endregion


    public void Retry(string text)
    {
        Score = 0;
        retryPanel.SetActive(true);
        retryText.text = text;
    }

    public void DecreaseMoves()
    {
        movesLeft -= 1;

        if (movesLeft <= 0)
        {
            string text;
            if (Score > 0)
            {
                text = "онаедю";
            }
            else
            {
                text = "ньхайю";
            }
            Retry(text);
        }
    }

    public void LoadNextLevel(int buildIndex)
    {
        SceneManager.LoadScene(buildIndex);
    }
}
