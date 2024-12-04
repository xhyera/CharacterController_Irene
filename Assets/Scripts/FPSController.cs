using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSController : MonoBehaviour
{
    private CharacterController _characterController;
    private Transform _camera;

     //----------------Variables Movimiento-----------------
    [SerializeField] private float _movementSpeed = 5;
    private float _horizontal;
    private float _vertical;
    private float _xRotation;
    [SerializeField] private float _jumpHeight=1; 
    [SerializeField] private float _sensitivity = 100;

    //----------------Variables Gravedad--------------------
    [SerializeField] private float _gravity=-9.81f;
    [SerializeField] private Vector3 _playergravity;

    //----------------Variables GroundSensor----------------
    [SerializeField] LayerMask _floorLayer;
    [SerializeField] Transform _sensorPosition;
    [SerializeField] float _sensorRadius=0.5f;
    
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Awake(){
        _characterController = GetComponent<CharacterController>();
        _camera = Camera.main.transform;
    }
    void Update(){
        _horizontal=Input.GetAxis("Horizontal");
        _vertical=Input.GetAxis("Vertical");
        Movement();

        if(Input.GetButtonDown("Jump") && IsGrounded()) Jump();
        Gravity();
    }
    void Movement(){
       float mouseX = Input.GetAxis("Mouse X") * _sensitivity * Time.deltaTime;
       float mouseY = Input.GetAxis("Mouse Y") * _sensitivity * Time.deltaTime;

        _xRotation -= mouseY;
        _xRotation = Mathf.Clamp(_xRotation, -90,90);

        _camera.localRotation = Quaternion.Euler(_xRotation, 0, 0);
        transform.Rotate(Vector3.up * mouseX);

        Vector3 move = transform.right *_horizontal + transform.forward * _vertical;
        _characterController.Move(move *_movementSpeed *Time.deltaTime);
    }
   
    void Gravity(){
        if(!IsGrounded()) _playergravity.y += _gravity *Time.deltaTime;
        else if(IsGrounded() && _playergravity.y <0) {
            _playergravity.y=-1;
        }
        _characterController.Move(_playergravity *Time.deltaTime);
    }
    bool IsGrounded(){
        return Physics.CheckSphere(_sensorPosition.position, _sensorRadius, _floorLayer);
    }
    void OnDrawGizmos(){
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(_sensorPosition.position, _sensorRadius);
    }
    void Jump(){
        _playergravity.y=Mathf.Sqrt(_jumpHeight*-2*_gravity);
    }
}
