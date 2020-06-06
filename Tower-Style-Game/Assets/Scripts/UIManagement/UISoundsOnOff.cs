using UnityEngine;
using UnityEngine.UI;

namespace GK {

    public class UISoundsOnOff : MonoBehaviour {

        [SerializeField]
        private Button _btnMusic = null;
        [SerializeField]
        private Button _btnSound = null;

        [SerializeField]
        private Sprite _imgMusicOn = null;
        [SerializeField]
        private Sprite _imgMusicOff = null;
        [SerializeField]
        private Sprite _imgSoundOn = null;
        [SerializeField]
        private Sprite _imgSoundOff = null;

        [SerializeField]
        private float _scaleAmount = 1.1f;
        [SerializeField]
        private float _scaleSpeed = 0.1f;

        private RectTransform _musicRectTransform = null;
        private RectTransform _soundRectTransform = null;
        private Image _imgMusic = null;
        private Image _imgSound = null;

        private bool _isMusicOn = true;
        private bool _isSoundOn = true;

        private void Awake() {

            _musicRectTransform = _btnMusic.GetComponent<RectTransform>();
            _soundRectTransform = _btnSound.GetComponent<RectTransform>();

            _imgMusic = _btnMusic.GetComponent<Image>();
            _imgSound = _btnSound.GetComponent<Image>();



            
            _btnMusic.onClick.AddListener(() => {
                if (LeanTween.isTweening(_musicRectTransform)) {
                    return;
                }

                _isMusicOn = !_isMusicOn;

                if (_isMusicOn) {
                    _imgMusic.sprite = _imgMusicOn;
                } else {
                    _imgMusic.sprite = _imgMusicOff;
                }

                // Pop size of button
                LeanTween.size(_musicRectTransform, _musicRectTransform.sizeDelta * _scaleAmount, _scaleSpeed).setEaseInOutCirc().setRepeat(2).setLoopPingPong().setIgnoreTimeScale(true);
            });

            _btnSound.onClick.AddListener(() => {
                if (LeanTween.isTweening(_soundRectTransform)) {
                    return;
                }

                _isSoundOn = !_isSoundOn;

                if (_isSoundOn) {
                    _imgSound.sprite = _imgSoundOn;
                } else {
                    _imgSound.sprite = _imgSoundOff;
                }

                // Pop size of button
                LeanTween.size(_soundRectTransform, _soundRectTransform.sizeDelta * _scaleAmount, _scaleSpeed).setEaseInOutCirc().setRepeat(2).setLoopPingPong().setIgnoreTimeScale(true);
            });
        }



        /// <summary>
        /// // IS GONNA CHANGE
        /// </summary>
        private void Update() {
            CheckSoundAndMusicStatus();
        }
        void CheckSoundAndMusicStatus() {
            _isMusicOn = MusicManager.instance.IsMute;
            _isSoundOn = PlayerSoundManager.instance.IsMute;
            if (_isMusicOn) {
                _imgMusic.sprite = _imgMusicOff;
            } else {
                _imgMusic.sprite = _imgMusicOn;
            }
            if (_isSoundOn) {
                _imgSound.sprite = _imgSoundOff;
            } else {
                _imgSound.sprite = _imgSoundOn;
            }
        }

        





    }

}
