using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GK;
namespace GY{

public class CameraDeathPosition : MonoBehaviour
 {
        [SerializeField]
        private Transform _player;
        [SerializeField]
        private float _deathZoneDistance;
        [SerializeField]
        [Utils.ReadOnly]
        private float _playerMaxPoint; 
        [SerializeField]
        [Utils.ReadOnly]
        private float _lavaYPosition;
        [SerializeField]
        private CameraController _cameraController;

        private void Start() {
            _lavaYPosition = -10f;
        }

        public float LavaYPosition() {
            return _lavaYPosition;
        }

        private void Update() {
            if(_player.position.y>_playerMaxPoint) {
                _playerMaxPoint = _player.position.y;
               _lavaYPosition= _playerMaxPoint - _deathZoneDistance;
            } else if(_player.position.y< _lavaYPosition) {
                _cameraController.target = null;
            }
            
        }
    }
}