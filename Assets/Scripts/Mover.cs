using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] float thrustForce = 20.0f;
    [SerializeField] float rotatingSensitivity;
    [SerializeField] float maxRotDeadzone;
    [SerializeField] float minRotDeadzone;
 
    private Rigidbody rocketRB;
 
    private bool thrustHasBeenPressed = false;
    private bool rotateHasBeenActivated = false;
 
    // Start is called before the first frame update
    void Start()
    {
        this.rocketRB = this.gameObject.GetComponent<Rigidbody>();
    }
 
    // Update is called once per frame
    void Update()
    {
        this.ProcessInput();
 
        this.ProcessRotation();
    }
 
    private void LateUpdate()
    {
        this.ThrustShipForward();
 
        this.RotateShip();
    }
 
    private void ThrustShipForward()
    {
        if (this.thrustHasBeenPressed)
            this.rocketRB.AddRelativeForce(Vector3.up * this.thrustForce, ForceMode.Force);
    }
 
    private void RotateShip()
    {
        if (this.rotateHasBeenActivated)
        {
            float shipRotAngle = Input.GetAxis("Mouse X") * this.rotatingSensitivity;
 
            this.rocketRB.AddRelativeTorque(Vector3.forward * shipRotAngle, ForceMode.Force);
        }
        else
        {
            this.rocketRB.angularVelocity = Vector3.zero;
        }
    }
 
    private void ProcessInput()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if(!this.thrustHasBeenPressed)
                this.thrustHasBeenPressed = true;
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            if (this.thrustHasBeenPressed)
                this.thrustHasBeenPressed = false;
        }
    }
 
    private void ProcessRotation()
    {
        if (Input.GetAxis("Mouse X") > this.maxRotDeadzone ||
            Input.GetAxis("Mouse X") < this.minRotDeadzone)
        {
            this.rotateHasBeenActivated = true;
        }
        else
        {
            this.rotateHasBeenActivated = false;
        }
    }
}