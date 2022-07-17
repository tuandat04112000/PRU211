using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AppleCounter : MonoBehaviour
{

    public GameObject player;
    public int apple;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        apple = GetComponent<PlayerInventory>().NumberOfDiamonds;
        GetComponent<Text>().text = $"NumberOfApples: {apple}";
    }
}
