
using UnityEngine;
public class PlayerController2D : MonoBehaviour
{

    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public float crouchHeight = 0.5f;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private BoxCollider2D collider;
    private Animator animator;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
    }
    //Crouches
    bool waterCrouch = true;//Shrinks user
    bool firedash = true;//Shrinks user, burns ground breakable walls 
//Double Jumps
    bool airJump = true;  //Unlimited (by mana) jumps
    bool fireJump = false; //Burns roof breakable walls
    bool waterJump = false; //single extra jump

//Climb/glide
    bool earthwallclimb = false; //Climb certain walls
    bool airGlide = false; // Long range glide.

//Platforms
    bool earthplatform = true; //Extend a platform below you.
    bool airplatform = false; //Create a platform in the air, temporarily.

//Wall breakers
    bool waterblast = false; //Breaks water breakable walls
    bool fireblast = false; //breaks fire breakable walls.

    bool cancast = false;
    void FixedUpdate()
    {
        animator.SetBool("Is_Jumping", false);
        // Handle horizontal movement
        // Check if the character can jump
        isGrounded = Physics2D.OverlapArea(new Vector2(collider.bounds.min.x, collider.bounds.min.y), new Vector2(collider.bounds.max.x, collider.bounds.min.y - 0.1f), groundLayer);
        if (isGrounded)
        {
            Debug.Log("Grounded");

            float move = Input.GetAxis("Horizontal");
            rb.velocity = new Vector2(move * moveSpeed, rb.velocity.y);
            animator.SetBool("Is_moving", move != 0);
            bool x = move != 0;
            if (x)
            {
                gameObject.GetComponent<SpriteRenderer>().flipX = move < 0;
                cancast = firedash;
            }
        }
        else
        {
            Debug.Log("Not Grounded");

            float move = Input.GetAxis("Horizontal");
            rb.velocity = new Vector2((move * moveSpeed) / 2, rb.velocity.y);
            bool x = move != 0;
            if (x)
            {
                gameObject.GetComponent<SpriteRenderer>().flipX = move < 0;
            }

            Collider2D[] collidersWall = Physics2D.OverlapAreaAll(new Vector2(collider.bounds.min.x, collider.bounds.min.y), new Vector2(collider.bounds.max.x, collider.bounds.max.y), groundLayer);
            for (int i = 0; i < collidersWall.Length; i++)
            {
                if (collidersWall[i].gameObject != null)
                {
                    rb.velocity = new Vector2(0, rb.velocity.y);
                }
            }

        }
        if (Input.GetAxis("Vertical") > 0 && isGrounded)
        {
            animator.SetBool("Is_Jumping", true);
            Jump();
        }
        else
        {
            if (Input.GetAxis("Vertical") > 0 && airJump && Input.GetKey(KeyCode.Z))
            {
                animator.SetBool("Is_Jumping", true);
                Jump();
            }
        }


            if (Input.GetAxis("Vertical") < 0) // Crouch key
            {
                animator.SetBool("Is_Crouched", true);
                if (waterCrouch)
                {
                    cancast = true;
                }
            }
            else
            {
                Debug.Log("Uncrouched");
                if (CanUncrouch())
                {
                    animator.SetBool("Is_Crouched", false);

                }
            }
            cancast = earthplatform;

            if (Input.GetKey(KeyCode.Z) && cancast)
            {
                Debug.Log("Casting");
                animator.SetBool("IsCasting", true);
            }
            else
            {
                animator.SetBool("IsCasting", false);

            }
        }

        void Jump()
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }


        bool CanUncrouch()
        {
            // Check if there's space above the character to uncrouch
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.up, collider.size.y + crouchHeight, groundLayer);
            return hit.collider == null;
        }
    }
