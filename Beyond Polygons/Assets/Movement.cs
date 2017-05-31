using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {

	private AnimationHandler animHandler;
	private Transform cam;

	void Start () {
		animHandler = GetComponent<AnimationHandler> ();
		cam = Camera.main.transform;
	}

	public void Move(Vector2 moveDir) {
		float magnitude = moveDir.magnitude;
		Vector3 transformedMovementDir = cam.TransformDirection (new Vector3(moveDir.x, 0, moveDir.y));
		transformedMovementDir = new Vector3 (transformedMovementDir.x, 0, transformedMovementDir.z).normalized * magnitude;
		transform.LookAt (new Vector3 (transform.position.x + transformedMovementDir.x, transform.position.y, transform.position.z + transformedMovementDir.z));
		animHandler.Move (moveDir);
	}

	public void MoveToAndActiveTrigger(Transform target, string triggerName){
		
	}
}
