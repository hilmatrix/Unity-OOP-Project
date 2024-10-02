using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject obstacle;
    public float positionZ = 100f;
    public float spawnDelayMin = 2f;
    public float spawnDelayMax = 4f;
    public float spawnWaitTime;
    public float wait;
    public int difficulty = 3;
    public Text textHealth;
    public Text textScore;
    public Text textDifficulty;
    public int _health;
    public int _score;
    public bool gameStarted = false;
    public GameObject displayStart;
    public GameObject displayGameOver;

    public int health
    {
        set
        {
            _health = value;
            textHealth.text = "Health : " + value;
        }
        get
        {
            return _health;
        }
    }

    public int score
    {
        set
        {
            _score = value;
            textScore.text = "Score : " + value;
        }
        get
        {
            return _score;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        gameStarted = false;
        wait = 0;
        spawnWaitTime = spawnDelayMax;
        displayGameOver.SetActive(false);

        StartGame(SaveManager.instance.difficulty);
    }

    // Update is called once per frame
    void Update()
    {
        if (gameStarted)
            Spawn();
    }

    public void Spawn()
    {
        if (wait < spawnWaitTime)
        {
            wait += Time.deltaTime;
        }
        else
        {
            wait = 0;
            Instantiate(obstacle, new Vector3(Random.RandomRange(-10,10), 0, positionZ), transform.rotation);
            spawnWaitTime = Random.RandomRange(spawnDelayMin, spawnDelayMax);
        }
    }

    public void AddScore(int score)
    {
        this.score += score;
    }

    public void SetHealth(int health)
    {
        this.health = health;
        if (health <= 0)
        {
            gameStarted = false;
            displayGameOver.SetActive(true);
            SaveManager.instance.SaveScore(score);
        }
    }

    public void StartGame(int difficulty)
    {
        this.difficulty = difficulty;
        gameStarted = true;
        switch(difficulty)
        {
            case (1): textDifficulty.text = "Difficulty : Easy"; break;
            case (2): textDifficulty.text = "Difficulty : Medium"; break;
            case (3): textDifficulty.text = "Difficulty : Hard"; break;
        }
        displayStart.SetActive(false);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Exit()
    {
        SceneManager.LoadScene(0);
    }

}
