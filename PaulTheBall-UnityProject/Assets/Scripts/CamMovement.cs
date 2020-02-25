using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMovement : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        if (!target)
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }
    
    void Update()
    {
        transform.position = target.position + offset;
    }
}
