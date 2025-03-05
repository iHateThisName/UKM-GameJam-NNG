using System;
using System.Collections;
using UnityEngine;

public class PlayerMovment : MonoBehaviour {
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float moveSpeed = 2.5f;
    [SerializeField] private float jumpPower = 16f;
    [SerializeField] private float gravity = 9.81f;
    [SerializeField] private PlayerGroundCheck groundCheck;

    [SerializeField] private Rigidbody2D rb2D;
    [SerializeField] private PlayerAnimator playerAnimator;

    public bool IsDreaming = false;

    private Vector2 moveInput;
    private bool isSpriteRight = true;

    private void FixedUpdate() {
        if (this.playerAnimator.IsAnimatingCooldown) return; // Do not allow movement when animating
        if (IsDreaming) {
            rb2D.linearVelocity = new Vector2(moveInput.x * moveSpeed, moveInput.y * moveSpeed);
        } else {
            rb2D.linearVelocity = new Vector2(moveInput.x * moveSpeed, rb2D.linearVelocity.y);
        }
    }


    // Update is called once per frame
    void Update() {
        this.moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        if (this.playerAnimator.IsAnimatingCooldown) return; // Do not allow movement when animating

        FlipSprite();
        Dreaming();

        if (IsDreaming) return; // Do not allow jump when dreaming

        // Jump
        if (Input.GetButtonDown("Jump") && this.groundCheck.IsGrounded) {
            this.rb2D.linearVelocity = new Vector2(rb2D.linearVelocity.x, this.jumpPower);
        }

        // Short Jump
        if (Input.GetButtonUp("Jump") && this.rb2D.linearVelocityY > 0f) {
            rb2D.linearVelocity = new Vector2(rb2D.linearVelocity.x, rb2D.linearVelocity.y * 0.5f);
        }
    }

    private void Dreaming() {
        if (Input.GetKeyDown(KeyCode.LeftShift)) {
            this.IsDreaming = !this.IsDreaming;

            if (this.IsDreaming) {
                if (this.playerAnimator != null) this.playerAnimator.currentPlayerState = PlayerAnimator.PlayerState.StartDreaming;
                this.rb2D.gravityScale = 0;
                this.rb2D.linearVelocity = Vector2.zero;
            } else {
                this.rb2D.gravityScale = 4;
                this.rb2D.linearVelocity = Vector2.zero;
                if (this.playerAnimator != null) this.playerAnimator.currentPlayerState = PlayerAnimator.PlayerState.EndDreaming;
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

    // make a public function called MovePlayer that takes in a Vector2 called direction and float called time and moves the player in that direction for that amount of time
    public void MovePlayer(Vector2 direction, float time) {
        StartCoroutine(MovePlayerCoroutine(direction, time));
    }

    private IEnumerator MovePlayerCoroutine(Vector2 direction, float time) {
        float elapsedTime = 0f;
        Vector2 initialPosition = rb2D.position;
        Vector2 targetPosition = initialPosition + direction;

        while (elapsedTime < time) {
            rb2D.position = Vector2.Lerp(initialPosition, targetPosition, elapsedTime / time);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        rb2D.position = targetPosition;
    }
}
