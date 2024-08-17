using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
//using FMODUnity;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour
{
    [field: Header("Player main movement")]
    public Rigidbody2D player_rb;
    public float player_speed = 10f;
    public float input;
    public SpriteRenderer spriterend;
    private Vector2 targetVelocity;
   

    [field: Header("Player jump")]
    public LayerMask groundLayer;
    public bool isGrounded;
    public Transform feetpos;
    public float groundCircle;
    public int jumpCount = 0;
    public float jumpForce = 5;
    private int maxJumps = 2;

    [field: Header("Sounds")]
    private bool isSoundPlayed = false;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject pauseButton;
    [SerializeField] private string paramName;
    [SerializeField] private int paramVal;

    [field: Header("Player Fall")]
    [SerializeField] private Vector3 respawnPoint;
    public GameObject fallDetector;
    [SerializeField] private float offset = -6f;

    [field: Header("Buttons")]
    public bool allButtonsPressed = true;
    public Sprite[] buttonPressed;
    public SpriteRenderer[] spriteRenderer;
    public GameObject[] Tilemap;
    public SpriteRenderer doorRenderer;
    public Sprite doorOpen;

    [field: Header("Lighting")]
    [SerializeField] private Light2D[] mylight;
   // [SerializeField] private LightDetection script;
    public bool boolean;
    public bool boolean2;
    //[SerializeField] private LightDetection check2;

    [field: Header("Managers")]
    [SerializeField] public GameManager gM;

    // Start is called before the first frame update
    void Start()
    {
        player_rb = GetComponent<Rigidbody2D>();
        player_rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        spriterend = GetComponent<SpriteRenderer>();

        respawnPoint = transform.position;
        fallDetector.transform.position = new Vector2(fallDetector.transform.position.x, fallDetector.transform.position.y + offset);

        PlayerIsIdle.Timeout = 3;
        if (gM.getCurrentLevel() == 6)
        {
            boolean = mylight[0].GetComponent<LightDetection>().LightHasHit;
        }
            
        
       // LightDetection.Instance.LightHitEvent += HandleLightHit;
        //AudioManager.instance.SetAmbienceParam(paramName, 0);
    }

    /*void HandleLightHit(bool lightHit)
    {
        boolean = lightHit;
    }*/


    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Cancel"))
        {
            //pause Game
            pauseMenu.SetActive(true);
            pauseButton.SetActive(false);

        }
        input = Input.GetAxisRaw("Horizontal");
        if (input < 0)
        {
            PlayerIsIdle.ReportAction();
            spriterend.flipX = true;
        }
        else if (input > 0)
        {
            PlayerIsIdle.ReportAction();
            spriterend.flipX = false;
        }


        isGrounded = Physics2D.OverlapCircle(feetpos.position, groundCircle, groundLayer); // check if we are grounded 

        if (gM.getCurrentLevel() > 4)//activate double jump
        {
            //Debug.Log(gM.getCurrentLevel());
            if (jumpCount > 0 && Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.W))
            {
                PlayerIsIdle.ReportAction();
                AudioManager.instance.PlayOneShot(FMODEvents.instance.playerJump, this.transform.position);
                player_rb.velocity = Vector2.up * jumpForce;
                jumpCount -= jumpCount;
                
                /*if (jumpCount > 0)
                {
                    jumpCount = 0;
                    isGrounded = false;
                }
                 jumpCount++;*/  
            }
            if (isGrounded)
            {
                jumpCount = maxJumps;
            }
        }
        else
        {
            if (isGrounded == true && Input.GetButtonDown("Jump"))//jump normally
            {
                PlayerIsIdle.ReportAction();
                player_rb.velocity = Vector2.up * jumpForce;
                AudioManager.instance.PlayOneShot(FMODEvents.instance.playerJump, this.transform.position);

            }
        }

        targetVelocity = new Vector2(input * player_speed, player_rb.velocity.y);

        fallDetector.transform.position = new Vector2(transform.position.x, fallDetector.transform.position.y);//follows player on the x axis but remains on the same y position

        if(PlayerIsIdle.IsIdle == true)
        {
            //play cat sound
            //AudioManager.instance.PlayOneShot(idleSound, this.transform.position);
            //isSound = true;
        }

        if (gM.getCurrentLevel() == 6)
        {
            boolean = mylight[0].GetComponent<LightDetection>().LightHasHit;
            boolean2 = mylight[1].GetComponent<LightDetection>().LightHasHit;
        }




    }



    private void FixedUpdate()
    {
        player_rb.velocity = Vector2.Lerp(player_rb.velocity, targetVelocity, 0.06f);

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("FallDetector"))
        {
            AudioManager.instance.PlayOneShot(FMODEvents.instance.playerfalls, this.transform.position);
            StartCoroutine(Wait());
            Debug.Log("done");
            transform.position = respawnPoint;//player respawns at the start
        }

        if (collision.CompareTag("spikes"))
        {
            // if(allButtonsPressed == true)
            //{
            //    transform.position = new Vector2(collision.transform.position.x + 2, collision.transform.position.y);
            //}
            // else
            // {
            //player restarts level
            AudioManager.instance.PlayOneShot(FMODEvents.instance.playerdies, this.transform.position);
            transform.position = respawnPoint;
            //  }

        }

        if (collision.gameObject.CompareTag("door"))
        {
            if(gM.getCurrentLevel() == 6)
            {
                gM.MainMenu();
            }
            else
            {
                gM.LoadNextScene();
            }
            
        }

        if(player_rb.velocity.magnitude ==0)
        {
            Debug.Log("PLAYER IS IDLE");
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {

        switch (collision.gameObject.tag)
        {
            case "button1":
                if (mylight[0].enabled == true)
                {

                }
                else if (mylight[0].enabled == false)
                {
                    Debug.Log("switch on");
                    mylight[0].enabled = true;
                    AudioManager.instance.SetAmbienceParam(paramName, paramVal);
                    //Debug.Log(boolean);
                    spriteRenderer[0].sprite = buttonPressed[0];
                    AudioManager.instance.PlayOneShot(FMODEvents.instance.buttonPress, this.transform.position);
                    Debug.Log("button sound");
                    OpenDoor();
                    //AllButtonsPressed();
                }
                break;

            case "button2":
                if (mylight[1].enabled == true)
                {

                }
                else if (mylight[1].enabled == false)
                {
                    Debug.Log("switch on");
                    mylight[1].enabled = true;
                    AudioManager.instance.SetAmbienceParam(paramName, paramVal);
                   // Debug.Log(check2.LightHasHit);
                    spriteRenderer[1].sprite = buttonPressed[1];
                    AudioManager.instance.PlayOneShot(FMODEvents.instance.buttonPress, this.transform.position);
                    Debug.Log("button sound");
                    AllButtonsPressed();
                }
                break;

        }
    }

    private void AllButtonsPressed()
    {
        bool allButtonsPressed = true;

        for (int i = 0; i < mylight.Length; i++)
        {
            if (mylight[i].enabled == false)
            {
                allButtonsPressed = false;
                break;
            }

        }

        if (allButtonsPressed == true)
        {
            OpenDoor();
        }
    }

    public void OpenDoor()
    {
        if(gM.getCurrentLevel()==6)
        {
            if (boolean == false)
            {
                Debug.Log(boolean);
                Tilemap[0].SetActive(false);//closed platform deactived
                Tilemap[1].SetActive(true);//open1 platform activated
            }
            else if (boolean2 == false)
            {
                Tilemap[1].SetActive(false);//open1 platform deactived
                Tilemap[2].SetActive(true);//open2 platform activated
                doorRenderer.sprite = doorOpen;
                AudioManager.instance.PlayOneShot(FMODEvents.instance.doorOpen, this.transform.position);
            }
            
        }
        else
        {
            Tilemap[0].SetActive(false);//closed platform deactived
            Tilemap[1].SetActive(true);//open platform activated
            doorRenderer.sprite = doorOpen;
            AudioManager.instance.PlayOneShot(FMODEvents.instance.doorOpen, this.transform.position);
        }
        
    }

    IEnumerator Wait()
    {
        // Wait for 2 sec before respawning

        yield return new WaitForSecondsRealtime(2);

    }
 }
