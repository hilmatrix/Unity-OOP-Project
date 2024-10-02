using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor;
#endif


public class TitleManager : MonoBehaviour
{
    [SerializeField] private TMP_InputField nameText;
    [SerializeField] private TMP_Text warning;
    [SerializeField] private Button start;
    [SerializeField] private Button exit;
    [SerializeField] private Button clearScore;

    public List<TMP_Text> scoreList;
    public List<DifficultyButton> difficultyButtonList;

    // Start is called before the first frame update
    void Start()
    {
        warning.gameObject.SetActive(false);
        start.onClick.AddListener(ButtonStart);
        exit.onClick.AddListener(ButtonExit);
        clearScore.onClick.AddListener(ButtonClearScore);

        UpdateScore();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateScore()
    {
        int saveLength = SaveManager.instance.scoreData.Count;

        for (int loop = 0; loop < saveLength; loop++)
        {
            scoreList[loop].text = (loop + 1) + ". " + SaveManager.instance.scoreData[loop].DataToString();
        }

        for (int loop = saveLength; loop < SaveManager.SAVE_MAX; loop++)
        {
            scoreList[loop].text = (loop + 1) + ". <Empty>";
        }
    }

    public void ButtonStart()
    {
        if (string.IsNullOrWhiteSpace(nameText.text))
        {
            warning.text = "Name should not empty or only white spaces";
            warning.gameObject.SetActive(true);
        }
        else if (nameText.text.Length > 16)
        {
            warning.text = "Name length should not exceed 16";
            warning.gameObject.SetActive(true);
        }
        else
        {
            SaveManager.instance.StartGame(nameText.text);
            SceneManager.LoadScene(1);
        }
    }

    public void ButtonExit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    public void ButtonClearScore()
    {
        SaveManager.instance.ClearScore();
    }
}
