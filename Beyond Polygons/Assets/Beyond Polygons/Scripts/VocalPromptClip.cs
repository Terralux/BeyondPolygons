using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace VocalPrompts{
	[System.Serializable]
	public class VocalActor {
		public string name;
		public List<float> timeStamps = new List<float> ();
		public Color myColor;

		public VocalActor(string name, Color color){
			this.name = name;
			myColor = color;
			timeStamps.Add (0);
		}
	}

	public class VocalPromptClip : ScriptableObject {
		//list of participants
		public List<VocalActor> myActors;
		public float duration = 80f*60f;
	}
}