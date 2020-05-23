using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GY{

public class CollisionDedector : MonoBehaviour
 {
        //Just for today
        int _goldCount=0;
        [SerializeField]
        private SceneBasicUIManager sceneUIManager;

        private void OnTriggerEnter2D(Collider2D collision) {
            if (collision.gameObject.tag == "Gold") {
                CollectGold(collision.gameObject);
            }
        }

        public void CollectGold(GameObject gold) {
            gold.SetActive(false);
            _goldCount += 1;
            sceneUIManager.UpdateGold(_goldCount);
        }

    }
}