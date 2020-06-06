using UnityEngine;

public class MusicManager : MonoBehaviour
{
    #region Singleton

    public static MusicManager instance;
    private void Awake() {
        if (instance == null) { 
            instance = this;
            DontDestroyOnLoad(this.gameObject);

        } else if (instance != this) {
            Destroy(gameObject); 
        }


    }

    #endregion

    private AudioSource musicSource;
    private bool _isMusicOff=false;


    private void Start() {
        musicSource = transform.GetComponent<AudioSource>();
    }

    public bool IsMute {
        get { return _isMusicOff; }
        set { _isMusicOff = value; }
    }

    public void MuteUnMuteMusic() {
        IsMute = !IsMute;
        musicSource.mute = IsMute;
    }



}
