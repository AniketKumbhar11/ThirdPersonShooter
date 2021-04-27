using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PLayerScript : MonoBehaviour
{
    #region

    [Header("--------------------------Animation Value For Animator-------------------")]

    [SerializeField] private float AnimWalk=0.2f;
    [SerializeField] private float AnimRun=1.5f;
    [SerializeField] private float Animidel=0f;
    [SerializeField] private float AnimCrouch=0f;
    [SerializeField] private float AnimationSmothSpeed=0.2f;

    [Header("--------------------------Controller Speed --------------------")]

    [SerializeField] private float CrouchSpeed=5f;
    [SerializeField] private float JumpHeight=1f;
    [SerializeField] private float RunSpeed=7f;
    [SerializeField] private float WalkSpeed=2f;

    public float Speed=3f;

    [Header("--------------------------Jump value and Speed --------------------")]

    public LayerMask graoundMask;
    public float groundDistance = 0.4f;
    public float gravity = -9.81f;
    private bool isGrounded;
    private Vector3 GroundVelocity;
    public Transform GroundCheck;

    [Header("--------------------------Camera value and Speed --------------------")]

    public Transform cam;
    public float TurnSmothTime = 0.1f;
    private float TurnSmothVelocity;

    [Header("---------------------------------------------------------------------")]

    [SerializeField] private CharacterController playerController;
    [SerializeField] private Animator PlayerAnimator;

    private bool InputPress;
    private Vector3 moveDir;
    public bool run, walk, idel,door,LeftShipressed,isvalid;
    public static PLayerScript ObjPlayer;


    public bool PlayerInput;
    #endregion

    private void Awake()
    {
    ObjPlayer = this;
    cam = Camera.main.transform;
    PlayerInput = true;
    playerController= GetComponent<CharacterController>();
    PlayerAnimator= GetComponent<Animator>();
    GroundCheck = GameObject.Find("GrvityCheck").transform;
    isvalid=true;
    
    }
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }


    void Update()
    {
        if (PlayerInput)
        {
            GetMoventInput();
            GetMoeventDirection();
            GetJumpInput();
          
        }

if (isvalid)
{
            if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            LeftShipressed=true;
        }
        if(Input.GetKeyUp(KeyCode.LeftShift))
        {
         LeftShipressed=false;
        }

}




    }

    private void GetJumpInput()
    {
        isGrounded = Physics.CheckSphere(GroundCheck.position, groundDistance, graoundMask);
      
        if (isGrounded && GroundVelocity.y < 0)
        {
            GroundVelocity.y = -2f;
            PlayerAnimator.SetBool("Jump", false);
        }
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            GroundVelocity.y = Mathf.Sqrt(JumpHeight * -4f * gravity);
            PlayerAnimator.SetBool("Jump", true);
           // Debug.Log("JUmmmpppp");          
        }
        GroundVelocity.y += gravity * Time.deltaTime;
        playerController.Move(GroundVelocity * Time.deltaTime);

    }

    private void GetMoventInput()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        if (horizontal!=0 || vertical!=0)
        {
            InputPress = true;
        }
        else
        {
            InputPress = false;
        }
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
        if (direction.magnitude>=0.1f)
        { 
        float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg+cam.eulerAngles.y;
        float Angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref TurnSmothVelocity,TurnSmothTime);
        transform.rotation = Quaternion.Euler(0f, Angle, 0f);
        moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
        playerController.Move(moveDir*Speed*Time.deltaTime);
        } 
    }
    private void GetMoeventDirection()
    {
        if (LeftShipressed && InputPress)
        {
        Speed = RunSpeed;
            run = true;
            walk = false;
            idel = false;          
            PlayerAnimator.SetFloat("Velocity", run? AnimRun : AnimWalk, AnimationSmothSpeed, Time.deltaTime);
        }
        else if (InputPress)
        {
            Speed = WalkSpeed;
            run = false;
            walk = true;
            idel = false;
            PlayerAnimator.SetFloat("Velocity", walk ? AnimWalk : Animidel, AnimationSmothSpeed, Time.deltaTime);
        }
        else
        {
            run = false;
            walk = false;
            idel = true;
            PlayerAnimator.SetFloat("Velocity", idel ?Animidel : 0f, AnimationSmothSpeed, Time.deltaTime);
        }

    }

}



