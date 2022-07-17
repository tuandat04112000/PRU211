using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{

    public Transform FlipSensor_01;
    public Transform FlipSensor_02;
    public LayerMask GroundLayer;

    Rigidbody2D _rigidbody2D;

    // Speed control
    public float BaseSpeed = 3;
    private int BaseUnit = 50;
    float FixedSpeedRuntime => Time.fixedDeltaTime * BaseUnit;
    float SpeedRuntime => Time.deltaTime * BaseSpeed * BaseUnit;

    float horizontalAxis = 1;

    public bool isMoving => horizontalAxis != 0;

    public float radius = 20;


    void MoveProcess()
    {
        float xVal = -horizontalAxis * SpeedRuntime * (BaseSpeed);
        _rigidbody2D.velocity = new Vector2(xVal, _rigidbody2D.velocity.y);

        transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * (horizontalAxis), transform.localScale.y, transform.localScale.z);
    }

    void FlipDetection()
    {
        bool isFlip = !Physics2D.OverlapCircle(FlipSensor_01.position, 0.1f, GroundLayer)
            || Physics2D.OverlapCircle(FlipSensor_02.position, 0.1f, GroundLayer);

        if(isFlip)
        {
            horizontalAxis *= -1;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();

        if(_rigidbody2D == null)
        {
            Debug.Log("Bunny: Missing component!");
        }

        
    }

    // Update is called once per frame
    void Update()
    {
        
        FlipDetection();
        MoveProcess();
    }
}
