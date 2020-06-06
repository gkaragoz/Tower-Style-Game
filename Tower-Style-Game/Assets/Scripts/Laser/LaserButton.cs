using UnityEngine;
using GK;

namespace GY {

    public class LaserButton : MonoBehaviour {

        [SerializeField]
        private LaserHolder _laserHolder = null;
        [SerializeField]
        private LaserInteractTween _laserInteractTween = null;
        [SerializeField]
        private ParticleSystem _VFX = null;

        [Utils.ReadOnly]
        [SerializeField]
        private bool _hasInteracted = false;

        public void CloseLaser() {
            if (_hasInteracted) {
                return;
            }
            PlayerSoundManager.instance.CloseLaser();

            _hasInteracted = true;
            _VFX.Play();
            _laserInteractTween.Interact();
            _laserHolder.CloseLaser();
        }
    }
}