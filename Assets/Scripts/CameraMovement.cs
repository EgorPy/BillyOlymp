using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RTS_Camera {
    public class CameraMovement : MonoBehaviour {
        [SerializeField] public float _speed = 1f;
        [SerializeField] private float _smoothing = 5f;
        [SerializeField] public Vector2 _range = new (100, 100);
        [SerializeField] public Vector2 _pos = new (0, 0);
        public float panBoardThickness = 2f;

        public Vector3 _targetPosition;
        public Vector3 _input;
        public Vector3 lastPosition;
        public bool skipIteration = false;
        Vector3 lastPosition2;
        bool needToSetTargetPosition = false;
        public bool enableMovement = true;
        Vector3 newTargetPosition;
        // Vector3 lastTargetObjectPosition;
        MouseManager mm;

        private void Awake()
        {
            _targetPosition = transform.position;
        }

        void Start() {
            mm = GameObject.FindObjectOfType<MouseManager>();
        }

        private void HandleInput() {
            // float x = Input.GetAxisRaw("Horizontal");
            // float z = Input.GetAxisRaw("Vertical");

            Vector3 forward = Vector3.zero;
            Vector3 right = Vector3.zero;
            if (Input.mousePosition.y >= Screen.height - panBoardThickness) {
                forward = transform.forward;
            }
            if (Input.mousePosition.y <= panBoardThickness) {
                forward = -transform.forward;
            }
            if (Input.mousePosition.x >= Screen.width - panBoardThickness) {
                right = transform.right;
            }
            if (Input.mousePosition.x <= panBoardThickness) {
                right = -transform.right;
            }
            // Vector3 right = transform.right * x;
            // Vector3 forward = transform.forward * z;

            _input = (forward + right).normalized;
        }

        private void Move() {
            // if (mm.selectedObject != null) {
            //     Vector3 pos = new Vector3(Mathf.Round(mm.selectedObject.transform.position.x), 
            //                               Mathf.Round(mm.selectedObject.transform.position.y), 
            //                               Mathf.Round(mm.selectedObject.transform.position.z));
            //     if (pos != lastTargetObjectPosition) {
            //         if (transform.position == lastPosition) {
            //             Debug.Log("Billy is movin");
            //             _input = transform.forward.normalized;
            //         }
            //     }
            //     lastTargetObjectPosition = pos;
            // }
            if (mm != null) {
                if (mm.selectedObject == null) {
                    CalculateNextTargetPosition();
                }
            } else {
                CalculateNextTargetPosition();
            }
            
            transform.position = Vector3.Lerp(transform.position, _targetPosition, Time.deltaTime * _smoothing);
            lastPosition = transform.position;
        }

        void CalculateNextTargetPosition() {
            Vector3 nextTargetPosition = _targetPosition + _input * _speed;
            Vector3 nextTargetPosition2 = transform.GetChild(0).position + _input * _speed;
            // transform.position = Vector3.Lerp(transform.position, _targetPosition, Time.deltaTime * _smoothing);
            if (IsInBounds(nextTargetPosition2)) {
                _targetPosition = nextTargetPosition;
                if (needToSetTargetPosition) {
                    _targetPosition = newTargetPosition + _input * _speed;
                    needToSetTargetPosition = false;
                }

                // lastPosition2 = transform.GetChild(0).position;
            } else {
                // _targetPosition = lastPosition;
                // _targetPosition = lastPosition;
                SetPosition(lastPosition);
                // this.gameObject.GetComponent<CameraZoom>().SetZoom(-0.1f);
            }
        }

        public void SetTargetPosition(Vector3 position) {
            Vector3 nextTargetPosition2 = transform.GetChild(0).position;
            if (IsInBounds(nextTargetPosition2)) {
                needToSetTargetPosition = true;
                newTargetPosition = position;
            }
        }

        public void SetPosition(Vector3 position) {
            transform.position = position;
            _targetPosition = position;
            // transform.GetChild(0).position = position2;
            // nextTargetPosition = angle;
        }

        public bool IsInBounds(Vector3 position) {
            return position.x > -_range.x + _pos.x && 
                   position.x < _range.x + _pos.x && 
                   position.z > -_range.y + _pos.y && 
                   position.z < _range.y + _pos.y;
        }

        private void Update()
        {
            if (enableMovement) {
                HandleInput();
            }
            Move();
        }

        private void OnDrawGizmos() {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, 5f);
            Gizmos.DrawWireSphere(transform.GetChild(0).position, 5f);
            Gizmos.DrawWireCube(new Vector3(_pos.x, 0, _pos.y), new Vector3(_range.x * 2f, 5f, _range.y * 2f));
        }
    }
}

// using UnityEngine;

// public class CameraMovement : MonoBehaviour
// {
//     public Transform _cameraObject;
//     public Transform _target;
//     public LayerMask _layer;

//     public float _zoomClampVariable = 3f;
//     public float _rotationSensitivity = 2.3f;

//     public float _upRotationClamp = 50f;
//     public float _downRotationClamp = -15f;

//     [Header("Camera NonTarget Movement")]

//     public float _speed;
//     public float _topDownClamp;

//     private float _startZoomPosition;
//     private float _scrollVar;
//     private Vector2 _turn;
//     private bool _worldDetected;
//     private bool _mouseClicked;




//     // [SerializeField] private float _speed = 1f;
//     [SerializeField] private float _smoothing = 5f;
//     [SerializeField] public Vector2 _range = new (100, 100);
//     [SerializeField] public Vector2 _pos = new (0, 0);
//     public float panBoardThickness = 2f;

//     public Vector3 _targetPosition;
//     private Vector3 _input;
//     Vector3 lastPosition;
//     Vector3 lastPosition2;
//     bool needToSetTargetPosition = false;
//     Vector3 newTargetPosition;
//     public void SetTargetPosition(Vector3 position) {

//     }
//     public void SetPosition(Vector3 position) {

//     }




//     private void Awake()
//     {
//         _startZoomPosition = _cameraObject.transform.localPosition.z;
//         _scrollVar = _startZoomPosition;
//     }
//     void Update()
//     {
//         if (Input.GetKeyDown(KeyCode.Mouse1))
//         {
//             _mouseClicked = true;
//         }
//         if (Input.GetKeyUp(KeyCode.Mouse1)){
//             _mouseClicked = false;
//         }
//         if (_target != null)
//         {
//             CameraPositionUpdater();
//             DetectWorldCollision();
//             ZoomFunction();
//             if (_mouseClicked)
//             {
//                 RotateAround();
//             }
//         }
//         else
//         {
//             Move();
//             if (_mouseClicked)
//             {
//                 Rotate();
//             }
//             _cameraObject.position = Vector3.Lerp( _cameraObject.position,transform.position,Time.deltaTime*5f);
//         }
//     }

//     void DetectWorldCollision()
//     {
//         RaycastHit hit;
//         if (Physics.Raycast(_target.position, _cameraObject.forward * -1, out hit, _scrollVar*-1f,_layer))
//         {
//             if (hit.collider != null)
//             {
//                 _cameraObject.position = Vector3.Lerp(_cameraObject.position, hit.point, Time.deltaTime * 5);
//                 _worldDetected=true;
//             }
//         }
//         else
//         {
//             _worldDetected = false ;
//         }
//     }

//     void CameraPositionUpdater()
//     {
//         transform.position = Vector3.Lerp(transform.position,_target.position + Vector3.up/2f, Time.deltaTime * 5);
//     }

//     void ZoomFunction()
//     {
//         if (!_worldDetected)
//         {
//             float scroll = Input.GetAxis("Mouse ScrollWheel");
//             _scrollVar = Mathf.Clamp(_scrollVar + scroll * 2f, _startZoomPosition, _startZoomPosition+_zoomClampVariable);
//             _cameraObject.localPosition = new Vector3(0f, 0f, _scrollVar);
//         }
//         else
//         {
//             float scroll = Input.GetAxis("Mouse ScrollWheel");
//             if (scroll > 0)
//             {
//                 _scrollVar = Mathf.Clamp(_scrollVar + scroll * 2f, _startZoomPosition, _startZoomPosition + _zoomClampVariable);
//                 _cameraObject.localPosition = new Vector3(0f, 0f, _scrollVar);
//             }
//         }
//     }

//     void RotateAround()
//     {
//         _turn.x += Input.GetAxis("Mouse X")*_rotationSensitivity;
//         _turn.y += Input.GetAxis("Mouse Y")*_rotationSensitivity;
//         _cameraObject.localRotation = Quaternion.Euler(0, 0, 0);
//         transform.localRotation = Quaternion.Euler(Mathf.Clamp(-_turn.y, _downRotationClamp, _upRotationClamp), _turn.x, 0);
//     }

//     private void Move()
//     {
//         float x = Input.GetAxis("Horizontal");
//         float y = Input.GetAxis("Vertical");

//         transform.position += transform.forward * y * _speed;
//         transform.position += transform.right * x * _speed;
//         if (Input.GetKey(KeyCode.E))
//         {
//             transform.position += transform.up * (_speed/2);
//         }
//         if (Input.GetKey(KeyCode.Q))
//         {
//             transform.position -= transform.up * (_speed/2);
//         }
//     }

//     private void Rotate()
//     {
//         _turn.x += Input.GetAxis("Mouse X") * _rotationSensitivity;
//         _turn.y += Input.GetAxis("Mouse Y") * _rotationSensitivity;
//         _cameraObject.localRotation = Quaternion.Euler(Mathf.Clamp(-_turn.y, -_topDownClamp, _topDownClamp), 0, 0);
//         transform.rotation = Quaternion.Euler(0, _turn.x, 0);
//     }
// }
