using UnityEngine;
using System.Collections;

/// <summary>
/// Responsible for listening for all player input and acting accordingly
/// </summary>
[RequireComponent(typeof(GroundDetectionScript))]
public class PlayerControlAbility : MonoBehaviour {
    // Configurable properties
    private float MaxSpeed = 2f;
    private float JumpForce = 250f;
    [Range(0, 1)]
    [SerializeField]
    private float AirControlModifier = 0.5f;

    [SerializeField]
    public GameObject AxeReference;

    // Attributes
    private GroundDetectionScript GroundCheck;
    private Rigidbody2D Rigidbody2D;
    private bool facingLeft = false;
    // TODO private Animator anim;
    // Constants
    private const float GroundCheckRadius = .05f;
    private const float CeilingRadius = .01f;
    private const float AXE_SPEED = 10.0f;
    private const float ROTATE_SPEED = 360f;
    private const float MAX_SPEED = 5f;
    private const float MIN_SPEED = 1f;

    private float DAMAGE_MULTIPLIER = 5.0f;

    // Use this for initialization
    void Start() {
        // TODO this.anim = this.GetComponent<Animator>();
    }

    void FixedUpdate() {
        // Update animation properties
        //Vector2 velocity = Rigidbody2D.velocity;
        // TODO this.anim.SetBool("HorizontalMovement", velocity.x != 0);
        // TODO this.anim.SetFloat("HorizontalMovementValue", velocity.x);
        // TODO this.anim.SetBool("VerticalMovement", velocity.y != 0);
        // TODO this.anim.SetFloat("VerticalMovementValue", velocity.y);
    }

    private void Awake() {
        // Setting up references.
        this.GroundCheck = this.GetComponentInChildren<GroundDetectionScript>();
        this.Rigidbody2D = this.GetComponent<Rigidbody2D>();
    }

    internal void NormalMove(float horizontal, bool jump) {
        // Move the character
        // X
        float moveX = (!this.GroundCheck.Grounded ? this.AirControlModifier : 1) * horizontal * MaxSpeed;
        if (this.GroundCheck.Grounded) {
            moveX = horizontal * MaxSpeed;
        } else {
            moveX = this.AirControlModifier * horizontal * MaxSpeed;
            float currentDir = this.Rigidbody2D.velocity.x > 0 ? 1 : -1;
            float newDir = horizontal > 0 ? 1 : -1;
            if (newDir == currentDir) {
                moveX = newDir * Mathf.Max(Mathf.Abs(this.Rigidbody2D.velocity.x), Mathf.Abs(moveX));
            }

        }
        this.Rigidbody2D.velocity = new Vector2(
            moveX,
            this.Rigidbody2D.velocity.y
        );

        // If the input is moving the player right and the player is facing left...
        if (horizontal > 0 && this.facingLeft) {
            // ... flip the player.
            Flip();
        }
        // Otherwise if the input is moving the player left and the player is facing right...
        else if (horizontal < 0 && !this.facingLeft) {
            // ... flip the player.
            Flip();
        }

        // If the player should jump...
        if (this.GroundCheck.Grounded && jump) {
            // Add a vertical force to the player.
            this.Rigidbody2D.AddForce(new Vector2(0f, JumpForce));
        }
    }

    private void Flip() {
        // Switch the way the player is labelled as facing.
        facingLeft = !facingLeft;

        // Multiply the player's x local scale by -1.
        Vector2 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }


    public void ThrowAxe(Vector2 position, Vector2 reticule) {
        reticule = ViewportToDiddieView(reticule);
        GameObject axe = Instantiate(AxeReference);
        Vector2 velocity = new Vector2(reticule.x,
            reticule.y) * AXE_SPEED;
        Rigidbody2D axeBody = axe.GetComponent<Rigidbody2D>();
        Stats axeStats = axe.GetComponent<Stats>();

        // movement
        axeBody.position = transform.position;
        axeBody.velocity = velocity;

        // rotation
        float rotation = ROTATE_SPEED * (velocity.x < 0 ? 1f : -1f);
        axeBody.AddTorque(rotation, ForceMode2D.Force);
        axeStats.Damage *= DAMAGE_MULTIPLIER;
    }

    Vector2 ViewportToDiddieView(Vector2 viewport) {
        return new Vector2(viewport.x - 0.5f, viewport.y - 0.5f);
    }

    Vector2 BoundVector2(Vector2 vector) {
        return new Vector2(BoundValue(vector.x), BoundValue(vector.y));
    }

    float BoundValue(float value) {
        if (value < 0) {
            return Mathf.Max(Mathf.Min(value, -MAX_SPEED), -MIN_SPEED);
        } else {
            return Mathf.Max(Mathf.Min(value, MIN_SPEED), MAX_SPEED);
        }
    }
}
