using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrintPercent : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int MaxScore = 100;
        int score = 20;
        float percent = score * 100 / MaxScore;
        Debug.Log(percent + "%");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
