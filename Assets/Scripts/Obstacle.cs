using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour, Enemy
{
    public GameManager gameManager;
    Vector3 rotationSpeedVector;
    public float rotationSpeed = 10;
    public int health = 10;
    public GameObject explosion;
    public AudioSource audioSource;
    public AudioClip crashSound;
    public float speedDeviation;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Scripts").GetComponent<GameManager>();
        int randomize = Random.Range(gameManager.difficulty, gameManager.difficulty * 3) - 1;
        health += randomize;
        Debug.Log("randomize = "  + randomize);
        transform.localScale = transform.localScale * (health / 3f);

        GetComponent<MoveForward>().speedDeviation = Random.Range(-speedDeviation, speedDeviation);

        audioSource = GameObject.Find("Scripts").GetComponent<AudioSource>();
        rotationSpeedVector = new Vector3(Random.RandomRange(-rotationSpeed, rotationSpeed), 
            Random.RandomRange(-rotationSpeed, rotationSpeed), Random.RandomRange(-rotationSpeed, rotationSpeed));
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(rotationSpeedVector);
    }

    public void Damage()
    {
        health--;
        if (health <= 0)
        {
            Die();
        }
    }

    public void DamageFull()
    {
        Die();
    }

    public void Die()
    {
        gameManager.AddScore(1*gameManager.difficulty);
        Instantiate(explosion, transform.position, transform.rotation);
        Destroy(gameObject);
        audioSource.PlayOneShot(crashSound, 0.5f);
    }
}
