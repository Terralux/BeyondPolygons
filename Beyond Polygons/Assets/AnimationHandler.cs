using UnityEngine;
using System.Collections;

public class AnimationHandler : MonoBehaviour {

	private Animator anim;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
	}

	public void Move(Vector2 moveDir){
		anim.SetFloat ("Speed", moveDir.magnitude);
	}
}
