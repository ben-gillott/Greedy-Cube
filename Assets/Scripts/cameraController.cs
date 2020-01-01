using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraController : MonoBehaviour
{
    public float dampTime = 0.15f;
    private Vector3 velocity = Vector3.zero;
    public Transform target;


    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Player").transform;
    }

     // Update is called once per frame
     void Update () 
     {
        //Player location
        Vector3 point = new Vector3(target.position.x, target.position.y, transform.position.z);

        // Vector3 delta = target.position - new Vector3(0.5f, 0.5f, transform.position.z);
        // Vector3 destination = transform.position + delta;

        transform.position = Vector3.SmoothDamp(transform.position, point, ref velocity, dampTime);     
     }
}
