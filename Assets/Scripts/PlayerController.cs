using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private CinemachineVirtualCamera aimCam;

    private float moveSpeed = 2.0f;
    private float sprintSpeed = 4.0f;

    private Vector3 moveInput;

    private float speed;

    public bool sprint;
    public bool fire;
    public bool reload;

    public Vector3 move;

    [SerializeField] private LayerMask targetLayer;
    public float jumpHeight = 2.0f;
    public float timeToJumpApex = 0.4f; 
    private float gravity;
    private float jumpVelocity;
    private bool isJumping = false;
    private float jumpStartTime;

    [SerializeField] private float rotationSpeed = 0.8f;

    private Animator anim;
    private CharacterController cc;


    private Transform camTransform;


    void Start()
    {
        anim = GetComponent<Animator>();
        cc = GetComponent<CharacterController>();


        gravity = -(2 * jumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        jumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;

        camTransform = Camera.main.transform;

    }

    void Update()
    {
        Move();
        Jump();
        
    }

    private void Move()
    {

        float targetSpeed = sprint ? sprintSpeed : moveSpeed;

        if (move == Vector3.zero)
        {
            targetSpeed = 0f;
        }

        speed = targetSpeed;

        moveInput = new Vector3(move.x, 0, move.z);

        cc.Move(moveInput * speed * Time.deltaTime);

        if (sprint)
        {
            anim.SetFloat("XSpeed", move.x);
            anim.SetFloat("YSpeed", move.z);
        }
        else
        {
            anim.SetFloat("XSpeed", move.x / 2);
            anim.SetFloat("YSpeed", move.z / 2);
        }

        Quaternion targetRotation = Quaternion.Euler(0, camTransform.eulerAngles.y, 0);
        
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    private void Jump()
    {
        isJumping = true;
        jumpStartTime = Time.time;

        if (isJumping)
        {
            float timeSinceJumpStart = Time.time - jumpStartTime;
            if (timeSinceJumpStart < timeToJumpApex)
            {
                float jumpForce = jumpVelocity + gravity * timeSinceJumpStart;
                transform.Translate(Vector3.up * jumpForce * Time.deltaTime);
            }
            else
            {
                isJumping = false;
            }
        }
    }



    public void OnMove(InputValue inputValue)
    {
        MoveInput(inputValue.Get<Vector3>());
    }

    public void OnJump(InputValue inputValue)
    {
        JumpInput(inputValue.isPressed);
    }

    public void OnSprint(InputValue inputValue)
    {
        SprintInput(inputValue.isPressed);
    }

    public void OnFire(InputValue inputValue)
    {
        FireInput(inputValue.isPressed);
        anim.SetTrigger("Fire");
        RaycastHit hit;

        Vector3 targetPosition = Vector3.zero;

        if (Physics.Raycast(camTransform.position, camTransform.forward, out hit, Mathf.Infinity, targetLayer))
        {
            targetPosition = hit.point;

            IDamageable damageAble = hit.transform.gameObject.GetComponent<IDamageable>();
            if (damageAble != null)
                damageAble.Damage(1);
            
        }
    }

    public void OnReload(InputValue inputValue)
    {
        ReloadInput(inputValue.isPressed);
        anim.SetTrigger("Reload");
    }

    public void MoveInput(Vector3 moveInput)
    {
        move = moveInput;
    }

    public void JumpInput(bool isJump)
    {
        isJumping = isJump;

    }

    public void SprintInput(bool isSprint)
    {
        sprint = isSprint;
    }

    public void FireInput(bool isFire)
    {
        fire = isFire;
    }

    public void ReloadInput(bool isReload)
    {
        reload = isReload;
    }

    private bool IsGrounded()
    {
        RaycastHit hit;
        float raycastDistance = 0.1f;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, raycastDistance))
        {
            return true;
        }
        return false;
    }
}
