using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.AttackerScripts
{
    internal class MoveManager : MonoBehaviour
    {

        public float SpeedToMove = 6;
        public float TimeToDestroy = 0.8f;
        public float Timer = 0;

        Rigidbody2D Rigidbody;
        Animator Animator;

        private void Start()
        {
            Rigidbody = GetComponent<Rigidbody2D>();
            Animator = GetComponent<Animator>();
        }

        private void Update()
        {
            Vector2 direction = new Vector2(transform.localScale.x, 0).normalized;
            Rigidbody.AddForce(direction * SpeedToMove);
            if (Timer < TimeToDestroy) Timer += Time.deltaTime;
        }

        private void FixedUpdate()
        {
            if(Timer >= TimeToDestroy)
            {
                this.Destroy();
            }
        }

        public void Destroy()
        {
            Destroy(gameObject);
        }
        public void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Bunny")
            {
                Destroy(collision.gameObject);
                Destroy();
            }
        }

    }
}
