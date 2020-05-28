using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GK;

namespace GY{

public class LaserHolder : MonoBehaviour
 {
        [SerializeField]
        private float _laserCloseSpeed=1;
        private Coroutine _closeLaser;

        public void CloseLaser() {
            if (transform.gameObject.activeSelf) {
             _closeLaser = StartCoroutine(CloseLaserStart());

            }
        }



        IEnumerator CloseLaserStart() {
            while (true) {
                transform.localScale = new Vector3(Mathf.MoveTowards(transform.localScale.x,0,Time.deltaTime*_laserCloseSpeed),transform.localScale.y,transform.lossyScale.z);
                if (transform.localScale.x<0.1f&&transform.localScale.x>-.1f) {
                    StopCoroutine(_closeLaser);
                    transform.gameObject.SetActive(false);
                }
                yield return null;
            }
        }
 }
}