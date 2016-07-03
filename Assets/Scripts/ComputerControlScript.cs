using UnityEngine;
using System.Collections;

/// <summary>
/// Responsible for moving the computer instance, storing info, and handling being hacked
/// </summary>
public class ComputerControlScript : MonoBehaviour {
        // Configurable properties
        private float MaxSpeed = 2f;
        private float JumpForce = 200f;
        [Range(0, 1)]
        [SerializeField]
        private float AirControlModifier = 0.5f;

        [SerializeField]
        public GameObject AxeReference;

        // Attributes
        private GroundDetectionScript GroundCheck;
        private Rigidbody2D Rigidbody2D;
        private Animator anim;
        // Constants
        private const float GroundCheckRadius = .05f;
        private const float CeilingRadius = .01f;
        private const float AXE_SPEED = 10.0f;
        private const float ROTATE_SPEED = 360f;
        private const float MAX_SPEED = 5f;
        private const float MIN_SPEED = 1f;

        private float DAMAGE_MULTIPLIER = 2.0f;

        private bool immune = false;
        private float health = 10f;

        // Use this for initialization
        void Start() {
            this.anim = this.GetComponent<Animator>();
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


            // If the player should jump...
            if (this.GroundCheck.Grounded && jump) {
                // Add a vertical force to the player.
                this.Rigidbody2D.AddForce(new Vector2(0f, JumpForce));
            }
        }

        public void ShootLightning(Vector2 angle) {
            // TODO shoot lightning
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
