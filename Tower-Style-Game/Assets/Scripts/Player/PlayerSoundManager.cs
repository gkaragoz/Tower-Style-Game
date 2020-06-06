using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundManager : MonoBehaviour {
    AudioSource audioSource;
    [SerializeField]
    private AudioClip _jump;
    [SerializeField]
    private AudioClip _doubleJumpUse;
    [SerializeField]
    private AudioClip _wallHit;
    [SerializeField]
    private AudioClip _coinCollect;
    [SerializeField]
    private AudioClip _powerUpCollect;
    [SerializeField]
    private AudioClip _armorUse;
    [SerializeField]
    private AudioClip _fallGameOver;
    [SerializeField]
    private AudioClip _obctacleGameOver;
    [SerializeField]
    private AudioClip _onGrounded;
    [SerializeField]
    private AudioClip _onGameWin;


    #region Singleton

    public static PlayerSoundManager instance;
    private void Awake() {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    #endregion

    private void Start() {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySingleJump() {
        audioSource.PlayOneShot(_jump);
    }

    public void PlayWallHit() {
        audioSource.PlayOneShot(_wallHit);
    }

    public void PlayCoinCollect() {
        audioSource.PlayOneShot(_coinCollect);
    }

    public void PlayArmorUse() {
        audioSource.PlayOneShot(_armorUse);
    }

    public void PlayDoubleJumpUse() {
        audioSource.PlayOneShot(_doubleJumpUse);
    }

    public void PlayPowerUpCollect() {
        audioSource.PlayOneShot(_powerUpCollect);
    }

    public void PlayFallGameOver() {
        audioSource.PlayOneShot(_fallGameOver);
    }

    // Değiştir
    public void PlayOnGrounded() {
        audioSource.PlayOneShot(_onGrounded);
    }

    public void PlayGameWin() {
        audioSource.PlayOneShot(_onGameWin);
    }

    // Havai Fişek Bul 2x
    public void PlayFireworks() {

    }

    // Bul
    public void PlayUIClick() {

    }

    // Bul
    public void PlayUIOpen() {

    }

    // Bul
    public void PlayUIClose() {

    }

}
