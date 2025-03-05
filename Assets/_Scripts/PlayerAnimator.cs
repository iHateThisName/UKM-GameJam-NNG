using System.Collections;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour {
    public enum PlayerState {
        Idle,
        Walk,
        Jump,
        Fall,
        StartDreaming,
        Dreaming,
        EndDreaming
    }
    public PlayerState currentPlayerState = PlayerState.Idle;
    public bool IsAnimatingCooldown => isAnimatingCooldown;
    [SerializeField] private bool isAnimatingCooldown = false;
    [SerializeField] private float animationSpeed = 0.25f;
    [SerializeField] private SpriteRenderer playerSprite;
    [SerializeField] private PlayerMovment playerMovment;
    [SerializeField] private PlayerSFX playerSFX;
    [SerializeField] private Sprite[] startDreamingSprites;
    [SerializeField] private Sprite[] DreamingSprites;
    private Sprite fallbackSprite;

    private PlayerState previousPlayerState;

    private void Start() {
        this.fallbackSprite = playerSprite.sprite;
    }
    private void FixedUpdate() {
        playerSprite.size = new Vector2(2, 2);
        if (isAnimatingCooldown || this.previousPlayerState == this.currentPlayerState) return;
        switch (currentPlayerState) {
            case PlayerState.Jump:
                this.playerSprite.sprite = this.fallbackSprite;
                playerSFX.PlayJumpSFX();
                break;
            case PlayerState.StartDreaming:
                this.isAnimatingCooldown = true;
                this.playerSFX.PlayDream();
                StartCoroutine(AnimateStartDreaming());
                break;
            case PlayerState.Dreaming:
                this.playerSprite.sprite = this.DreamingSprites[0];
                break;
            default:
                this.playerSprite.sprite = this.fallbackSprite;
                this.playerSFX.PlayReal();
                break;
        }
        this.previousPlayerState = this.currentPlayerState;
    }

    private IEnumerator AnimateStartDreaming() {
        int index = 0;

        while (index < startDreamingSprites.Length) {
            if (index == 1) {
                this.playerMovment.MovePlayer(Vector2.up, animationSpeed * 2);
            }
            playerSprite.sprite = startDreamingSprites[index];
            yield return new WaitForSeconds(animationSpeed);
            index++;
        }

        this.isAnimatingCooldown = false;
        this.currentPlayerState = PlayerState.Dreaming;
    }
}
