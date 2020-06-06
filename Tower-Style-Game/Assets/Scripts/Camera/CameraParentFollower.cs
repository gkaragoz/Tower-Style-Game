using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GY {

    public class CameraParentFollower : MonoBehaviour {
        [SerializeField]
        private Vector3 _offset = Vector3.zero;

        public Transform player;
        Vector3 _targetPos;

        private CameraDeathPosition _cameraDeathPosition;
        
        private void Start() {
            _cameraDeathPosition = GetComponentInChildren<CameraDeathPosition>();
        }

        private void LateUpdate() {
            if (player.position.y > _cameraDeathPosition.LavaYPosition()) {
                _targetPos.y = player.transform.position.y;
                transform.position = _targetPos + _offset;
            }
        }
    }
}