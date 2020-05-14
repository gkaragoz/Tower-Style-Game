using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GK{

public class ProjectileModifier : MonoBehaviour
 {
        [SerializeField]
        Transform playerPos;
        [SerializeField]
        private GameObject _singleBall=null;
        [SerializeField]
        private int _projectileCount;
        [SerializeField]
        private float _localScaleDivider;
        [SerializeField]
        private float projectileLenghtMultiplier;

        private Vector3 _startPos;
        private Vector3 _baseLocalScale;
        private GameObject[] _balls;


        private void Start() {
            InputManager.instance.OnInputBegin += OnInputBegin;
            InputManager.instance.OnInputDragging += OnInputDragging;
            InputManager.instance.OnInputEnd += OnInputEnd;

            _baseLocalScale = _singleBall.transform.localScale;
            _balls = new GameObject[_projectileCount];

            for (int i = 0; i < _projectileCount; i++) {
                _balls[i] = Instantiate(_singleBall);
                _balls[i].SetActive(false);
            }

        }

        private void OnInputBegin(Vector2 startPos) {

            _startPos = startPos;
        }

        private void OnInputDragging(Vector2 currentPos, Vector2 dragPos) {
            SetPositionProjectiles(currentPos,dragPos);
        }

        private void OnInputEnd(Vector2 startPos, Vector2 endPos, float arg3) {
            for (int i = 0; i < _balls.Length; i++) {
                _balls[i].SetActive(false);
                _balls[i].transform.localScale = _baseLocalScale;
            }
        }


        private void SetPositionProjectiles(Vector3 currentPos,Vector3 dragPos) {
            /* Vector3 direction = currentPos - _startPos;
             Vector3 distanceProjectile = direction / _balls.Length;*/ // for Infinity Projectiles

            Vector3 distanceProjectile = dragPos / _balls.Length;


            for (int i = 0; i < _balls.Length; i++) {
                _balls[i].SetActive(true);
                _balls[i].transform.position = playerPos.position +  distanceProjectile * i*projectileLenghtMultiplier;
                _balls[i].transform.localScale = _baseLocalScale/(_localScaleDivider*i+1); 

            }
        }
    }
}