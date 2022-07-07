using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScreen : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {

            if(gameObject.name == "Door2")
            {
                SceneManager.LoadScene("Map3");
            }    

            else if(gameObject.name == "Door1")
            {
                SceneManager.LoadScene("Map2");
            }

            else if(gameObject.name == "Door3") {
                SceneManager.LoadScene("EndGame");
            }
        }

    }

}
