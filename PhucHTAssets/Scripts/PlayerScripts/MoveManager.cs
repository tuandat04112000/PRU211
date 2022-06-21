using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.PlayerScripts
{
    public class MoveManager : MonoBehaviour
    {

        // Components
        Rigidbody2D PlayerBody;
        Animator PlayerAnimator;
        PlayerManager player_manager;

        // Speed control
        public float BaseSpeed = 3;
        private int BaseUnit = 50;
        float FixedSpeedRuntime => Time.fixedDeltaTime * BaseUnit;
        float SpeedRuntime => Time.deltaTime * BaseSpeed * BaseUnit;

        // Acceleration control
        public float acceleration = 1;
        bool accelSignal = false;
        float bonusSpeedByAccel = 0;
        float maxBonusSpeedByAccel => 2 * BaseSpeed;
        public int accelMpFrequencyPerSecond = 20;
        float accelMpLosses = 0;
        public bool isRunning => accelSignal && isMoving && player_manager.Current_MP > 0;

        float horizontalAxis = 0;

        public bool isMoving => horizontalAxis != 0;

        public Transform sideDetectorTop;
        public Transform sideDetectorBottom;
        public float sideDetectorRadius = 0.15f;
        public bool isSideColllapse;
        public LayerMask sideDetectorLayer;

        void SideCollapseProcess()
        {
            if(isSideColllapse)
            {
                Vector2 move = PlayerBody.velocity;
                move.x = 0;
                PlayerBody.velocity = move;
            }
        }

        void AccelProcess()
        {
            if (accelSignal && isMoving && player_manager.Current_MP > 0)
            {
                accelMpLosses += Time.fixedDeltaTime * accelMpFrequencyPerSecond;
                if (accelMpLosses >= 1)
                {
                    // player_manager.Curent_MP--;
                    accelMpLosses = 0;
                }
                Debug.Log("MoveManager: Accel Active...");
                bonusSpeedByAccel = Mathf.Min(bonusSpeedByAccel + acceleration, maxBonusSpeedByAccel);
            }
            else
            {
                bonusSpeedByAccel = 0;
            }
        }

        void MoveProcess()
        {
            if(isMoving) Debug.Log("MoveManager: Move active...");
            float xVal = horizontalAxis * FixedSpeedRuntime * (BaseSpeed + bonusSpeedByAccel);
            PlayerBody.velocity = new Vector2(xVal, PlayerBody.velocity.y);
        }

        void FaceProcess()
        {
            if(isMoving)
            {
                Vector3 face = gameObject.transform.localScale;
                face = new Vector3(Mathf.Abs(face.x) * horizontalAxis, face.y, face.z);
                gameObject.transform.localScale = face;
            }
        }

        void MoveDetection()
        {
            horizontalAxis = Input.GetAxisRaw("Horizontal");
        }

        void AccelDetection()
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                accelSignal = true;
            }
            if (Input.GetKeyUp(KeyCode.Tab))
            {
                accelSignal = false;
            }
        }

        void SideCollapseDetection()
        {
            isSideColllapse = Physics2D.OverlapCircle(sideDetectorBottom.position, sideDetectorRadius, sideDetectorLayer)
                || Physics2D.OverlapCircle(sideDetectorTop.position, sideDetectorRadius, sideDetectorLayer);
        }

        void Start()
        {
            PlayerBody = GetComponent<Rigidbody2D>();
            PlayerAnimator = GetComponent<Animator>();
            player_manager = GetComponent<PlayerManager>();
            if(player_manager == null || PlayerBody == null || PlayerAnimator == null)
            {
                throw new System.Exception("MoveManager: Missing Component!");
            }

        }

        void Update()
        {
            AccelDetection();
            MoveDetection();
            SideCollapseDetection();
        }

        void FixedUpdate()
        {
            AccelProcess();
            MoveProcess();
            FaceProcess();
            SideCollapseProcess();
        }
    }
}
