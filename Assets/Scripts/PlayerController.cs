using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private GameManager gameManager;
    Vector3 initialPosition;
    public bool moveUp;
    public float moveUpOffset = 1;
    public float moveXOffset = 5;
    public float currentRotation = 0;
    public float moveXSpeed;

    public float lastFire;
    public float fireDelay;
    public bool readyToFire;

    public GameObject projectile;
    public GameObject weaponLeft;
    public GameObject weaponCenter;
    public GameObject weaponRight;
    public GameObject explosion;

    public bool weaponRightReady;
    public bool weaponCenterReady;

    public AudioClip fireSound;
    public AudioClip crashSound;
    private AudioSource audioSource;

    public int initialHealth;
    public int health;

    public float posX
    {
        get { return transform.position.x; }
    }

    public float posY
    {
        get { return transform.position.y; }
    }

    public float posZ
    {
        get { return transform.position.z; }
    }

    public float initPosX
    {
        get { return initialPosition.x; }
    }

    public float initPosY
    {
        get { return initialPosition.y; }
    }

    public float initPosZ
    {
        get { return initialPosition.z; }
    }


    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Scripts").GetComponent<GameManager>();
        health = initialHealth;
        gameManager.SetHealth(health);
        moveUp = true;
        weaponRightReady = true;
        readyToFire = true;
        initialPosition = transform.position;
        audioSource = GameObject.Find("Scripts").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!readyToFire)
        {
            lastFire += Time.deltaTime;
            if (lastFire > fireDelay)
            {
                lastFire = 0;
                readyToFire = true;
            }
        }

        if (moveUp)
        {
            if (transform.position.y > (initialPosition.y + moveUpOffset))
            {
                moveUp = false;
            }
        } else
        {
            if (transform.position.y < (initialPosition.y - moveUpOffset))
            {
                moveUp = true;
            }
        }
        transform.position = new Vector3(transform.position.x, transform.position.y + (moveUp ? Time.deltaTime : -Time.deltaTime), transform.position.z);

        float horizontal = Input.GetAxis("Horizontal");
        if ((Mathf.Abs(horizontal) < 0.05f) && (Mathf.Abs(currentRotation) < 0.05f))
        {
            currentRotation = 0;
        }
        else if (horizontal < currentRotation)
        {
            currentRotation -= Time.deltaTime*5;
            if (Mathf.Abs(horizontal - currentRotation) < 0.05f)
                currentRotation = horizontal;
        } else if (horizontal > currentRotation)
        {
            currentRotation += Time.deltaTime * 5;
            if (Mathf.Abs(horizontal - currentRotation) < 0.05f)
                currentRotation = horizontal;
        }
        
            
        transform.rotation = Quaternion.Euler(0,0,-40* currentRotation);
        //Debug.Log("horizontal = " + horizontal + "currentRotation = " + currentRotation + " diff = " + Mathf.Abs(horizontal - currentRotation));


        transform.position = new Vector3(posX + horizontal * moveXSpeed * Time.deltaTime, initPosY, initPosZ);

        if (Input.GetKey(KeyCode.Space) && readyToFire)
        {
            readyToFire = false;
            audioSource.PlayOneShot(fireSound, 0.5f);

            if (weaponCenterReady)
            {
                weaponCenterReady = false;
                Instantiate(projectile, weaponCenter.transform.position, projectile.transform.rotation);
            }
            else if (weaponRightReady)
            {
                weaponRightReady = false;
                weaponCenterReady = true;
                Instantiate(projectile, weaponRight.transform.position, projectile.transform.rotation);
            } else
            {
                weaponRightReady = true;
                weaponCenterReady = true;
                Instantiate(projectile, weaponLeft.transform.position, projectile.transform.rotation);
            }
        }

        if (posX < (initPosX - moveXOffset))
        {
            transform.position = new Vector3(initPosX - moveXOffset, posY, posZ);
        } else if (posX > (initPosX + moveXOffset))
        {
            transform.position = new Vector3(initPosX + moveXOffset, posY, posZ);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Enemy enemy = other.gameObject.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.DamageFull();
            gameManager.SetHealth(--health);

            if (health <= 0)
            {
                Instantiate(explosion, transform.position, transform.rotation);
                Destroy(gameObject);
            }
        }
    }
}
