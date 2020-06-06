using UnityEngine;
using GK;
using System;

namespace GY
{

    public class CollisionDedector : MonoBehaviour
    {
        public Action OnGameOver;
        [SerializeField]
        private PlayerController _playerController;
        [SerializeField]
        private UIManager _uiManager;
        [SerializeField]
        private CameraDeathPosition _cameraDeathPos;
        [SerializeField]
        private ParticleSystem _VFXDeath = null;
        private bool isGameOver;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Gold")
            {
                PlayCollectableVFX(collision.gameObject);
                CollectGold(collision.gameObject);
                PlayerSoundManager.instance.PlayCoinCollect();
            }
            if (collision.gameObject.tag == "LaserButton")
            {
                collision.gameObject.GetComponent<LaserButton>().CloseLaser();
            }
            if (collision.gameObject.tag == "DoubleJump")
            {
                if (_playerController.HasDoubleJump)
                {
                    return;
                }
                PlayCollectableVFX(collision.gameObject);
                collision.gameObject.SetActive(false);
                _playerController.HasDoubleJump = true;
                _uiManager.ShowDoubleJump();
                PlayerSoundManager.instance.PlayPowerUpCollect();
            }
            if (collision.gameObject.tag == "Armor")
            {
                if (_playerController.HasArmor)
                {
                    return;
                }

                PlayCollectableVFX(collision.gameObject);
                collision.gameObject.SetActive(false);
                _playerController.HasArmor = true;
                _uiManager.ShowArmor();
                PlayerSoundManager.instance.PlayPowerUpCollect();
            }
            if (collision.gameObject.tag == "EndGameArea")
            {
                PlayerSoundManager.instance.PlayGameWin();
                _uiManager.OpenSuccesPanel();
                collision.enabled = false;
            }
            if (collision.gameObject.tag == "Obstacle")
            {

                Die();

            }
        }

        private void PlayCollectableVFX(GameObject targetGameObject)
        {
            ParticleSystem vfx = targetGameObject.GetComponentInChildren<ParticleSystem>();
            vfx.transform.parent = null;
            vfx.Play();
        }

        public void CollectGold(GameObject gold)
        {
            gold.SetActive(false);
            _uiManager.AddGold();
        }

        public void Die()
        {
            if (!isGameOver)
            {

                if (_playerController.HasArmor)
                {
                    _playerController.HasArmor = false;
                    _uiManager.CloseArmor();
                }
                else
                {
                    isGameOver = true;
                    Debug.Log("GameOVer");
                    _VFXDeath.transform.position = new Vector3(_playerController.transform.position.x, _playerController.transform.position.y, _VFXDeath.transform.position.z);
                    _VFXDeath.Play();
                    _cameraDeathPos.GameOver();
                    OnGameOver?.Invoke();
                    _uiManager.OpenFailPanel();
                }
            }
        }



    }
}