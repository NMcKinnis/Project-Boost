using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float mainThrust = 100f;
    [SerializeField] float rotationThrust = 1f;
    [SerializeField] Transform rotationThreshold;
    [SerializeField] AudioClip mainEngine;
    Rigidbody myRigidbody;
    AudioSource audioSource;
    bool isPlaying = false;


    // Start is called before the first frame update
    private void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }
    private void Update()
    {
        
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        ProcessThrust();
        ProcessRotation();
    }

    private void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            PlayThrustSFX();
            myRigidbody.AddRelativeForce(Vector3.up *mainThrust * Time.deltaTime);
        }
        else
        {
            audioSource.Stop();
            isPlaying = false;
        }
    }

    private void PlayThrustSFX()
    {
        if (!isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);
            isPlaying = true;
        }

    }

    private void ProcessRotation()
    {
        if(Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D)) { return; }
        if(Input.GetKey(KeyCode.D))
        {
            ApplyRotation(rotationThrust);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            ApplyRotation(-rotationThrust);
        }
    }

    private void ApplyRotation(float rotationThisFrame)
    {
        myRigidbody.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        myRigidbody.freezeRotation = false;
    }
}
