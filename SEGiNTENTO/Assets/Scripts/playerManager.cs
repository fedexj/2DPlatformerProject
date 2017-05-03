using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerManager : MonoBehaviour
{
    Animator penguinAnim;
    //Transform penguinTrans;
    Rigidbody2D penguinRigidBody;
    SpriteRenderer penguinSpriteRender;

    //audio variables
    public AudioClip walkFX;
    public AudioClip jumpFX;
    public AudioClip teleportFX;

    public float teleportDistance = 100f;

    private float cont = 0.22f;
    
    bool grounded = false;
    public Transform groundcheck;
    float groundRadious;
    public LayerMask whatIsGround;
    public float jumpForce=700f;

    //player speed
    public float topSpeed = 10f;
   
	// Use this for initialization
	void Start ()
    {
        penguinAnim = GetComponent<Animator>();
        //penguinTrans = GetComponent<Transform>();
        penguinRigidBody = GetComponent<Rigidbody2D>();
        penguinSpriteRender = GetComponent<SpriteRenderer>();
    }
	
    void FixedUpdate()
    {
        MovePlayer();
        Animation();
        //flip
        Flip();
        
    }

    void Update()
    {       
        jump();
        teleport();     
    }

    void MovePlayer()
    {
        grounded = Physics2D.OverlapCircle(groundcheck.position, groundRadious, whatIsGround);
        penguinAnim.SetBool("Ground", grounded);
        //penguinAnim.SetFloat("verticalSpeed", penguinRigidBody.velocity.y);

        //get direction
        float move = Input.GetAxis("Horizontal");
        //float jump = Input.GetAxis("Vertical");
        //add velocity to the rigidBody in the moveDirection*our speed
        penguinRigidBody.velocity = new Vector2(move * topSpeed, penguinRigidBody.velocity.y);
        //PlayWalk();
      
    }

    void PlayWalk()
    {
        if (penguinRigidBody.velocity.x != 0  && grounded)
        {
            cont = cont - Time.deltaTime;
            if (cont <= 0f)
            {
                SoundManager.instance.playWalk(walkFX);
                cont = 0.22f;
            }
        }
    }

    public void Animation()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            penguinAnim.SetInteger("state", 3); 
        }
              

        else if (Input.GetAxis("Horizontal") > 0)
        {            
            penguinAnim.SetInteger("state", 1);
            PlayWalk();
                       
        }

        else if (Input.GetAxis("Horizontal") < 0)
        {
            penguinAnim.SetInteger("state", 1);
            PlayWalk();

        }

        else
        {
            penguinAnim.SetInteger("state", 0);            
        }
        
    }

    void Flip()
    {
        if (penguinRigidBody.velocity.x > 0 )
        {
            penguinSpriteRender.flipX=false;
        }
        if (penguinRigidBody.velocity.x < 0 )
        {
            penguinSpriteRender.flipX=true;
        }
    }

    void jump()
    {
        if (grounded && Input.GetKeyDown(KeyCode.Space))
        {
            penguinAnim.SetBool("Ground", false);
            penguinRigidBody.AddForce(new Vector2(0, jumpForce));
            SoundManager.instance.playSingle(jumpFX);
        }
    }

    void teleport()
    {
        if(Input.GetKeyDown(KeyCode.LeftShift ) && penguinRigidBody.velocity.x > 0 )
        {
            penguinRigidBody.position = new Vector2(penguinRigidBody.position.x + teleportDistance * Time.deltaTime, penguinRigidBody.position.y);
            penguinAnim.SetInteger("state", 3);
            SoundManager.instance.playSingle(teleportFX);
            

        }
        if (Input.GetKeyDown(KeyCode.LeftShift) && penguinRigidBody.velocity.x < 0)
        {
            penguinRigidBody.position = new Vector2(penguinRigidBody.position.x - teleportDistance * Time.deltaTime, penguinRigidBody.position.y);
            penguinAnim.SetInteger("state", 3);
            SoundManager.instance.playSingle(teleportFX);            
        }        
    }   
}
