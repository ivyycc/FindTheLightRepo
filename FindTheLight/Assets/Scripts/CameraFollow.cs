using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;
    public float smoothSpeed = 0.125f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void LateUpdate()
    {
        Vector3 desiredpos = player.position + offset;
        Vector3 smoothPos = Vector3.Lerp(transform.position, desiredpos, smoothSpeed*Time.deltaTime);
        transform.position = smoothPos;
    }
}
