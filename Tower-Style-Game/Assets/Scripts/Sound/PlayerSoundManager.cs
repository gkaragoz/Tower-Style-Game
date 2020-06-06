using System;
using UnityEngine;

public class PlayerSoundManager : MonoBehaviour {

    private bool isSoundOff;
    public Action SoundSettingsChanged;

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
    [SerializeField]
    private AudioClip _closeLaser;

 
    #region Singleton

    public static PlayerSoundManager instance;
    private void Awake() {
        if (instance == null) { 
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        } else if (instance != this) { Destroy(gameObject); }
          
    }

    #endregion


    public bool IsMute {
        get { return isSoundOff; }
    }



    private void Start() {
        if (true) {

        }
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

    public void CloseLaser() {
        audioSource.PlayOneShot(_closeLaser);

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


    public void MuteUnmuteSound() {
        isSoundOff = !isSoundOff;
        audioSource.mute=isSoundOff;
        SoundSettingsChanged?.Invoke();
    }

}
