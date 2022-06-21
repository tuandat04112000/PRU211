using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonExit : MonoBehaviour
{
    // Start is called before the first frame update
    public void OnClickButtonStart()
    {
        SceneManager.LoadScene("Nature_assets");
        Debug.Log("The game is startted");
    }
    public void OnClickButtonLoadGame()
    {
        Debug.Log("The game is Loaded");
    }
    public void OnClickButtonExit()
    {
        Debug.Log("The game is exitted");
        SceneManager.LoadScene("ExitScene");
    }
}
