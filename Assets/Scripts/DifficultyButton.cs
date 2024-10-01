using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyButton : MonoBehaviour
{
    public int difficulty;

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(Clicked);
    }

    private void Clicked()
    {
        GameManager gameManager = GameObject.Find("Scripts").GetComponent<GameManager>();
        gameManager.StartGame(difficulty);
    }
}
