using UnityEngine;
using UnityEditor;
using System.Collections;
using VocalPrompts;

public class ActorCreatorPopup : PopupWindowContent {
	private VocalPromptClip targetClip;

	private string actorName;
	private Color color;

	public override void OnOpen (){
		VocalPromptEditor window = (VocalPromptEditor)EditorWindow.GetWindow<VocalPromptEditor> ();
		targetClip = window.targetClip;
	}

	public override void OnGUI(Rect rect){
		actorName = EditorGUILayout.TextField ("Name of Actor", actorName);
		color = EditorGUILayout.ColorField ("Actors Color", color);

		if (GUILayout.Button ("Create")) {
			color = new Color (color.r, color.g, color.b, 1);
			targetClip.myActors.Add (new VocalActor (actorName, color));
		}
	}
}
