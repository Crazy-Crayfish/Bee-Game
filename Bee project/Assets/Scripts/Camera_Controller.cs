using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Camera_Controller : MonoBehaviour
{
    public float normalSpeed;
    public float fastSpeed;
    public float movementSpeed;
    public float movementTime;
    public float rotationAmount;

    public Vector3 newPosition;
    public Quaternion newRotation;

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
        if (Input.GetKey(KeyCode.LeftShift) ||Input.GetKey(KeyCode.RightShift))
        {
            movementSpeed = fastSpeed;
        }
        else
        {
            movementSpeed = normalSpeed;
        }
        if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            newPosition += (transform.up * movementSpeed);
        }
        if(Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            newPosition += (transform.up * -movementSpeed);
        }
        if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            newPosition += (transform.right * movementSpeed);
        }
        if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            newPosition += (transform.right * -movementSpeed);
        }

        if(Input.GetKey(KeyCode.Space))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        

        // //if(Input.GetKey(KeyCode.Z))
        // {
        //     newPosition += (transform.forward * movementSpeed);
        // }
        // if(Input.GetKey(KeyCode.C))
        // {
        //     newPosition += (transform.forward * -movementSpeed);
        // }
        // rotation
        // if(Input.GetKey(KeyCode.Q))
        // {
        //     newRotation *= Quaternion.Euler(Vector3.forward * rotationAmount);
        // }
        // if(Input.GetKey(KeyCode.E))
        // {
        //     newRotation *= Quaternion.Euler(Vector3.forward * -rotationAmount);
        // }

        transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * movementTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, Time.deltaTime * movementTime);
    }
}
