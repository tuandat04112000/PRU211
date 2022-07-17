using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour
{
    public Transform Target;
    public Vector3 Offset;
    
    [Range(1, 10)]
    public float smooth;

    void Start() 
    {
        smooth = 10;
    }

    void FixedUpdate() 
    {
        Follow();
    }

    void Follow()
    {
        Vector3 targetPosition = Target.position + Offset;
        Vector3 smoothPosition = Vector3.Lerp(transform.position, targetPosition, smooth * Time.fixedDeltaTime);
        transform.position = smoothPosition;
    }

}
