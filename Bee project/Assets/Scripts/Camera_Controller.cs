using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Controller : MonoBehaviour
{
    public float normalSpeed;
    public float fastSpeed;
    public float movementSpeed;
    public float movementTime;
    public float rotationAmount;

    private Vector3 newPosition;
    private Quaternion newRotation;

    // Start is called before the first frame update
    void Start()
    {
        newPosition = transform.position;
        newRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovementInput();
    }

   void HandleMovementInput()
{
    // Skip movement updates if movementSpeed is 0
    if (movementSpeed == 0)
    {
        return;
    }

    if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
    {
        movementSpeed = fastSpeed;
    }
    else
    {
        movementSpeed = normalSpeed;
    }

    if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
    {
        newPosition += (transform.up * movementSpeed);
    }
    if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
    {
        newPosition += (transform.up * -movementSpeed);
    }
    if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
    {
        newPosition += (transform.right * movementSpeed);
    }
    if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
    {
        newPosition += (transform.right * -movementSpeed);
    }
    if (Input.GetKey(KeyCode.Z) && GetComponent<Camera>().orthographicSize > 0.01)
    {
        GetComponent<Camera>().orthographicSize -= 0.01f;
    }
    if (Input.GetKey(KeyCode.C))
    {
        GetComponent<Camera>().orthographicSize += 0.01f;
    }

    // Moving without infinite momentum
    transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * movementTime);
    transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, Time.deltaTime * movementTime);
}


    // Method to stop camera movement completely
    public void noMovement()
    {   
        Debug.Log("trying");
        newPosition = transform.position; 
        newRotation = transform.rotation; 
        movementSpeed = 0; 
    }

    public void normalMovement() 
    {
        movementSpeed = 0.01;
    }
}
