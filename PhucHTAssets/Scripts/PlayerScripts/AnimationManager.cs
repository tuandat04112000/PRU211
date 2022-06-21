using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.PlayerScripts
{
    public class AnimationManager : MonoBehaviour
    {

        public enum AnimationCode
        {
            Idle_01,
            Idle_02,
            Walk,
            Walk_Shield,
            Run,
            Run_Shield,
            Sword_Attack,
            Shield_Attack,
            Buff_01,
            Buff_02,
            Buff_03,
            Hit,
            Dead,
            JumpUp,
            JumpDown,
        }

        Animator _animator;
        public AnimationCode CurrentAnimationCode = AnimationCode.Idle_01;

        MoveManager _moveManager;
        JumpManager _jumpManager;
        AttackManager _attackManager;

        private void Start()
        {
            _animator = GetComponent<Animator>();
            _moveManager = GetComponent<MoveManager>();
            _jumpManager = GetComponent<JumpManager>();
            _attackManager = GetComponent<AttackManager>();
            if(_animator == null || _moveManager == null || _jumpManager == null || _attackManager == null)
            {
                throw new Exception("AnimationManager: Misssing component!");
            }
        }

        private void Update()
        {
            if(_attackManager.IsAttackAnimating)
            {
                if(CurrentAnimationCode != AnimationCode.Sword_Attack) CurrentAnimationCode = AnimationCode.Sword_Attack;
            }
            else if(!_jumpManager.isGround)
            {
                if (!_jumpManager.falling) CurrentAnimationCode = AnimationCode.JumpUp;
                else CurrentAnimationCode = AnimationCode.JumpDown;
            }
            else if(_moveManager.isRunning)
            {
                CurrentAnimationCode = AnimationCode.Run_Shield;
            }
            else if(_moveManager.isMoving)
            {
                CurrentAnimationCode = AnimationCode.Walk_Shield;
            }
            else
            {
                CurrentAnimationCode = AnimationCode.Idle_02;
            }

            
        }

        private void FixedUpdate()
        {
            _animator.SetFloat("Blend", (float) CurrentAnimationCode);
        }

    }
}
