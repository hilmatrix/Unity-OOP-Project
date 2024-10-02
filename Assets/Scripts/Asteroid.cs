using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// INHERITANCE
public class Asteroid : Enemy
{
    [SerializeField] private Vector3 rotationSpeedVector;
    [SerializeField] public float rotationSpeed = 10;
    [SerializeField] private float speedDeviation;

    // POLYMORPHISM
    protected override void Preparation()
    {
        int randomize = Random.Range(gameManager.difficulty, gameManager.difficulty * 3) - 1;
        int score = 1 * gameManager.difficulty;
        health += randomize;
        transform.localScale = transform.localScale * (health / 3f);
        GetComponent<MoveForward>().speedDeviation = Random.Range(-speedDeviation, speedDeviation);
        rotationSpeedVector = new Vector3(Random.Range(-rotationSpeed, rotationSpeed),
            Random.Range(-rotationSpeed, rotationSpeed), Random.Range(-rotationSpeed, rotationSpeed));
    }

    // POLYMORPHISM
    protected override void Action()
    {
        transform.Rotate(rotationSpeedVector);
    }
}
