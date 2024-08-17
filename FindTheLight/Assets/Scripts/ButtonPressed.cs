using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;


public class ButtonPressed : MonoBehaviour
{
    public GameObject[] buttons;
    public GameObject button;
    public Transform player;
    public Sprite buttonPressed;
    public SpriteRenderer spriteRenderer;


    [SerializeField]
    private Light2D mylight;

    [SerializeField] public  GameManager gM;

    private void Start()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            if(mylight.enabled == true)
            {
                //do nothing
            }
            else if (mylight.enabled == false)
            {
                Debug.Log("switch on");
                mylight.enabled = true;
                spriteRenderer.sprite = buttonPressed;
                

                //CheckAllButtons();
                
            }
            

        }
    }


}
