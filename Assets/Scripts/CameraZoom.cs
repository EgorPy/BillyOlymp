using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTS_Camera {
    public class CameraZoom : MonoBehaviour {
        [SerializeField] private float _speed = 25f;
        [SerializeField] private float _smoothing = 5f;
        [SerializeField] private Vector2 _range = new (20f, 70f);
        [SerializeField] private Transform _cameraHolder;

        private Vector3 _cameraDirection => transform.InverseTransformDirection(_cameraHolder.forward);

        private Vector3 _targetPosition;
        private float _input;

        float zoomAmount = 0f;

        private void Awake()
        {
            _targetPosition = _cameraHolder.localPosition;
        }

        private void HandleInput() {
            // Debug.Log(zoomAmount);
            if (zoomAmount == 0) {
                _input = Input.GetAxisRaw("Mouse ScrollWheel");
            } else {
                _input = zoomAmount;
                zoomAmount = 0;
            }
        }

        public void Zoom() {
            Vector3 nextTargetPosition = _targetPosition + _cameraDirection * (_input * _speed);
            if (IsInBounds(nextTargetPosition)) _targetPosition = nextTargetPosition;
            _cameraHolder.localPosition = Vector3.Lerp(_cameraHolder.localPosition, _targetPosition, Time.deltaTime * _smoothing);
        }

        private bool IsInBounds(Vector3 position) {
            // Debug.Log(System.Math.Round(position.magnitude, 3));
            return System.Math.Round(position.magnitude, 3) > _range.x && System.Math.Round(position.magnitude, 3) < _range.y;
        }

        public void SetZoom(float zoom) {
            zoomAmount = zoom;
        }

        private void Update() {
            HandleInput();
            Zoom();
        }
    }
}
