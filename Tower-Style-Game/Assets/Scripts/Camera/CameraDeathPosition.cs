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
        private float _deathZoneDistance=0;
        [SerializeField]
        [Utils.ReadOnly]
        private float _playerMaxPoint=0; 
        [SerializeField]
        [Utils.ReadOnly]
        private float _lavaYPosition=0;
        [SerializeField]
        private CameraController _cameraController=null;
        [SerializeField]
        private UIManager _uIManager;
        [SerializeField]
        [Utils.ReadOnly]
        private float _endGameHeight;
        [SerializeField]
        [Utils.ReadOnly]
        private Transform _player=null;
        private void Start() {
            _player = GameObject.FindGameObjectWithTag("Player").transform;
            _lavaYPosition = -10f;
            _playerMaxPoint = 0;
            _endGameHeight = GameObject.FindGameObjectWithTag("EndGameArea").transform.position.y;
        }

        public float LavaYPosition() {
            return _lavaYPosition;
        }

        private void Update() {
            _uIManager.UpdateIndicator(_playerMaxPoint/_endGameHeight);

            if (_player.position.y>_playerMaxPoint) {
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
            _uIManager.OpenFailPanel();
            _isGameOver = true;
        }
    }
}