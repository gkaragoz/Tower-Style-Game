using UnityEngine;
using GK;
using System;

namespace GY{

public class CollisionDedector : MonoBehaviour
 {
        public Action OnGameOver;


        //Just for today
        int _goldCount =0;
        [SerializeField]
        private SceneBasicUIManager sceneUIManager=null;

        private void OnTriggerEnter2D(Collider2D collision) {
            if (collision.gameObject.tag == "Gold") {
                CollectGold(collision.gameObject);
            }

            if (collision.gameObject.tag == "LaserButton") {
                collision.gameObject.GetComponent<LaserButton>().CloseLaser();
            }

            if (collision.gameObject.tag=="DoubleJump") {
                collision.gameObject.SetActive(false);
                InputManager.instance.JumpTimes =1;
                sceneUIManager.UpdateJump(true);
            }
            if (collision.gameObject.tag == "Armor") {
                collision.gameObject.SetActive(false);
                InputManager.instance.HasArmor = true;
                sceneUIManager.UpdateArmor();

            }

            if (collision.gameObject.tag == "Obstacle") {
                if (InputManager.instance.HasArmor) {
                    InputManager.instance.HasArmor = false;
                    sceneUIManager.UpdateArmor();
                } else {
                    Debug.Log("GameOVer");
                    Camera.main.GetComponent<CameraDeathPosition>().GameOver();
                    OnGameOver?.Invoke();
                }
                
            }
        }

        public void CollectGold(GameObject gold) {
            gold.SetActive(false);
            _goldCount += 1;
            sceneUIManager.UpdateGold(_goldCount);
        }

    }
}