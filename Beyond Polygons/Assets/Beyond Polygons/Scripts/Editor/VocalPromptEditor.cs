using UnityEngine;
using UnityEditor;
using System.Collections;
using VocalPrompts;

public class VocalPromptEditor : EditorWindow {

	public VocalPromptClip targetClip;

	private float scaleFactor = 0f;
	private float xOffset = 0f;

	private int selectedActorIndex = 0;

	private int timestampIndex = -1;

	private Color defaultColor;

	[MenuItem("Window/VocalPromptEditor")]
	public static void Init(){
		VocalPromptEditor window = (VocalPromptEditor) EditorWindow.GetWindow<VocalPromptEditor> ();
		window.Show ();
	}

	void OnEnable(){
		defaultColor = GUI.color;
		this.wantsMouseMove = true;
	}

	void OnGUI(){
		ShowWindowLayout ();

		if (Event.current.type == EventType.scrollWheel) {
			scaleFactor -= Event.current.delta.y;

			if (scaleFactor < 0f) {
				scaleFactor = 0f;
			}
			Repaint ();
		}

		if (Event.current.type == EventType.mouseDrag) {
			xOffset -= Event.current.delta.x;

			if (xOffset < 0f) {
				xOffset = 0f;
			}
			if (xOffset > (position.width - 70) * (1 + (scaleFactor / 100))) {
				xOffset = (position.width - 70) * (1 + (scaleFactor / 100));
			}
			Repaint ();
		}

		if (Event.current.type == EventType.MouseMove) {
			if (timestampIndex > -1) {
				targetClip.myActors [selectedActorIndex].timeStamps [timestampIndex] += Event.current.delta.x;
				if (targetClip.myActors [selectedActorIndex].timeStamps [timestampIndex] < 0) {
					targetClip.myActors [selectedActorIndex].timeStamps [timestampIndex] = 0;
				}
				if (targetClip.myActors [selectedActorIndex].timeStamps [timestampIndex] > targetClip.duration) {
					targetClip.myActors [selectedActorIndex].timeStamps [timestampIndex] = targetClip.duration;
				}
				Repaint ();
			}
		}

		if (Event.current.type == EventType.mouseDown) {
			if (timestampIndex > -1) {
				timestampIndex = -1;
				Repaint ();
			} 
		}

		if (targetClip != null) {
			GUILayout.BeginArea (new Rect (0, 0, position.width / 5, position.height));{
				GUI.Box (new Rect (0, 0, position.width / 5, position.height),"");
				if (GUI.Button (new Rect (0, 0, position.width / 5, 30), "Add Actor")) {
					PopupWindow.Show (new Rect (), new ActorCreatorPopup ());
				}

				if (targetClip.myActors != null) {
					for (int i = 0; i < targetClip.myActors.Count; i++) {
						GUI.color = targetClip.myActors [i].myColor;
						if (selectedActorIndex != i) {
							if (GUI.Button (new Rect (0, 35 + i * 30, position.width / 5, 30), targetClip.myActors [i].name)) {
								selectedActorIndex = i;
							}
						} else {
							GUI.Box (new Rect (0, 35 + i * 30, position.width / 5, 30), targetClip.myActors [i].name);
						}
					}
					GUI.color = defaultColor;
				}
			}GUILayout.EndArea ();

			GUILayout.BeginArea (new Rect (position.width / 5, 0, (position.width / 5) * 4, position.height));{

				Texture2D tempTexture = new Texture2D (1,1);
				GUI.DrawTexture(new Rect(50 - xOffset, position.height/2, (position.width - 70) * (1 + (scaleFactor/100)), 50), tempTexture);

				GUI.Box(new Rect(50 - xOffset, position.height - 20, (position.width - 70) * (1 + (scaleFactor/100)), 20), tempTexture);

				for (int i = 0; i < 5 + (scaleFactor/10); i++) {
					int targetValue = (int)(targetClip.duration / (5 + (scaleFactor/10))) * i;
					float targetPercentage = targetValue / targetClip.duration;

					float targetXCoord = ((position.width - 70) * (1 + (scaleFactor / 100)) - 50) * targetPercentage;

					GUI.Label (new Rect((50 - xOffset) + targetXCoord, position.height - 15, 40, 30), targetValue.ToString());
					GUI.color = new Color (0.5f, 0.5f, 0.5f, 0.2f);
					GUI.Box (new Rect ((50 - xOffset) + targetXCoord, 0, 1, position.height), "");
					GUI.color = defaultColor;
				}

				if (GUILayout.Button ("Create new Vocal Prompt Clip")) {
					CreateNewVocalPromptClip ();
				}

				for (int i = 0; i < targetClip.myActors.Count; i++) {
					if (i != selectedActorIndex) {
						for (int j = 0; j < targetClip.myActors [i].timeStamps.Count; j++) {
							targetClip.myActors [i].timeStamps [j] = (targetClip.myActors [i].timeStamps [j] > targetClip.duration ? targetClip.duration : targetClip.myActors [i].timeStamps [j]);

							if (j % 2 == 0) {
								GUI.color = new Color (targetClip.myActors [i].myColor.r, targetClip.myActors [i].myColor.g, targetClip.myActors [i].myColor.b, 0.2f);
							} else {
								GUI.color = targetClip.myActors [i].myColor;
							}
							float xPos = (50 - xOffset) + (position.width - 70) * (1 + (scaleFactor / 100)) * (targetClip.myActors [i].timeStamps [j] / targetClip.duration);
							if (GUI.Button (new Rect (xPos, (position.height / 2) - 10 + (i * 15), 10, 10), tempTexture)) {
								timestampIndex = j;
								selectedActorIndex = i;
							}
							GUI.color = defaultColor;
						}
					}
				}

				for (int j = 0; j < targetClip.myActors [selectedActorIndex].timeStamps.Count; j++) {
					targetClip.myActors [selectedActorIndex].timeStamps [j] = (targetClip.myActors [selectedActorIndex].timeStamps [j] > targetClip.duration ? targetClip.duration : targetClip.myActors [selectedActorIndex].timeStamps [j]);

					if (j % 2 == 0) {
						GUI.color = new Color (targetClip.myActors [selectedActorIndex].myColor.r, targetClip.myActors [selectedActorIndex].myColor.g, targetClip.myActors [selectedActorIndex].myColor.b, 0.2f);
					} else {
						GUI.color = targetClip.myActors [selectedActorIndex].myColor;
					}
					float xPos = (50 - xOffset) + (position.width - 70) * (1 + (scaleFactor / 100)) * (targetClip.myActors [selectedActorIndex].timeStamps [j] / targetClip.duration);

					if (j == timestampIndex) {
						GUI.Box (new Rect (xPos, (position.height / 2) - 10 + (selectedActorIndex * 15), 10, 10), tempTexture);
					}else{	
						if (GUI.Button (new Rect (xPos, (position.height / 2) - 10 + (selectedActorIndex * 15), 10, 10), tempTexture)) {
							timestampIndex = j;
						}
					}

					GUI.color = defaultColor;
					if (j == timestampIndex) {
						GUI.Label (new Rect (xPos, (position.height / 2) - 30 + (selectedActorIndex * 15), 60, 30), targetClip.myActors [selectedActorIndex].timeStamps [timestampIndex].ToString ());
					}
				}

			}GUILayout.EndArea();
		} else {
			if (GUILayout.Button ("Create new Vocal Prompt Clip")) {
				CreateNewVocalPromptClip ();
			}
			if (GUILayout.Button ("Open Vocal Prompt Clip")) {
				OpenVocalPromptList ();
			}
		}

		if (Event.current.type == EventType.mouseDown) {
			if (timestampIndex < 0) {
				if (Event.current.control) {
					if (Event.current.button == 0) {
						Vector2 mousePosInEditor = GUIUtility.GUIToScreenPoint (Event.current.mousePosition);
						if (position.Contains (mousePosInEditor)) {
							Vector2 editorSpacePoint = (mousePosInEditor - position.position);
							editorSpacePoint = new Vector2 (editorSpacePoint.x - (position.width / 5) - 50 + xOffset, editorSpacePoint.y);
							Debug.Log (editorSpacePoint);

							float targetPercentageReversed = editorSpacePoint.x / ((position.width - 70) * (1 + (scaleFactor / 100)) - 50);
							float value = (targetPercentageReversed * targetClip.duration) - 10;

							Debug.Log (value);

							targetClip.myActors [selectedActorIndex].timeStamps.Add (value);
							targetClip.myActors [selectedActorIndex].timeStamps.Sort ();

							//timestampIndex = targetClip.myActors [selectedActorIndex].timeStamps.IndexOf (value);

							Repaint ();
						}
					}
					if (Event.current.button == 1) {
						Vector2 mousePosInEditor = GUIUtility.GUIToScreenPoint (Event.current.mousePosition);
						if (position.Contains (mousePosInEditor)) {
							Vector2 editorSpacePoint = (mousePosInEditor - position.position);
							editorSpacePoint = new Vector2 (editorSpacePoint.x - (position.width / 5) - 50 + xOffset, editorSpacePoint.y);
							Debug.Log (editorSpacePoint);

							float targetPercentageReversed = editorSpacePoint.x / ((position.width - 70) * (1 + (scaleFactor / 100)) - 50);
							float value = (targetPercentageReversed * targetClip.duration) - 10;

							float min = targetClip.myActors [selectedActorIndex].timeStamps[0];
							for (int i = 1; i < targetClip.myActors [selectedActorIndex].timeStamps.Count; i++) {
								if (Mathf.Abs(targetClip.myActors [selectedActorIndex].timeStamps [i] - value) < Mathf.Abs(min - value)) {
									min = targetClip.myActors [selectedActorIndex].timeStamps [i];
								}
							}

							targetClip.myActors [selectedActorIndex].timeStamps.Remove (min);
							targetClip.myActors [selectedActorIndex].timeStamps.Sort ();

							Repaint ();
						}
					}
				}
			}
		}
	}

	private void ShowWindowLayout (){
		//GUI.DrawTexture(new Rect( 0, 0, position.x, position.y),)
	}

	private void CreateNewVocalPromptClip(){
		VocalPromptClip VPC = ScriptableObject.CreateInstance<VocalPromptClip>();
		AssetDatabase.CreateAsset (VPC, "Assets/Beyond Polygons/VocalPromptClips/NewVocalPromptClip.asset");
		AssetDatabase.SaveAssets ();

		EditorUtility.FocusProjectWindow ();

		Selection.activeObject = VPC;

		targetClip = VPC;

		if (VPC) {
			VPC.myActors = new System.Collections.Generic.List<VocalActor> ();
			string relPath = AssetDatabase.GetAssetPath (targetClip);
			EditorPrefs.SetString ("ObjectPath", relPath);
		}
	}

	private void OpenVocalPromptList(){
		string absPath = EditorUtility.OpenFilePanel ("Select Vocal Prompt Clip/", Application.dataPath + "/Beyond Polygons/VocalPromptClips","");
		if (absPath.StartsWith (Application.dataPath)) {
			string relPath = absPath.Substring (Application.dataPath.Length - "Assets".Length);
			targetClip = (VocalPromptClip) AssetDatabase.LoadAssetAtPath (relPath, typeof(VocalPromptClip));
			if (targetClip.myActors == null) {
				targetClip.myActors = new System.Collections.Generic.List<VocalActor> ();
			}
			if (targetClip) {
				EditorPrefs.SetString ("ObjectPath", relPath);
			}
		}
	}

}
