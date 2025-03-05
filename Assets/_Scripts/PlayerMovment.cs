using UnityEngine;

public class PlayerMovment : MonoBehaviour {
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float moveSpeed = 2.5f;
    [SerializeField] private float jumpHeight = 1.5f;
    [SerializeField] private float gravity = 9.81f;
    [SerializeField] private PlayerGroundCheck groundCheck;

    [SerializeField] private Rigidbody2D rb2D;

    public bool IsDreaming = false;

    private Vector2 moveInput;

    //sprite
    private bool isSpriteRight = true;

    private void FixedUpdate() {
        if (IsDreaming) {
            rb2D.linearVelocity = new Vector2(moveInput.x * moveSpeed, moveInput.y * moveSpeed);
        } else {
            rb2D.linearVelocity = new Vector2(moveInput.x * moveSpeed, rb2D.linearVelocity.y);
        }
    }


    // Update is called once per frame
    void Update() {
        this.moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        FlipSprite();
        Dreaming();

        if (IsDreaming) return;

        // Jump
        if (Input.GetButtonDown("Jump") && this.groundCheck.IsGrounded) {
            float UpForce = Mathf.Sqrt(this.jumpHeight * 5.0f * this.gravity);
            rb2D.linearVelocity = new Vector2(rb2D.linearVelocity.x, UpForce);
        }

        // Short Jump
        if (Input.GetButtonUp("Jump") && this.rb2D.linearVelocityY > 0f) {
            rb2D.linearVelocity = new Vector2(rb2D.linearVelocity.x, rb2D.linearVelocity.y * 0.5f);
        }
    }

    private void Dreaming() {
        // Dreaming

        if (Input.GetKeyDown(KeyCode.LeftShift)) {
            this.IsDreaming = !this.IsDreaming;

            if (this.IsDreaming) {
                this.rb2D.gravityScale = 0;
                this.rb2D.linearVelocity = Vector2.zero;
            } else {
                this.rb2D.gravityScale = 1;
            }
        }
    }

    private void FlipSprite() {
        //// flip the sprite when chaning direction
        if (this.isSpriteRight && this.moveInput.x < 0) {
            this.isSpriteRight = false;
            this.transform.localScale = new Vector3(-this.transform.localScale.x, this.transform.localScale.y, this.transform.localScale.z);
        } else if (!this.isSpriteRight && this.moveInput.x > 0) {
            this.isSpriteRight = true;
            this.transform.localScale = new Vector3(-this.transform.localScale.x, this.transform.localScale.y, this.transform.localScale.z);
        }
    }
}
