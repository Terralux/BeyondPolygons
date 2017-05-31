using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using VocalPrompts;

public class VoiceActingMaster : MonoBehaviour {
	
	public VocalPromptClip VPC;

	public delegate void Enable (int actorIndex);
	public Enable enable;

	private int actorCount;

	private float startTime;
	private bool isRunning = false;
	private List<int> actorVoiceEventIndexList = new List<int>(); 

	// Use this for initialization
	void Start () {
		actorCount = VPC.myActors.Count;
		foreach (VocalActor va in VPC.myActors) {
			va.timeStamps.Sort ();
			actorVoiceEventIndexList.Add (0);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (isRunning) {
			for(int i = 0; i < VPC.myActors.Count; i++){
				if (Time.time - startTime >= VPC.myActors [i].timeStamps [actorVoiceEventIndexList [i]]) {
					actorVoiceEventIndexList [i]++;
					enable (i);
				}
			}
		} else {
			if (Input.GetKeyDown (KeyCode.Return)) {
				isRunning = true;
				startTime = Time.time;
			}
		}
	}

	void OnGUI(){
		if(isRunning)
			GUI.Box (new Rect (10, 10, 100, 30), Time.time - startTime + " / " + VPC.duration);
	}
}
