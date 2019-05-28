using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player{
	public class CameraMovement : MonoBehaviour {

		#region Variables
			public bool freeze = false;

			public PlayerController target;
			[SerializeField]
			private float m_Smooth;
			
			private Vector3 velocity = Vector3.zero;

			[SerializeField]
			private Transform intermediatePoint;
			private Vector3 sumOfVectors;
			public Vector3 offset;
			Vector3 runOffset;
			
			Vector3 flatTargetPosition;
			Vector3 finalPosition;

			public float maxDistance;

			private InputManager inputs;
		#endregion

		private void Start() {
			inputs = InputManager.instance;
		}

		void Update(){
			if(!target){
				return;
			}

			if(UIManager.instance.pause){
                return;
            }

			HandleCamera();
		}

		protected virtual void HandleCamera(){
			if(!target._run){
				Ray cameraRay = GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
				Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
				float rayLenght;
				if(groundPlane.Raycast(cameraRay, out rayLenght)){
					Vector3 rayPoint = cameraRay.GetPoint(rayLenght);
					sumOfVectors = (target.transform.position + new Vector3(rayPoint.x,0,rayPoint.z)) / 2;
					float dist = (target.transform.position - sumOfVectors).magnitude;
					if(dist <= maxDistance){
						intermediatePoint.position = sumOfVectors;
					}
					dist = 0;
				}
			} else {
				intermediatePoint.position = target.transform.position + new Vector3(inputs.h,0,inputs.v) * 3f;
			}

			flatTargetPosition = intermediatePoint.position;
			flatTargetPosition.y = 0;
			finalPosition = flatTargetPosition + offset;
		}

		void FixedUpdate(){
			if(!target){
				return;
			}

			transform.position = Vector3.SmoothDamp(transform.position,finalPosition, ref velocity, m_Smooth);
		}
	}
}
