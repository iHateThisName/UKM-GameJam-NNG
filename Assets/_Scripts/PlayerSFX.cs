using UnityEngine;

public class PlayerSFX : MonoBehaviour {
    [Header("World Audio")]
    [SerializeField] private AudioSource worldAudioSource;
    [SerializeField] private AudioClip worldDream;
    [SerializeField] private AudioClip worldReal;

    [Header("Dream Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip jumpSFX;

    private bool isPlayingDreamWorldAudio = false;

    public void PlayJumpSFX() {
        if (this.audioSource == null || this.jumpSFX == null) {
            Debug.LogWarning("Audio Source or Jump SFX Audio Clip is not set in the PlayerSFX component.");
            return;
        }
        audioSource.PlayOneShot(jumpSFX);
    }

    public void PlayDream() {
        if (this.worldAudioSource == null || this.worldDream == null) {
            Debug.LogWarning("World Audio Source or World Dream Audio Clip is not set in the PlayerSFX component.");
            return;
        }

        if (this.isPlayingDreamWorldAudio && this.worldAudioSource.clip == this.worldDream) return;
        this.worldAudioSource.clip = this.worldDream;
        this.worldAudioSource.Play();
        this.isPlayingDreamWorldAudio = true;
    }

    public void PlayReal() {
        if (this.worldAudioSource == null || this.worldReal == null) {
            Debug.LogWarning("World Audio Source or World Real Audio Clip is not set in the PlayerSFX component.");
            return;
        }

        if (!this.isPlayingDreamWorldAudio && this.worldAudioSource.clip == this.worldReal) return;
        this.worldAudioSource.clip = this.worldReal;
        this.worldAudioSource.Play();
        this.isPlayingDreamWorldAudio = false;
    }


}
