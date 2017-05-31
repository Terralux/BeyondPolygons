using UnityEngine;
using System.Collections;

public class RotateAndHover : MonoBehaviour {

	public bool canRotate;
	public bool canFloat;

	[Range(0.1f, 10f)]
	public float hoverDistance;
	[Range(0f, 2f)]
	public float hoverSpeed;

	[Range(1f, 100f)]
	public float rotationSpeed;

	private Vector3 originPosition;
	private bool isGoingUp = true;

	public bool isUsingYAxis;
	private float fraction;

	// Use this for initialization
	void Start () {
		originPosition = transform.position;

	}

	// Update is called once per frame
	void Update () {
		if (canRotate) {
			Rotate ();
		}
		if (canFloat) {
			Hover ();
		}
	}

	private void Rotate(){
		transform.Rotate (isUsingYAxis ? Vector3.up : Vector3.forward, rotationSpeed * Time.deltaTime);
	}

	private void Hover(){
		fraction += Time.deltaTime * hoverSpeed;

		if (fraction >= 1) {
			fraction = 0; 
			isGoingUp = !isGoingUp;
		}

		if (!isGoingUp) {
			transform.position = Vector3.Slerp (originPosition, originPosition + Vector3.up * hoverDistance, fraction);
		} else {
			transform.position = Vector3.Slerp (originPosition + Vector3.up * hoverDistance, originPosition, fraction);
		}
	}
	
}
