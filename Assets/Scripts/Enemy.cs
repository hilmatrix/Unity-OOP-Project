using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected GameManager gameManager;
    [SerializeField] private GameObject explosion;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip crashSound;

    [SerializeField] protected int health;
    [SerializeField] protected int score = 1;

    void Start()
    {
        gameManager = GameObject.Find("Scripts").GetComponent<GameManager>();
        audioSource = GameObject.Find("Scripts").GetComponent<AudioSource>();
        Preparation();
    }


    // Update is called once per frame
    void Update()
    {
        Action();
    }

    protected virtual void Preparation()
    {

    }

    protected virtual void Action()
    {

    }

    // ABSTRACTION
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

    // ABSTRACTION
    public void Die()
    {
        gameManager.AddScore(score);
        Instantiate(explosion, transform.position, transform.rotation);
        Destroy(gameObject);
        audioSource.PlayOneShot(crashSound, 0.5f);
    }
}
