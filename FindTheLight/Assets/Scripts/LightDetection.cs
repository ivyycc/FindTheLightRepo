using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightDetection : MonoBehaviour
{
    //Collider2D[] lightColliders = new Collider2D[10];

    //public static LightDetection Instance { get; private set; }

    //public bool LightHasHit { get; set; }
    // public event Action<bool> LightHitEvent;

    [SerializeField] private Transform roof;

    public bool LightHasHit;


    [SerializeField] private Light2D mylight;
    private void Start()
    {
        mylight = GetComponent<Light2D>();
    }
    private void Update()
    {

        /*if(mylight.enabled == true)
        {
            this.GetComponent<PolygonCollider2D>().enabled = true;
            Debug.Log("light enabled");
        }*/

    
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(mylight.enabled == true)
        {
            //Debug.Log("collision happening: " + collision.tag);
            if (collision.gameObject.CompareTag("roof1"))
            {
                //LightHitEvent?.Invoke(true);
                LightHasHit = true;
                //Debug.Log("first roof detected");
            }
        }
        
    }


    /*
         // int numColliders = Physics2D.OverlapCollider(GetComponent<Collider2D>(), new ContactFilter2D(), lightColliders);

          // if (numColliders > 0)
          // {
          // Object is currently lit
          //  Debug.Log("Object is lit!");
          // }*/


}
