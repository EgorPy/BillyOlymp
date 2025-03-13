using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RTS_Camera {
    public class CameraRotation : MonoBehaviour {
        [SerializeField] private float _speed = 15f;
        [SerializeField] private float _smoothing = 5f;
        CameraMovement cm;

        public float _targetAngle;
        private float _currentAngle;
        float lastAngle;
        Quaternion lastRotation;

        private void Awake() {
            _targetAngle = transform.eulerAngles.y;
            _currentAngle = _targetAngle;
            cm = GameObject.FindObjectOfType<CameraMovement>();
        }

        private void HandleInput() {
            if (!Input.GetMouseButton(1)) return;
            _targetAngle += Input.GetAxisRaw("Mouse X") * _speed;
        }

        public void SetRotation(float angle) {
            transform.rotation = Quaternion.Euler(0, angle, 0);
            _targetAngle = angle;
            _currentAngle = angle;
        }

        private void Rotate() {
            _currentAngle = Mathf.Lerp(_currentAngle, _targetAngle, Time.deltaTime * _smoothing);
            transform.rotation = Quaternion.AngleAxis(_currentAngle, Vector3.up);
            if (!IsInBounds(transform.GetChild(0).position)) {
                SetRotation(lastAngle);
                // _currentAngle = lastAngle;
            } else {
                lastAngle = _currentAngle;
            }
            lastRotation = transform.rotation;
        }

        private void Update() {
            HandleInput();
            Rotate();
        }

        private bool IsInBounds(Vector3 position) {
            return position.x > -cm._range.x + cm._pos.x && 
                   position.x < cm._range.x + cm._pos.x && 
                   position.z > -cm._range.y + cm._pos.y && 
                   position.z < cm._range.y + cm._pos.y;
        }
    }
}
