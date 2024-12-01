
using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
public class PlayerController2D : MonoBehaviour
{

    public float moveSpeed = Player.movespeed;
    public float jumpForce = 10f;
    public float crouchHeight = 0.5f;
    public LayerMask groundLayer;
    public LayerMask deathLayer;

    private Rigidbody2D rb;
    private BoxCollider2D collider;
    public BoxCollider2D collider2;
    private Animator animator;
    private bool isGrounded;

    void Awake()
    {
        Player.gameObject  = gameObject;
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
    }
    //Crouches
    bool waterCrouch = Player.player.spellbook[type.water] == spell.dash;//Shrinks user
    bool firedash = Player.player.spellbook[type.fire] == spell.dash;//Shrinks user, burns ground breakable walls 
//Double Jumps
    bool airJump = Player.player.spellbook[type.air] == spell.jump;  //Unlimited (by mana) jumps
    bool fireJump = Player.player.spellbook[type.fire] == spell.jump; //Burns roof breakable walls

    bool waterJump = Player.player.spellbook[type.water] == spell.jump; //single extra jump
//Climb/glide
    bool earthwallclimb = Player.player.spellbook[type.earth] == spell.climb; //Climb certain walls
    bool airGlide = Player.player.spellbook[type.air] == spell.glide; // Long range glide.

//Platforms
    bool earthplatform = Player.player.spellbook[type.earth] == spell.platform; //Extend a platform below you.
    bool airplatform = Player.player.spellbook[type.air] == spell.platform; //Create a platform in the air, temporarily.

//Wall breakers
    bool waterblast = Player.player.spellbook[type.water] == spell.blast; //Breaks water breakable walls
    bool fireblast = Player.player.spellbook[type.fire] == spell.blast; //breaks fire breakable walls.

    bool cancast = false;
    
    public void FixedUpdate()
    {  
        animator.SetBool("Is_Jumping", false);
                    animator.SetBool("IsWalled", false);
        // Handle horizontal movement
        // Check if the character can jump
        isGrounded = Physics2D.OverlapCircle(new Vector2((collider.bounds.min.x + collider.bounds.max.x) / 2, collider.bounds.min.y),0.175f, groundLayer);
        Player.player.interact = Player.player.interact==Player.Interact.Interact?Player.Interact.Interact:isGrounded?Player.Interact.Cast:Player.Interact.CastAriel;
        
            animator.SetBool("IsGrounded", isGrounded);
        if (Player.upDown())
        {
            animator.SetBool("Is_Jumping", true);
            Jump();
        }


            if (Player.DownDown()) // Crouch key
            {
                animator.SetBool("Is_Crouched", true);
            }
            else
            {
                Debug.Log("Uncrouched");
                if (CanUncrouch())
                {
                    animator.SetBool("Is_Crouched", false);

                }
            }

        if (isGrounded)
        {
            Debug.Log("Grounded");

            float move = Player.GetHorizontal();
            rb.velocity = new Vector2(move * moveSpeed, rb.velocity.y);
            animator.SetBool("Is_moving", move != 0);
            bool x = move != 0;
            if (x)
            {
                gameObject.GetComponent<Transform>().rotation =move > 0?new Quaternion(0,0,0,0): new Quaternion(0,180,0,0);
            }
            isWalled();
        }
        else
        {
            Debug.Log("Not Grounded");

            float move = Player.GetHorizontal();
            rb.velocity = new Vector2((move * moveSpeed) / 2, rb.velocity.y);
            bool x = move != 0;
            if (x)
            {
                gameObject.GetComponent<Transform>().rotation =move > 0?
                 new Quaternion(0,0,0,0): new Quaternion(0,180,0,0);
            }

            isWalled();

        }

            if (Player.Casting()&&Player.canCast(isGrounded))
            {
                animator.SetInteger("IsCasting", Player.CastingCount());
            }
            else
            {
                animator.SetInteger("IsCasting", Player.CastingCount());

            }

        if (Physics2D.OverlapCircle(new Vector2((collider.bounds.min.x + collider.bounds.max.x) / 2, collider.bounds.min.y), 0.15f, deathLayer))
            {
                animator.SetBool("IsDying", true);
            }
            else
            {


            }
        }

    private void isWalled()
    {
                    Collider2D[] collidersWall = Physics2D.OverlapAreaAll(new Vector2(collider2.bounds.min.x, collider2.bounds.min.y), new Vector2(collider2.bounds.max.x, collider2.bounds.max.y), groundLayer);
            Debug.Log(collidersWall.Length);
            for (int i = 0; i < collidersWall.Length; i++)
            {
                if (collidersWall[i].gameObject != null)
                {
                Debug.Log(collidersWall[i].gameObject.name);

                float move = Player.GetHorizontal();
                    Player.animator.SetBool("IsWalled", true);
                    rb.velocity = move!=0?new Vector2(rb.velocity.x, Player.Verticalsmovespeed * Math.Abs(move)):new Vector2(rb.velocity.x, rb.velocity.y);
                }
            }
    }

    void Jump()
        {
           // rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }


        bool CanUncrouch()
        {
            // Check if there's space above the character to uncrouch
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.up, collider.size.y + crouchHeight, groundLayer);
            return hit.collider == null;
        }
    }
