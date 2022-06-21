using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnlickYes()
    {
        Debug.Log("exit");
        Quit();
            
    }
    public void OnlickNo()
    {
        SceneManager.LoadScene("MenuScene");
    }
    public void Quit()
    {
            #if UNITY_STANDALONE
                    Application.Quit();
            #endif
            #if UNITY_EDITOR
                    UnityEditor.EditorApplication.isPlaying = false;
            #endif
    }
}
