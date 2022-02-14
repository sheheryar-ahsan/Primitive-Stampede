using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    public float speed = 15f;
    private float zRange = 3f;

    void Update()
    {
        CursorController();
    }
    private void CursorController() // For Controlling the cursor
    {
        if (Input.GetKey(KeyCode.UpArrow)) // On up arrow press
        {
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
        }
        if (Input.GetKey(KeyCode.DownArrow)) // On down arrow press
        {
            transform.Translate(Vector3.forward * Time.deltaTime * -speed);
        }
        if (Input.GetKey(KeyCode.RightArrow)) // On right arrow press
        {
            transform.Translate(Vector3.right * Time.deltaTime * speed);
        }
        if (Input.GetKey(KeyCode.LeftArrow)) // On left arrow press
        {
            transform.Translate(Vector3.right * Time.deltaTime * -speed);
        }
        CreateCursorBounds();
    }
    private void CreateCursorBounds() // The cursor bounds on -X,X & -Z,Z
    {
        if (transform.position.z > zRange)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, zRange);
        }
        if (transform.position.z < -zRange)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -zRange);
        }
        if (transform.position.x > 15)
        {
            transform.position = new Vector3(15, transform.position.y, transform.position.z);
        }
        if (transform.position.x < 5)
        {
            transform.position = new Vector3(5, transform.position.y, transform.position.z);
        }
    }
}
