using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GY{

public class CameraParentFollower : MonoBehaviour
 {
        public Transform player;
        Vector3 _myPos;
        private CameraDeathPosition _cameraDeathPosition;

        private void Start() {
            _cameraDeathPosition = GetComponentInChildren<CameraDeathPosition>();
        }
        private void Update() {
            if (player.position.y>_cameraDeathPosition.LavaYPosition()) {
                _myPos = transform.position;
                _myPos.y = player.transform.position.y;
                transform.position = _myPos;
            }
            
        }
    }
}