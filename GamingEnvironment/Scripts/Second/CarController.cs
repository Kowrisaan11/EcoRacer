using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    [Header ("Car Settings")]
    public float driftFactor = 0.95f;
    public float accelerationFactor = 15.0f;
    public float turnFactor = 2.5f;
    public float maxspeed = 6;


    // Local variables
    float accelerationInput = 0;
    float steeringInput = 0;
    float rotationAngle = 0;
    float velocityVsUp = 0;

    //Components
    Rigidbody2D carRigidbody2D;

    void Awake(){
        carRigidbody2D = GetComponent<Rigidbody2D>();
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        ApplyEngineForce();

        KillOrthogonalVelocity();

        ApplySteering();

    }

    void ApplyEngineForce()
    {
        velocityVsUp = Vector2.Dot(transform.up, carRigidbody2D.velocity);

        if (velocityVsUp > maxspeed && accelerationInput > 0)
            return;

        if (velocityVsUp < -maxspeed * 0.5f && accelerationInput < 0)
            return;

        if (carRigidbody2D.velocity.sqrMagnitude > maxspeed * maxspeed && accelerationInput > 0)
            return;

        if (accelerationInput == 0)
            carRigidbody2D.drag = Mathf.Lerp(carRigidbody2D.drag, 3.0f, Time.fixedDeltaTime * 3);
        else carRigidbody2D.drag = 0;

        Vector2 engineForceVector = transform.up * accelerationInput * accelerationFactor;
        carRigidbody2D.AddForce(engineForceVector, ForceMode2D.Force);

        Debug.Log(velocityVsUp);
    }
 
    void ApplySteering(){
        float minSpeedBeforeAllowTurningFactor = (carRigidbody2D.velocity.magnitude / 8);
        minSpeedBeforeAllowTurningFactor = Mathf.Clamp01(minSpeedBeforeAllowTurningFactor);

        rotationAngle -= steeringInput * turnFactor * minSpeedBeforeAllowTurningFactor;

        // rotationAngle -= steeringInput * turnFactor;
        carRigidbody2D.MoveRotation(rotationAngle);

        // Debug.Log(rotationAngle);
        
    }

    void KillOrthogonalVelocity(){
        Vector2 forwardVelocity = transform.up * Vector2.Dot(carRigidbody2D.velocity, transform.up);
        Vector2 rightVelocity = transform.right * Vector2.Dot(carRigidbody2D.velocity, transform.right);

        carRigidbody2D.velocity = forwardVelocity + rightVelocity * driftFactor;
        // Debug.Log(carRigidbody2D.velocity);
        // Debug.Log(forwardVelocity);


    }

    public void SetInputVector(Vector2 inputVector){
        steeringInput = inputVector.x;
        accelerationInput = inputVector.y;
        
        // Debug.Log(accelerationInput);
        
    }

}
