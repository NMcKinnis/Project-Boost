using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    //Movement Tuning Variables
    [SerializeField] float mainThrust = 100f;
    [SerializeField] float maxSpeed = 300f;
    [SerializeField] float rotationThrust = 1f;
    //Particle FX
    [SerializeField] GameObject mainThruster;
    [SerializeField] GameObject sideThruster;
    [SerializeField] Transform mainThrusterPos;
    [SerializeField] Transform leftThrusterPos;
    [SerializeField] Transform rightThrusterPos;
    //Audio
    [SerializeField] AudioClip mainEngine;
    Rigidbody myRigidbody;
    AudioSource audioSource;
    //State
    bool isPlaying = false;


    private void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();

    }

    void FixedUpdate()
    {
        ProcessThrust();
        ProcessRotation();
        Debug.Log(myRigidbody.velocity);
    }

    private void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            PlayThrustSFX();
            Instantiate(mainThruster, mainThrusterPos);
   
            myRigidbody.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
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
            audioSource.clip = mainEngine;
            audioSource.Play();
            isPlaying = true;
        }

    }

    private void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D)) { return; }
        if (Input.GetKey(KeyCode.D))
        {
            Instantiate(sideThruster, leftThrusterPos);
            ApplyRotation(rotationThrust);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            Instantiate(sideThruster, rightThrusterPos);
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
