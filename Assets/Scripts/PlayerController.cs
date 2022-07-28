using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    public bool Hidden
    {
        get { return hidden; }
    }

    private Transform playerObj;

    #region Player Inputs

    private PlayerInput playerInput;
    private PlayerControls playerControls;
    private CharacterController characterController;

    private float playerSpeed = 10.0f;
    
    private Vector3 playerPosition;

    #endregion

    #region Camera Settings

    public Transform playerCamera;
    private float CameraRotX;

    #endregion


    private bool hidden = false;

   

 

    private bool moving = false;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        characterController = GetComponent<CharacterController>();

        playerControls = new PlayerControls();
        playerControls.Player.Enable();

        playerObj = this.gameObject.transform;
    }

    private void Movement()
    {
        // Getting Input and Mouse Vectors
        Vector2 inputVector = playerControls.Player.Movement.ReadValue<Vector2>();
        Vector2 mouseVector = Mouse.current.delta.ReadValue();

        // Setting the of player
        playerObj.rotation *= Quaternion.Euler(0, mouseVector.x * 30.0f * Time.deltaTime, 0);

        // Setting the rotation of the camera
        SetCamera();
        
        // Position of character
        playerPosition = new Vector3(inputVector.x, 0, inputVector.y) * playerSpeed;
        playerPosition = transform.TransformDirection(playerPosition);
        characterController.Move(playerPosition * Time.deltaTime);
    }

    private void SetCamera()
    {
        CameraRotX += Input.GetAxis("Mouse Y");
        CameraRotX = Mathf.Clamp(CameraRotX, -40, 40);
        playerCamera.rotation = Quaternion.Euler(CameraRotX, transform.eulerAngles.y, 0);

        Debug.Log(playerCamera.eulerAngles.x);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }
}
