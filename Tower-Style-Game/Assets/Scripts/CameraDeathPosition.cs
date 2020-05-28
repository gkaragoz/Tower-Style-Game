using UnityEngine;
using GK;
namespace GY{

public class CameraDeathPosition : MonoBehaviour
 {
        [SerializeField]
        [Utils.ReadOnly]
        private bool _isGameOver;
        [SerializeField]
        private GameObject _collector = null;
        [SerializeField]
        private Transform _player=null;
        [SerializeField]
        private float _deathZoneDistance=0;
        [SerializeField]
        [Utils.ReadOnly]
        private float _playerMaxPoint=0; 
        [SerializeField]
        [Utils.ReadOnly]
        private float _lavaYPosition=0;
        [SerializeField]
        private CameraController _cameraController=null;

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
                _collector.transform.position = Vector3.up * _lavaYPosition;
            } else if(_player.position.y< _lavaYPosition&&!_isGameOver) {
             
            GameOver();
            }
        }

        public void GameOver() {
            _cameraController.target = null;
            Debug.Log("Oyun Bitti");
            _isGameOver = true;
        }
    }
}