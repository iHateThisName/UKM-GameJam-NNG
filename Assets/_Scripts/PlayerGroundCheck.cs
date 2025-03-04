using UnityEngine;

public class PlayerGroundCheck : MonoBehaviour {
    public bool IsGrounded { get; private set; } = false;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Ground")) {
            IsGrounded = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.CompareTag("Ground")) {
            IsGrounded = false;
        }
    }
}
