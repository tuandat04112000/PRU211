using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.PlayerScripts
{
    internal class JumpManager : MonoBehaviour
    {
        public bool isGround;

        public LayerMask GroundLayer;
        public Transform GroundDetectorLeft;
        public Transform GroundDetectorRight;
        public float sensorRadius = 0.15f;

        PlayerManager player_manager;
        Rigidbody2D playerBody;

        // Jump Control
        int jumpLevel = 0;
        int jumpMpNeededBase = 10;
        int maxJumpLevel = 2;
        bool jumpSignal = false;
        public float JumpPower = 16;
        public float JumpBaseUnit = 50;
        int MpNeededByJump => jumpLevel * jumpMpNeededBase;

        public bool falling => playerBody.velocity.y < 0;


        bool CanJump
        {
            get
            {
                if (MpNeededByJump > player_manager.Current_MP) return false;
                if (isGround) return true;
                if (jumpLevel < maxJumpLevel) return true;
                return false;
            }
        }

        void JumpProcess()
        {
            if (CanJump && jumpSignal)
            {
                Debug.Log($"JumpManager: Jump level {jumpLevel + 1} active...");
                float yVal = JumpPower * Time.fixedDeltaTime * JumpBaseUnit;
                playerBody.velocity = new Vector2(playerBody.velocity.x, playerBody.velocity.y + yVal);
                player_manager.Current_MP -= MpNeededByJump;
                jumpLevel += 1;
                jumpSignal = false;
            }
        }

        void JumpDetection()
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                jumpSignal = true;
            }

        }

        void GroundDetection()
        {
            isGround = Physics2D.OverlapCircle(GroundDetectorLeft.position, sensorRadius, GroundLayer)
                || Physics2D.OverlapCircle(GroundDetectorRight.position, sensorRadius, GroundLayer);
            if (isGround) jumpLevel = 0;
        }

        private void Start()
        {
            player_manager = GetComponent<PlayerManager>();
            playerBody = GetComponent<Rigidbody2D>();

            if(player_manager == null || playerBody == null)
            {
                throw new Exception("JumpManager: Missing Component!");
            }

            
        }

        private void Update()
        {
            GroundDetection();
            JumpDetection();
        }

        private void FixedUpdate()
        {
            JumpProcess();
        }


    }
}
