using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.PlayerScripts
{
    public class PlayerManager : MonoBehaviour
    {

        private float MAX_HP = 2000;
        private float MAX_MP = 300;
        private float MAX_SPEED = 15;
        private float BASE_SPEED = 8;
        private float BASE_DAMAGE = 20;

        private float INCREASE_MAX_HP = 0;
        private float INCREASE_MAX_MP = 0;
        private float INCREASE_MAX_DAMAGE_PERCENTAGE = 0;
        private float INCREASE_SPEED_PERCENTAGE = 0;

        public float Current_HP;
        public float Current_MP;
        [SerializeField]
        private float Current_DAMAGE;
        [SerializeField]
        private float Current_SPEED;
    

        private void Start()
        {
            if (!LoadPlayer())
            {
                Current_HP = MAX_HP;
                Current_MP = MAX_MP;
            }
        }

        private void Update()
        {

        }
        public void FixedUpdate()
        {
            MpRecovery();
        }

        bool LoadPlayer()
        {
            // Implement later
            return false;
        }
        public float MpRecoveryTime = 5f;
        public int MpRecoveryValue = 10;
        private float currentMpRecoveryTimer = 0;
        void MpRecovery()
        {
            currentMpRecoveryTimer -= Time.deltaTime;
            if (currentMpRecoveryTimer >= MpRecoveryTime)
            {
                if (Current_MP < MAX_MP)
                {
                    // Debug.Log("Mp Recovery...");
                    Current_MP = Mathf.Min(Current_MP + MpRecoveryValue, MAX_MP);
                }

            }
        }
    }
}
