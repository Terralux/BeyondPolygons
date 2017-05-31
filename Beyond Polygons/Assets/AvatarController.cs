using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Movement), typeof(FacialExpressionHandler), typeof(AnimationHandler))]
public class AvatarController : MonoBehaviour {

	private Movement movementScript;
	private FacialExpressionHandler facialScript;

	private bool isInLockedState = false;
	private bool isCurrentlyPossessingAnItem = false;

	[Range(0.1f, 2.2f)]
	public float detectionRange = 1.8f;

	void Start(){
		movementScript = GetComponent<Movement> ();
		facialScript = GetComponent<FacialExpressionHandler> ();
	}

	void Update(){
		if (Input.GetButtonDown ("R3")) {
			facialScript.UpdateEmotion (new Vector2 (Input.GetAxis ("RHorizontal"), Input.GetAxis ("RVertical")));
		}

		if (isInLockedState) {
			if (Input.GetButtonDown ("B")) {
				//Exit locked state
				Debug.Log ("Exiting locked state");
			}
			if (Input.GetButtonDown ("A")) {
				if (isCurrentlyPossessingAnItem) {
					Debug.Log ("Using my item");
				}else{
					Debug.Log ("Doing a random action");
				}
			}
		} else {
			movementScript.Move (new Vector2 (Input.GetAxis ("Horizontal"), Input.GetAxis ("Vertical")));

			if (Input.GetButtonDown ("A")) {
				if (isCurrentlyPossessingAnItem) {
					Debug.Log ("Using my item");
				}else{
					Collider[] collidersHit = Physics.OverlapSphere (transform.position + transform.forward * detectionRange + Vector3.up * 2.2f, detectionRange);

					foreach (Collider col in collidersHit) {
						Debug.Log (col.gameObject.name);
					}
				}
			}
		}
	}

	void OnDrawGizmos(){
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere (transform.position + transform.forward * detectionRange + Vector3.up * 2.2f, detectionRange);
	}

	/*
	 * every character will be able to interact with objects, this means either picking up items or changing from free roaming to a locked state
	 * every character will be able to use an item if they are currently in possession of one
	 * every character will be able to exit a locked state
	 * when in a locked state the character will have an idle animation, along with some additional animations 
	*/

}
