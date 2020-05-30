using UnityEngine;
using GK;
using System;

namespace GY{

public class CollisionDedector : MonoBehaviour
 {
        public Action OnGameOver;
        [SerializeField]
        private PlayerController _playerController;
        [SerializeField]
        private UIManager _uiManager;

        private void OnTriggerEnter2D(Collider2D collision) {
            if (collision.gameObject.tag == "Gold") {
                CollectGold(collision.gameObject);
            }
            if (collision.gameObject.tag == "LaserButton") {
                collision.gameObject.GetComponent<LaserButton>().CloseLaser();
            }
            if (collision.gameObject.tag=="DoubleJump") {
                collision.gameObject.SetActive(false);
                _playerController.HasDoubleJump = true;
                _uiManager.ShowDoubleJump();
            }
            if (collision.gameObject.tag == "Armor") {
                collision.gameObject.SetActive(false);
                _playerController.HasArmor = true;
                _uiManager.ShowArmor();
            }
            if (collision.gameObject.tag == "EndGameArea") {
                collision.gameObject.SetActive(false);
                _playerController.HasArmor = true;
                _uiManager.OpenSuccesPanel();
            }
            if (collision.gameObject.tag == "Obstacle") {
                if (_playerController.HasArmor) {
                    _playerController.HasArmor = false;
                    _uiManager.CloseArmor();
                } else {
                    Debug.Log("GameOVer");
                    Camera.main.GetComponent<CameraDeathPosition>().GameOver();
                    OnGameOver?.Invoke();
                    _uiManager.OpenFailPanel();
                }
                
            }
        }

        public void CollectGold(GameObject gold) {
            gold.SetActive(false);
            _uiManager.AddGold();            
        }

    }
}