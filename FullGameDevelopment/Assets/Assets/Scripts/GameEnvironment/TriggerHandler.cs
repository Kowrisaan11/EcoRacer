using System.Collections;
using System.Collections.Generic;
//using Unity.VisualScripting;
using UnityEngine;

public class TriggerHandler : MonoBehaviour
{
    CarController carController;

    void Awake()
    {
        carController = GetComponent<CarController>();
    }

    // Start is called before the first frame update
    private void Start()
    {
        Debug.Log("TriggerHandler script has started.");

    }

    void OnCollisionEnter2D(Collision2D collision2D)
    {
        Debug.Log("wall triggered");

        Rigidbody2D carRigidbody2D = GetComponent<Rigidbody2D>();
        carRigidbody2D.velocity = -carRigidbody2D.velocity / 2;


    }

    void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.CompareTag("CheckPoint"))
        {
            Debug.Log("check point triggered");

        }

    }










    // void OnTriggerEnter2D(Collider2D collider2D){

    //     if (collider2D.CompareTag("CheckPoint")){
    //         Debug.Log("check point triggered");

    //     }
    //     else{
    //         Debug.Log("wall triggered");
    //         Rigidbody2D carRigidbody2D = GetComponent<Rigidbody2D>();
    //         carRigidbody2D.velocity = -carRigidbody2D.velocity / 4;



    //         // triggered = true;
    //         // Rigidbody2D carRigidbody2D = GetComponent<Rigidbody2D>();

    //         // // carController.KillOrthogonalVelocity();


    //         // // carRigidbody2D.velocity = Vector2.one;

    //         // Debug.Log("1"+carRigidbody2D.velocity);
    //         // // carRigidbody2D.velocity = -carRigidbody2D.velocity / 4;   
    //         // Debug.Log("2"+carRigidbody2D.velocity);  
    //         // // carRigidbody2D.AddForce(-carRigidbody2D.velocity/carRigidbody2D.velocity.magnitude * 100, ForceMode2D.Force); 
    //         // carRigidbody2D.AddForce(new Vector2(0,-1) * 100, ForceMode2D.Force);


    //         // // if (carRigidbody2D.velocity.magnitude < 0.9f)
    //         // // {
    //         // //     // Apply a small impulse in the opposite direction to simulate repulsion
    //         // //     // Vector2 repulseDirection = -carRigidbody2D.transform.up; // Assuming the car's forward direction is 'up'
    //         // //     // carRigidbody2D.AddForce(-carRigidbody2D.velocity/carRigidbody2D.velocity.magnitude * 50f, ForceMode2D.Force);
    //         // //     carRigidbody2D.velocity = -carRigidbody2D.velocity / carRigidbody2D.velocity.magnitude;
    //         // // }
    //         // // else
    //         // // {
    //         // //     // Invert the car's velocity and reduce it to a quarter
    //         // //     carRigidbody2D.velocity = -carRigidbody2D.velocity / 4;
    //         // // }



    //     }

    // }
}
