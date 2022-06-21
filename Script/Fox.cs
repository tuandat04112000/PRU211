using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation
{
    public static float IdleAnimation = 0;
    public static float WalkAnimation = 1;
    public static float RunAnimation = 2;
    public static float JumpUpAnimation = 3;
    public static float JumpDownAnimation = 4;
}
public class Fox : MonoBehaviour
{

    //Rigidbody2D body;
    //Animator animator;
    //// Start is called before the first frame update
    //void Start()
    //{
    //    body = GetComponent<Rigidbody2D>();
    //    animator = GetComponent<Animator>();
    //}
    //void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.W))
    //    {
    //        Debug.Log("Flyyyy....");
    //        body.velocity = new Vector3(0, 10, 0);
    //        // animator.SetFloat("Blend", 0);

    //    }
    //    else if (Input.GetKeyUp(KeyCode.W))
    //    {
    //        Debug.Log("....");
    //        body.velocity = new Vector3(0, 0, 0);
    //        // animator.SetFloat("Blend", 2);

    //    }
    //    else if (Input.GetKeyUp(KeyCode.Q))
    //    {
    //        Debug.Log("....");
    //        body.velocity = new Vector3(0, 0, 0);
    //        //animator.SetFloat("Blend", 0);

    //    }

    //    if (Input.GetKeyDown(KeyCode.A))
    //    {
    //        Debug.Log("Move Left....");
    //        body.velocity = new Vector3(-5, 0, 0);
    //        // animator.SetFloat("Blend", 1);
    //    }

    //    if (Input.GetKeyDown(KeyCode.D))
    //    {
    //        Debug.Log("Move Right....");
    //        body.velocity = new Vector3(5, 0, 0);
    //        //animator.SetFloat("Blend", 1);

    //    }
    //    Vector3 eulerRotation = transform.rotation.eulerAngles;
    //    transform.rotation = Quaternion.Euler(eulerRotation.x, eulerRotation.y, 0);
    //}

    // Components
    Rigidbody2D PlayerBody;
    Animator PlayerAnimator;
    Transform GroundDetector;

    public float BaseSpeed = 1;
    private int BaseUnit = 50;
    float FixedSpeedRuntime() => Time.fixedDeltaTime * BaseSpeed * BaseUnit;
    float SpeedRuntime() => Time.deltaTime * BaseSpeed * BaseUnit;

    [SerializeField]
    float DirectionSprite = 1;
    float AnimationState = PlayerAnimation.IdleAnimation;

    [SerializeField]
    float moveToX = 0;
    [SerializeField]
    bool isGround = true;
    [SerializeField]
    int jumpLevel = 0;
    int maxJumpLevel = 2;
    [SerializeField]
    float JumpPower = 8;

    void MoveProcess()
    {
        Debug.Log("MoveProcess");
        if (PlayerBody == null || moveToX == 0) return;
        float xVal = moveToX * FixedSpeedRuntime();
        PlayerBody.velocity = new Vector2(xVal, PlayerBody.velocity.y);
    }

    void FaceDirectionProcess()
    {
        if (DirectionSprite != 0)
        {
            float baseScaleX = Mathf.Abs(transform.localScale.x);
            transform.localScale = new Vector3(DirectionSprite * baseScaleX, transform.localScale.y, transform.localScale.z);
            Vector3 eulerRotation = transform.rotation.eulerAngles;
             transform.rotation = Quaternion.Euler(eulerRotation.x, eulerRotation.y, 0);
        }
    }

    void AnimationProcess()
    {
        PlayerAnimator.SetFloat("state", AnimationState);
    }

    void JumpProcess()
    {
        Debug.Log("Jump Process");
        if (PlayerBody == null || !isGround || jumpLevel == 0) return;
        if (jumpLevel > 0)
        {
            Debug.Log("Jump Process active...");
            float yVal = JumpPower * Time.fixedDeltaTime * BaseUnit;
            PlayerBody.velocity = new Vector2(PlayerBody.velocity.x, PlayerBody.velocity.y + yVal);
            jumpLevel -= 1;
        }
    }

    void FaceDetection()
    {
        DirectionSprite = Input.GetAxisRaw("Horizontal");
    }

    void MoveDetection()
    {
        moveToX = Input.GetAxis("Horizontal");

        UpdateAnimationState();
    }

    void UpdateAnimationState()
    {
        if (moveToX != 0)
        {
            AnimationState = PlayerAnimation.WalkAnimation;
        }
        else
        {
            AnimationState = PlayerAnimation.IdleAnimation;
        }
    }

    void JumpDetection()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (jumpLevel < maxJumpLevel) jumpLevel += 1;
        }
    }

    void GroundDetection()
    {

    }


    void Awake()
    {
        PlayerBody = GetComponent<Rigidbody2D>();
        PlayerAnimator = GetComponent<Animator>();
    }

    void Start()
    {

    }

    void Update()
    {
        JumpDetection();
        MoveDetection();
        FaceDetection();
    }

    void FixedUpdate()
    {
        JumpProcess();
        MoveProcess();
        FaceDirectionProcess();
        AnimationProcess();
    }
}
