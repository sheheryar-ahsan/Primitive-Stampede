using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatBackground : MonoBehaviour
{
    private Vector3 startPos;
    private float moveSpeed = 5;
    private float repeatWidth;
    private GameManager gameManager;

    void Start()
    {
        startPos = transform.position;
        repeatWidth = GetComponent<BoxCollider>().size.x / 2; // Find the center of the background using box collider for accrate repeating from center again & again
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }
    void Update()
    {
        MoveTheRide();
        RepeatTheScene();
    }
    private void MoveTheRide() // Moving the backgorung left with a speed
    {
        if (gameManager.gameStop == false)
        {
            transform.Translate(Vector3.right * -moveSpeed * Time.deltaTime);
        }
    }
    private void RepeatTheScene() // Resetting the bacground posisiton after a certain X move
    {
        if (transform.position.x < startPos.x - repeatWidth)
        {
            transform.position = startPos;
        }
    }
}
