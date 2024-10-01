using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : MonoBehaviour
{
    public float speed = 1f;
    public float speedDeviation = 0;
    public float timeToLive = 1f;
    public float time;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + Vector3.forward * speed * Time.deltaTime + Vector3.right * speedDeviation * Time.deltaTime;
        time += Time.deltaTime;
        if (time >= timeToLive)
        {
            Destroy(gameObject);
        }
    }
}
