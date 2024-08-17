using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatforms : MonoBehaviour
{
    public int startPoint; // The starting point of the platform
    public Transform[] points;   // The ending point of the platform
    public float speed = 3f;     // Movement speed
    private int i;
    public float offset = 0.7f;

    //private Vector3 targetPosition;
    //public bool movingToEnd = true;

    private void Start()
    {
        transform.position = points[startPoint].position;
        AudioManager.instance.PlatformMove();
    }

    private void Update()
    {
       
        // Check if the platform has reached the target
        if (Vector3.Distance(transform.position, points[i].position) < 0.1f)
        {
            //Debug.Log(Vector3.Distance(transform.position, points[i].position));
            i++;
            if (i == points.Length)
            {
                i = 0;
            }
        }

            //move to the end target
        transform.position = Vector2.MoveTowards(transform.position, points[i].position, speed * Time.deltaTime);
         
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            if(transform.position.y<collision.transform.position.y-offset)
            {
                collision.transform.SetParent(transform);
            }
            
        }

        

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(null);
        }
            
    }

}
