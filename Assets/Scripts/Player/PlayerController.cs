using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        #region
        public Camera cam;
        public bool _run;
        [SerializeField]
        private bool _roll;
        [SerializeField]
        private float playerSpeed;
        [SerializeField]
        private float walkSpeed;
        [SerializeField]
        private float runSpeed;
        private Animator _anim;
        private CharacterController cc;
        private InputManager inputs;
        #endregion

        private void Start() {
            inputs = InputManager.instance;
            cc = GetComponent<CharacterController>();
            _anim = GetComponent<Animator>();
        }

        private void Update(){
            SetPlayerValues();
        }

        private void SetPlayerValues(){
            float inputsMagn = new Vector2(inputs.h,inputs.v).magnitude;
            if(inputsMagn > 0){
                _run = Input.GetKey(KeyCode.LeftShift);
                _anim.SetBool("Run", _run);
                playerSpeed = SpeedChanger();
            }

            // Расчитываем куда повернуть аниацию относительно клавиш на клавиатуре.
            float calcVertical = inputs.v * Mathf.Cos(transform.eulerAngles.y * Mathf.PI / 180)
                + inputs.h * Mathf.Sin(transform.eulerAngles.y * Mathf.PI / 180);
            float calcHorizontal = inputs.h * Mathf.Cos(transform.eulerAngles.y * Mathf.PI / 180)
                - inputs.v * Mathf.Sin(transform.eulerAngles.y * Mathf.PI / 180);

            _anim.SetFloat("Vertical", calcVertical,.1f,Time.deltaTime);
            _anim.SetFloat("Horizontal", calcHorizontal,.1f,Time.deltaTime);
        }

        private void RotatePlayer(){    
            if(!_run){
                Ray cameraRay = cam.ScreenPointToRay(Input.mousePosition);
                Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
                float rayLenght;
                Vector3 pointToLook;
                if(groundPlane.Raycast(cameraRay, out rayLenght)){
                    pointToLook = cameraRay.GetPoint(rayLenght);
                    pointToLook.Set(pointToLook.x, transform.position.y, pointToLook.z);
                    transform.LookAt(pointToLook);
                }   
            } else {
                Vector3 keyboard = new Vector3(inputs.h,0,inputs.v);
                float rotateAngle = -Vector3.SignedAngle(keyboard,Vector3.forward, Vector3.up);
                transform.eulerAngles = Vector3.up * rotateAngle;
            }

            MovePlayer();
        }

        private void MovePlayer(){
            Vector3 movement;
            movement = (Vector3.forward * inputs.v) + (Vector3.right * inputs.h);

            cc.SimpleMove(movement.normalized * playerSpeed);
        }

        void FixedUpdate(){
            RotatePlayer();
        }

        private float SpeedChanger(){
            if(_run){
                return runSpeed;
            }
            return walkSpeed;
        }
    } 
}
