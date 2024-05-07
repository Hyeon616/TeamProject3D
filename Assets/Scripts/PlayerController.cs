using Cinemachine;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera aimCam;

    public PlayerInput playerInput;
    public GameObject grenadePrefab;
    public Transform throwPoint;
    public AudioSource audioSource;
    public AudioClip fireSound;

    public float BasePlayerHp = 10.0f;
    public int currentBullet = 30;//쏘고 남은 현재 총알 개수 

    public int maxBullet = 100;//예비 총알 개수
    public int currentBulletTemp = 30;//장전 시 30개 채워지도록 하는 변수
    public int currentgrenade = 3;

    private float moveSpeed = 2.0f;
    private float sprintSpeed = 4.0f;

    private Vector3 moveInput;

    private float speed;

    public bool sprint;
    public bool fire;
    public bool reload;

    public Vector3 move;

    public float jumpHeight = 2.0f;
    public float timeToJumpApex = 0.4f;
    private float gravity;
    private float jumpVelocity;
    private bool isJumping = false;
    private float jumpStartTime;

    [SerializeField] private float rotationSpeed = 0.8f;

    private Animator anim;
    private CharacterController cc;
    [SerializeField] private Transform camTransform;


    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        anim = GetComponent<Animator>();
        cc = GetComponent<CharacterController>();
        audioSource = GetComponent<AudioSource>();

        audioSource.clip = fireSound;

        gravity = -(2 * jumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        jumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;

        camTransform = Camera.main.transform;

    }

    void Update()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Move();
        Jump();
        Zoom();
        ThrowGrenade();
        Debug.Log($"캐릭터 HP : {BasePlayerHp}");
    }

    private void Zoom()
    {
        playerInput.actions["Zoom"].performed += ctx =>
        {
            if (Mouse.current.rightButton.isPressed && aimCam.m_Lens.FieldOfView == 60)
            {
                aimCam.m_Lens.FieldOfView = 30;
            }
            else { aimCam.m_Lens.FieldOfView = 60; }
        };
    }

    private void ThrowGrenade()
    {
        playerInput.actions["Toss"].performed += ctx =>
        {
            if (Keyboard.current.eKey.isPressed && currentgrenade != 0)
            {
                StartCoroutine(Active());
            }
            else { Debug.Log("폭탄 없음"); }
        };
    }

    private IEnumerator Active()
    {
        GameObject obj = GameObject.Find("WeaponHolder");
        obj.SetActive(false);
        anim.SetTrigger("Toss");
        Invoke("Toss", 2f);
        yield return new WaitForSeconds(3);
        obj.SetActive(true);
    }

    private void Toss()
    {
        GameObject grenand = Instantiate(grenadePrefab, throwPoint.position, throwPoint.rotation);
        currentgrenade -= 1;
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
        if (reload == true)
            return;
        if (currentBullet > 0)
        {
            FireInput(inputValue.isPressed);
            anim.SetTrigger("Fire");
            audioSource.PlayOneShot(fireSound);
            RaycastHit hit;

            currentBullet -= 1;

            Vector3 targetPosition = Vector3.zero;

            if (Physics.Raycast(camTransform.position, camTransform.forward, out hit, Mathf.Infinity))
            {
                targetPosition = hit.point;

                IDamageable damageAble = hit.transform.gameObject.GetComponent<IDamageable>();
                if (damageAble != null)
                {

                    if (gameObject.layer == 8)
                        damageAble.Damage(1);
                    else if (gameObject.layer == 9)
                        damageAble.Damage(2);
                    else if (gameObject.layer == 10)
                        damageAble.Damage(5);

                }
            }
            if (currentBullet == 0)
            {
                OnReload();
            }
        }
    }
    public void OnReload()
    {
        if (maxBullet > 0)
        {
            reload = true;
            anim.SetTrigger("Reload");
            Invoke("BulletDelay", 3);
        }

    }

    public void BulletDelay()
    {
        reload = false;
        //총알 몇 개 쐈는지 계산해서 bulletsToReload에 넣음
        int bulletsToReload = currentBulletTemp - currentBullet;
        //계산된 개수 currentBullet 여기 추가해주고
        currentBullet += bulletsToReload;
        //추가한만큼 예비 탄창에서 빼기
        maxBullet -= bulletsToReload;
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
    public void bulletUp(int v)
    {
        maxBullet += v;
        currentBullet = currentBulletTemp;
    }
    public void hpUp(int v)
    {
        maxBullet += v;
    }
}
