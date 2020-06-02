using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GY{

public class CameraParentFollower : MonoBehaviour
 {
        public Transform player;
        Vector3 _myPos;
        private void Update() {
            _myPos = transform.position;
            _myPos.y = player.transform.position.y;
            transform.position=_myPos;
        }
    }
}