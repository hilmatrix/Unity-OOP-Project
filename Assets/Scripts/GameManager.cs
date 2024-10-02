using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject asteroid;
    [SerializeField] private GameObject ufo;
    [SerializeField] private float positionZ = 100f;
    [SerializeField] private float spawnDelayMin = 2f;
    [SerializeField] private float spawnDelayMax = 4f;
    [SerializeField] private float spawnWaitTime;
    [SerializeField] private float wait;
    [SerializeField] private Text textHealth;
    [SerializeField] private Text textScore;
    [SerializeField] private Text textDifficulty;
    [SerializeField] private int _health;
    [SerializeField] private int _score;
    [SerializeField] private bool gameStarted = false;
    [SerializeField] private GameObject displayStart;
    [SerializeField] private GameObject displayGameOver;

    // ENCAPSULATION
    [SerializeField] private int m_difficulty = 1;

    int spawnBeforeUFO = 0;

    public int difficulty
    {
        get { return m_difficulty; }
        private set { m_difficulty = value; }
    }

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
            if (spawnBeforeUFO > 0)
            {
                Instantiate(asteroid, new Vector3(Random.Range(-10, 10), 0, positionZ), transform.rotation);
                spawnBeforeUFO--;

            } else
            {
                Instantiate(ufo, new Vector3(Random.Range(-10, 10), 0, positionZ), transform.rotation);
                spawnBeforeUFO = 3;
            }
            spawnWaitTime = Random.Range(spawnDelayMin, spawnDelayMax);
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
