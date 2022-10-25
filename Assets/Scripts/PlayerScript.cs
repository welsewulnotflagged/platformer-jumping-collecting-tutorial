using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D rd2d;

    public float speed;

    public Text score;

    public Text lives;

    public Text level;

    private int scoreValue = 0;

    private int livesValue = 3;

    private int levelNumber = 1;

    public GameObject winTextObject;

    public GameObject loseTextObject;

    private bool isOnGround;
    
    public Transform groundcheck;
    
    public float checkRadius;
    
    public LayerMask allGround;

    public GameObject stage1;

    public GameObject stage2;

    public AudioClip coinPickupAudio;

    public AudioClip enemyAudio;

    public AudioController audio;

    private Animator anim; 

    private bool facingRight = true;

    public GameObject playerObject;


    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        score.text = scoreValue.ToString();
        lives.text = livesValue.ToString();
        level.text = levelNumber.ToString();

        winTextObject.SetActive(false);
        loseTextObject.SetActive(false);
        stage1.SetActive(true);
        stage2.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        float hozMovement = Input.GetAxis("Horizontal");

        float vertMovement = Input.GetAxis("Vertical");

        isOnGround = Physics2D.OverlapCircle(groundcheck.position, checkRadius, allGround);

        rd2d.AddForce(new Vector2(hozMovement * speed*2, vertMovement * speed/16));

        if(hozMovement > 0 && facingRight == false) 
        {
            
            anim.SetInteger("State", 1);
            Flip();
        
        } 

        else if (facingRight == true && hozMovement < 0)

        {
            
            anim.SetInteger("State", 1);
            Flip ();
        }

        else if (hozMovement == 0) 
        {
           anim.SetInteger("State", 0); 
        }
        
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
       if (collision.collider.tag == "Coin")
        {
            scoreValue += 1;
            score.text = scoreValue.ToString();
            AudioSource.PlayClipAtPoint(coinPickupAudio,transform.position);
            Destroy(collision.collider.gameObject);
            SetCountText();
        }

        if (collision.collider.tag == "Enemy")
        {
            livesValue -= 1;
            lives.text = livesValue.ToString();
            AudioSource.PlayClipAtPoint(enemyAudio,transform.position);
            Destroy(collision.collider.gameObject);
            SetCountText();
            anim.Play("Base Layer.Hurt", 0, 0); 
        }
    }

    void Flip()
   {
        facingRight = !facingRight;
        Vector2 Scaler = transform.localScale;
        Scaler.x = Scaler.x * -1;
        transform.localScale = Scaler;
   }

   void PlayerReset() 
    { 
			
            speed = 0;
            rd2d.AddForce(new Vector2(0,0));
 
    }

 
    private void OnCollisionStay2D(Collision2D collision)
    {

        if (collision.collider.tag == "Ground" && isOnGround )
        {
            if (Input.GetKey(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0, 7), ForceMode2D.Impulse);
               //the 3 in this line of code is the player's "jumpforce," and you change that number to get different jump behaviors.  You can also create a public variable for it and then edit it in the inspector.
            }
        }
    }


    private void SetCountText() 
    {   if (scoreValue >= 4) 
        {
                levelNumber = 2;
                level.text = levelNumber.ToString();
                stage1.SetActive(false);
                stage2.SetActive(true);

                if (scoreValue >=8)
                {
                    winTextObject.SetActive(true);
                    audio.ChangeMusic("win");
                    PlayerReset();
                }
                else if (livesValue <= 0) 
        {
            loseTextObject.SetActive(true);
            audio.ChangeMusic("lose");
            PlayerReset();
        }
        }
        else if (livesValue <= 0) 
        {
            loseTextObject.SetActive(true);
            audio.ChangeMusic("lose");
            PlayerReset();
        }
    }
}