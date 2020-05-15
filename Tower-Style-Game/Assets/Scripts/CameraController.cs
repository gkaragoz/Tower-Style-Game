using UnityEngine;
using System;

namespace GK {

    [Flags]
    public enum Direction {
        None = 0,
        Horizontal = 1,
        Vertical = 2,
        Both = 3
    }

    public class CameraController : MonoBehaviour {

        public Transform target;
        public float dampTime = 0.15f;
        public Direction followType = Direction.Horizontal;

        [Range(0.0f, 1.0f)]
        public float cameraCenterX = 0.5f;
        [Range(0.0f, 1.0f)]
        public float cameraCenterY = 0.5f;

        public Direction boundType = Direction.None;
        public float leftBound = 0;
        public float rightBound = 0;
        public float upperBound = 0;
        public float lowerBound = 0;
        public Direction deadZoneType = Direction.None;
        public bool hardDeadZone = false;
        public float leftDeadBound = 0;
        public float rightDeadBound = 0;
        public float upperDeadBound = 0;
        public float lowerDeadBound = 0;

        // private
        private Camera _camera;
        private Vector3 _velocity = Vector3.zero;
        private float _vertExtent;
        private float _horzExtent;
        private Vector3 _tempVec = Vector3.one;
        private bool _isBoundHorizontal;
        private bool _isBoundVertical;
        private bool _isFollowHorizontal;
        private bool _isFollowVertical;
        private bool _isDeadZoneHorizontal;
        private bool _isDeadZoneVertical;
        private Vector3 _deltaCenterVec;

        private void Start() {
            _camera = GetComponent<Camera>();
            _vertExtent = _camera.orthographicSize;
            _horzExtent = _vertExtent * Screen.width / Screen.height;
            _deltaCenterVec = _camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0))
                - _camera.ViewportToWorldPoint(new Vector3(cameraCenterX, cameraCenterY, 0));


            _isFollowHorizontal = (followType & Direction.Horizontal) == Direction.Horizontal;
            _isFollowVertical = (followType & Direction.Vertical) == Direction.Vertical;
            _isBoundHorizontal = (boundType & Direction.Horizontal) == Direction.Horizontal;
            _isBoundVertical = (boundType & Direction.Vertical) == Direction.Vertical;

            _isDeadZoneHorizontal = ((deadZoneType & Direction.Horizontal) == Direction.Horizontal) && _isFollowHorizontal;
            _isDeadZoneVertical = ((deadZoneType & Direction.Vertical) == Direction.Vertical) && _isFollowVertical;
            _tempVec = Vector3.one;
        }

        private void LateUpdate() {
            if (target) {
                Vector3 delta = target.position - _camera.ViewportToWorldPoint(new Vector3(cameraCenterX, cameraCenterY, 0));

                if (!_isFollowHorizontal) {
                    delta.x = 0;
                }
                if (!_isFollowVertical) {
                    delta.y = 0;
                }
                Vector3 destination = transform.position + delta;

                if (!hardDeadZone) {
                    _tempVec = Vector3.SmoothDamp(transform.position, destination, ref _velocity, dampTime);
                } else {
                    _tempVec.Set(transform.position.x, transform.position.y, transform.position.z);
                }

                if (_isDeadZoneHorizontal) {
                    if (delta.x > rightDeadBound) {
                        _tempVec.x = target.position.x - rightDeadBound + _deltaCenterVec.x;
                    }
                    if (delta.x < -leftDeadBound) {
                        _tempVec.x = target.position.x + leftDeadBound + _deltaCenterVec.x;
                    }
                }
                if (_isDeadZoneVertical) {
                    if (delta.y > upperDeadBound) {
                        _tempVec.y = target.position.y - upperDeadBound + _deltaCenterVec.y;
                    }
                    if (delta.y < -lowerDeadBound) {
                        _tempVec.y = target.position.y + lowerDeadBound + _deltaCenterVec.y;
                    }
                }

                if (_isBoundHorizontal) {
                    _tempVec.x = Mathf.Clamp(_tempVec.x, leftBound + _horzExtent, rightBound - _horzExtent);
                }

                if (_isBoundVertical) {
                    _tempVec.y = Mathf.Clamp(_tempVec.y, lowerBound + _vertExtent, upperBound - _vertExtent);
                }

                _tempVec.z = transform.position.z;
                transform.position = _tempVec;
            }
        }
    }

}
