using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyButton : MonoBehaviour
{
    public int difficulty;

    private Button m_button;

    private void Awake()
    {
        m_button = GetComponent<Button>();
    }

    private void Start()
    {
        m_button.onClick.AddListener(Clicked);
        Recolor();
    }

    private void Clicked()
    {
        //GameManager gameManager = GameObject.Find("Scripts").GetComponent<GameManager>();
        //gameManager.StartGame(difficulty);
        SaveManager.instance.difficulty = difficulty;
        Recolor();
    }

    private void Selected()
    {
        m_button.GetComponent<Image>().color = Color.blue;
        m_button.GetComponentInChildren<TMPro.TMP_Text>().color = Color.white;
    }

    private void Unselected()
    {
        m_button.GetComponent<Image>().color = Color.white;
        m_button.GetComponentInChildren<TMPro.TMP_Text>().color = Color.black;
    }

    private void Recolor()
    {
        List<DifficultyButton> difficultyButtons = GameObject.Find("TitleManager").GetComponent<TitleManager>().difficultyButtonList;
        
        foreach (DifficultyButton difficultyButton in difficultyButtons)
        {
            
            if (difficultyButton.difficulty == SaveManager.instance.difficulty)
            {
                difficultyButton.Selected();
            } else
            {
                difficultyButton.Unselected();
            }
        }
    }
}
