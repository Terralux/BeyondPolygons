using UnityEngine;
using System.Collections;

public class FacialExpressionHandler : MonoBehaviour {



	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void UpdateEmotion(Vector2 targetPosition){
		Debug.Log ("Facial Expression: " + targetPosition);
	}
}
