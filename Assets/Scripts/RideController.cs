using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RideController : MonoBehaviour
{
    private Rigidbody rideRb;
    private float speed = 10.0f;
    private float rideSpeed = 5;
    private float zRange = 3f;
    private bool riderCheck = false;
    private GameManager gameManager;
    public ParticleSystem collisionParticle;
    private AudioSource rideAudio;
    public AudioClip collisionSound;

    void Start()
    {
        rideRb = GetComponent<Rigidbody>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        rideAudio = GetComponent<AudioSource>();
    }
    void Update()
    {
        MoveTheRide();
        ControlTheRide();
        DestroyTheRide();
    }
    private void MoveTheRide() // For moving the ride left
    {
        if (gameManager.gameStop == false)
        {
            transform.Translate(Vector3.right * -rideSpeed * Time.deltaTime);
        }
    }
    private void ControlTheRide() // For Controlling the Ride on vertical axis after successful jump
    {
        if (riderCheck == true)
        {
            float horizontalInput = Input.GetAxis("Vertical");
            transform.Translate(Vector3.forward * horizontalInput * Time.deltaTime * speed);
            CreateRideBounds();
        }
    }
    private void CreateRideBounds() // Creating the bounds for the ride to not leave the screen on -Z,Z & X
    {
        if (transform.position.z > zRange)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, zRange);
        }
        if (transform.position.z < -zRange)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -zRange);
        }
        if (transform.position.x < 0 && riderCheck == true)
        {
            transform.position = new Vector3(0, transform.position.y, transform.position.z);
        }
    }
    private void DestroyTheRide() // After a far certain left moving, when ride is offscreen destroy it
    {
        if (transform.position.x < -10)
        {
            Destroy(this.gameObject);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Rider")) // when player jumps on this ride
        {
            riderCheck = true;
        }
        if (collision.gameObject.CompareTag("Ride") && riderCheck == true) // Collision with the other ride
        {
            gameManager.gameStop = true;
            Instantiate(collisionParticle, transform.position, collisionParticle.transform.rotation);
            rideAudio.PlayOneShot(collisionSound, 1.0f);
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Rider")) // When player jumps off from this ride
        {
            riderCheck = false;
        }
    }
}
