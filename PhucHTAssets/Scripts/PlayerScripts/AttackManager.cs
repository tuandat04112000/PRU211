using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.PlayerScripts
{
    internal class AttackManager : MonoBehaviour
    {

        Animator animator;
        AnimationManager animationManager;

        bool isLoaded = false;

        private bool AttackSignal;
        public bool IsAttackAnimating;
        
        public bool CanAttack;

        float attackFrequencePerSecond = 2;
        float attackTimer = 0;

        public GameObject attackPrefab;

        private void Start()
        {
            animator = GetComponent<Animator>();
            animationManager = GetComponent<AnimationManager>();
            if(animator == null || animationManager == null)
            {
                throw new Exception("AttackManager: Component not found.");
            }
            isLoaded = true;
            AttackSignal = false;
            IsAttackAnimating = false;
            CanAttack = true;
        }

        private void Update()
        {

            if(Input.GetKeyDown(KeyCode.Space))
            {
                AttackSignal = true;
            }
            if(Input.GetKeyUp(KeyCode.Space))
            {
                AttackSignal = false;
            }

            attackTimer += Time.deltaTime;
            if (AttackSignal && attackTimer >= (1 / attackFrequencePerSecond) && CanAttack && !IsAttackAnimating)
            {
                IsAttackAnimating = true;
                attackTimer = 0;
            }

        }

        private void FixedUpdate()
        {
            
        }

        private void Attack()
        {
            GameObject attacker = Instantiate(attackPrefab);
            attacker.transform.position = transform.position;
            attacker.transform.localScale = transform.localScale;
        }

        private void AttackDone()
        {
            IsAttackAnimating = false;
            AttackSignal = false;
        }

    }
}
