using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class TPSController : MonoBehaviour
{
    private CharacterController _characterController;
    private Transform _camera;
    private Transform _lookAtPlayer;

     //-----------------------Camaras-----------------------
     [SerializeField] private GameObject _normalCamera;
     [SerializeField] private GameObject _aimCamera;
     //----------------Variables Movimiento-----------------
    [SerializeField] private float _movementSpeed = 5;
    private float _horizontal;
    private float _vertical;
    [SerializeField] private float _turnSmoothTime=0.1f;
    [SerializeField] private float _jumpHeight=1; 

    //----------------Variables Gravedad--------------------
    [SerializeField] private float _gravity=-9.81f;
    [SerializeField] private Vector3 _playergravity;

    //----------------Variables GroundSensor----------------
    [SerializeField] LayerMask _floorLayer;
    [SerializeField] Transform _sensorPosition;
    [SerializeField] float _sensorRadius=0.5f;

    //----------------Variables Axis----------------
    [SerializeField] private AxisState xAxis;
    [SerializeField] private AxisState yAxis;



    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Awake(){
        _characterController = GetComponent<CharacterController>();
        _camera = Camera.main.transform;
        _lookAtPlayer = GameObject.Find("LookAtPlayer").transform;
    }

    // Update is called once per frame
    void Update(){
        _horizontal=Input.GetAxis("Horizontal");
        _vertical=Input.GetAxis("Vertical");
        if(Input.GetButtonDown("Fire2")){
            _normalCamera.SetActive(false);
            _aimCamera.SetActive(true);
        }else if (Input.GetButtonUp("Fire2")){
            _normalCamera.SetActive(true);
            _aimCamera.SetActive(false);
        }
        Movement();
        if(Input.GetButtonDown("Jump") && IsGrounded()) Jump();
        Gravity();
    }

    void Movement(){
        Vector3 move = new Vector3(_horizontal, 0, _vertical);
        
        //Camera Movement
        yAxis.Update(Time.deltaTime);
        xAxis.Update(Time.deltaTime);
        transform.rotation=Quaternion.Euler(0,xAxis.Value, 0);
        _lookAtPlayer.rotation=Quaternion.Euler(yAxis.Value, xAxis.Value,0);
        float targetAngle = Mathf.Atan2(move.x, move.z) * Mathf.Rad2Deg+_camera.eulerAngles.y;
        
        if(move!=Vector3.zero){
            //Ch Movement
            Vector3 moveDirection = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;
            _characterController.Move(moveDirection*_movementSpeed*Time.deltaTime);
        }
    }
    void Gravity(){
        if(!IsGrounded()) _playergravity.y += _gravity *Time.deltaTime;
        else if(IsGrounded() && _playergravity.y <0) _playergravity.y=-1;
        
        _characterController.Move(_playergravity *Time.deltaTime);
    }
    bool IsGrounded(){
        return Physics.CheckSphere(_sensorPosition.position, _sensorRadius, _floorLayer);
    }

    void Jump(){
        _playergravity.y=Mathf.Sqrt(_jumpHeight*-2*_gravity);
    }
}
