using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Run : MonoBehaviour
{
    public float runSpeed = 10;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetFloat("Speed_f", 1f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.forward * runSpeed * Time.deltaTime;
    }
}
