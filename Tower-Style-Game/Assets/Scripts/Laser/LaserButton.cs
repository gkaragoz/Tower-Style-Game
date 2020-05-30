using UnityEngine;
using GK;

namespace GY {

    public class LaserButton : MonoBehaviour {

        [SerializeField]
        private LaserHolder _laserHolder = null;
        [SerializeField]
        private LaserInteractTween _laserInteractTween = null;

        [Utils.ReadOnly]
        [SerializeField]
        private bool _hasInteracted = false;

        public void CloseLaser() {
            if (_hasInteracted) {
                return;
            }

            _hasInteracted = true;
            _laserInteractTween.Interact();
            _laserHolder.CloseLaser();
        }
    }
}