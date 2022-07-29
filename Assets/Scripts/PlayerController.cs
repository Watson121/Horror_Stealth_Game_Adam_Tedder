using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{

    #region Properties

    public bool Hidden
    {
        get { return hidden; }
    }

    private bool hidden = false;
    #endregion

    [Header("Player Settings")]
    #region Player Inputs

    private PlayerInput playerInput;
    private PlayerControls playerControls;
    private CharacterController characterController;
    private Transform playerObj;

    public float playerSpeed = 10.0f;
    
    private Vector3 playerPosition;

    #endregion

    [Header("Camera Settings")]
    #region Camera Settings

    public Transform playerCamera;
    public float mouseSensivity = 30.0f; 

    private float CameraRotX;
    private float CameraRotY;

    #endregion

    private TextMeshProUGUI hiddenText;


    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        characterController = GetComponent<CharacterController>();

        playerControls = new PlayerControls();
        playerControls.Player.Enable();

        playerObj = this.gameObject.transform;

        //hiddenText = GameObject.Find("HiddenText").GetComponent<TextMeshProUGUI>();
    }

    private void Movement()
    {
        // Getting Input and Mouse Vectors
        Vector2 inputVector = playerControls.Player.Movement.ReadValue<Vector2>();
        Vector2 mouseVector = Mouse.current.delta.ReadValue();

        // Setting the of player
        playerObj.rotation *= Quaternion.Euler(0, mouseVector.x * 30.0f * Time.smoothDeltaTime, 0);
        playerObj.rotation.Normalize();

        // Setting the rotation of the camera
        SetCamera();

        // Position of character
        playerPosition = new Vector3(inputVector.x * playerSpeed, 0, inputVector.y * playerSpeed);
        playerPosition = transform.TransformDirection(playerPosition);
        characterController.Move(playerPosition * Time.smoothDeltaTime);
    }

    private void SetCamera()
    {
        CameraRotX -= Input.GetAxisRaw("Mouse Y") * mouseSensivity * Time.smoothDeltaTime;
        CameraRotX = Mathf.Clamp(CameraRotX, -40, 40);

        playerCamera.rotation = Quaternion.Euler(CameraRotX, playerObj.eulerAngles.y, 0);
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "HiddenZone")
        {
            hidden = true;
            SettingHiddenText("Hidden");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "HiddenZone")
        {
            hidden = false;
            SettingHiddenText("Unhidden");
        }
    }

    // Setting the Hidden Text
    private void SettingHiddenText(string text)
    {
        hiddenText.text = text;
    }

}
