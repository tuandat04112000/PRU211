using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartSystem : MonoBehaviour
{

    public GameObject[] hearts;
    //public int life;
    private int life;   
    private bool dead;

    private void Start()
    {
        life = hearts.Length;
    }

    void Update()
    {
        //if (life < 1)
        //{
        //    Destroy(hearts[0].gameObject);
        //}
        //else if (life < 2)
        //{
        //    Destroy(hearts[1].gameObject);
        //}
        //else if (life < 3)
        //{
        //    Destroy(hearts[2].gameObject);
        //}

        if(dead == true)
        {
            Debug.Log("We Are Dead!!!");
        }

    }

    public void TakeDamage(int d)
    {
        if(life >= 1)
        {
            life -= d;
            Destroy(hearts[life].gameObject);
            if (life < 1)
            {
                dead = true;
            }
        }
        
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Bunny")
        {
            TakeDamage(1);
        }
    }
}
