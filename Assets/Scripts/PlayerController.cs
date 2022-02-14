using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    public GameObject boxPrefab;
    private ProjectileSystem projectileSystem;
    private GameManager gameManager;
    private Transform horsePosition;
    private bool rideCheck = false;
    private bool groundCheck = false;
    private bool startRun = true;
    private AudioSource playerAudio;
    public AudioClip jumpSound;
    public AudioClip crashSound;

    void Start()
    {
        projectileSystem = new ProjectileSystem();
        playerRb = GetComponent<Rigidbody>();
        playerAudio = GetComponent<AudioSource>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }
    void Update()
    {
        PlayerJump();
    }
    private void PlayerJump() // For the jump of the rider, from his location to the cursor point
    {
        Vector3 vo = projectileSystem.delegateProjectileSystem.Invoke(boxPrefab.transform.position, this.transform.position, 1f);

        if (Input.GetKeyDown(KeyCode.Space) && groundCheck == false) // When space is pressed and player is on the ride
        {
            playerRb.constraints = RigidbodyConstraints.None;
            playerRb.velocity = vo;
            rideCheck = false;
            startRun = false;
            playerAudio.PlayOneShot(jumpSound, 1.0f);
        }
        if (rideCheck == true) // While Riding, the rider posistion is bind to the ride position
        {
            transform.position = new Vector3(horsePosition.transform.position.x, horsePosition.transform.position.y + 1f, horsePosition.transform.position.z);
            playerRb.constraints = RigidbodyConstraints.FreezeRotation;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor") && startRun == false) // On collision with the ground
        {
            groundCheck = true;
            gameManager.gameStop = true;
            playerAudio.PlayOneShot(crashSound, 1.0f);
            playerRb.constraints = RigidbodyConstraints.FreezeAll;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ride") && gameManager.gameStop == false) // When rider hits the ride trigger
        {
            rideCheck = true;
            horsePosition = other.gameObject.GetComponent<Transform>();
            groundCheck = false;
            gameManager.UpdateScore(25); // Adding the Score of 25 on each successful jump
        }
    }
}
