using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ClockScript : MonoBehaviour {

	public Transform largeHandle;
	public Transform smallHandle;

	public Text digitalCounter;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		System.DateTime target = System.DateTime.Now;
		smallHandle.localRotation = Quaternion.Euler (new Vector3 (0, -(((float)target.Hour)/24f) * 360, 0));
		largeHandle.localRotation = Quaternion.Euler (new Vector3 (0, -(((float)target.Minute)/60f) * 360, 0));
		digitalCounter.text = target.Hour + ":" + target.Minute;
	}
}
