using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillatior : MonoBehaviour
{

    [SerializeField] float period = 2f;
    [SerializeField] Vector3 movementVector;
    Vector3 startingPosition;
    float movementFactor;
    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (period <= Mathf.Epsilon)
        {
            return;
        }
        float cycles = Time.time / period; 
        const float tau = Mathf.PI * 2; // constant value of 6.283
        float rawSineWave = Mathf.Sin(cycles * tau); // going from -1 to 1
        movementFactor = (rawSineWave +1f) / 2f;
        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPosition + offset;
    }
}
