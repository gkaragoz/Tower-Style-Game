using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GY{

public class PlatformCollector : MonoBehaviour
 {
        private void OnTriggerEnter2D(Collider2D collision) {
            if (collision.tag=="Platform") {
                collision.gameObject.SetActive(false);
            }
        }
    }
}