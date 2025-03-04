using UnityEngine;

public class PlayerMovment : MonoBehaviour {
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float moveSpeed = 2.5f;
    [SerializeField] private float jumpHeight = 1.0f;
    [SerializeField] private float gravity = 9.81f;
    [SerializeField] private PlayerGroundCheck groundCheck;

    public bool IsDreaming = false;

    private Vector2 moveInput;
    private Vector2 velocity;

    //sprite
    private bool isSpriteRight = true;


    // Update is called once per frame
    void Update() {
        this.moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

        // Move horizontally
        float horizontalMovement = this.moveInput.x * this.moveSpeed * Time.deltaTime;


        // Check if player is on the ground
        bool isGrounded = this.groundCheck.IsGrounded;


        // Apply vertical movement
        if (this.IsDreaming) {
            this.playerTransform.position += new Vector3(horizontalMovement, this.moveInput.y * this.moveSpeed * Time.deltaTime, 0);

        } else {
            // Jumping
            if (isGrounded && velocity.y <= 0) {
                velocity.y = 0; // Reset velocity when touching the ground
                if (Input.GetButtonDown("Jump")) {
                    velocity.y = Mathf.Sqrt(this.jumpHeight * 2.0f * this.gravity); // Apply jump force
                }
            }

            // Apply gravity
            if (!isGrounded) velocity.y -= gravity * Time.deltaTime;
            this.playerTransform.position += new Vector3(horizontalMovement, velocity.y * Time.deltaTime, 0);
        }

        // flip the sprite when chaning direction
        if (this.isSpriteRight && horizontalMovement < 0) {
            this.isSpriteRight = false;
            this.transform.localScale = new Vector3(-this.transform.localScale.x, this.transform.localScale.y, this.transform.localScale.z);
        } else if (!this.isSpriteRight && horizontalMovement > 0) {
            this.isSpriteRight = true;
            this.transform.localScale = new Vector3(-this.transform.localScale.x, this.transform.localScale.y, this.transform.localScale.z);
        }

    }
}
