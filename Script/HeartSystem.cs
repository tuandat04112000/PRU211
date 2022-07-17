using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
            if (life <= 0 )
            {
                dead = true;
                Scene scene = SceneManager.GetActiveScene();
                SceneManager.LoadScene(scene.name);
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

 

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bunny")
        {
            TakeDamage(1);
        }
    }
}
