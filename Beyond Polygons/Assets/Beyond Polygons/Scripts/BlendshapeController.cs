using UnityEngine;
using System.Collections;

public class BlendshapeController : MonoBehaviour {

	private SkinnedMeshRenderer skm;
	private static int actorCount = 0;
	private int myIndex = 0;

	private bool isTalking = false;

	public enum Emotion
	{
		NONE,
		HAPPY,
		FROWN,
		WRYSMILE,
		SURPRISED
	}

	private Emotion currentCharacterEmotion = Emotion.NONE;

	private enum Vocals
	{
		BASIS = -1,
		O = 0,
		AH = 1,
		F = 2,
		M = 3,
		I = 4
	}

	private Vocals characterVocalState = Vocals.O;
	private Vocals targetVocalState = Vocals.O;

	private int vocalStateCount = 0;

	private float curValue = 0f;

	[Range(0.05f,1f)]
	public float talkSpeed = 0.214f;

	// Use this for initialization
	void Start () {
		GameObject.FindGameObjectWithTag("VoiceActingMaster").GetComponent<VoiceActingMaster>().enable += OnActorPromptEvent;

		myIndex = actorCount;
		actorCount++;
		skm = GetComponent<SkinnedMeshRenderer> ();

		vocalStateCount = System.Enum.GetValues (typeof(Vocals)).GetLength (0);
		targetVocalState = (Vocals)System.Enum.GetValues(typeof(Vocals)).GetValue(Random.Range(1, vocalStateCount));
	}
	
	// Update is called once per frame
	void Update () {
		if (isTalking) {
			DoTalk ();
		}
	}

	void OnActorPromptEvent(int actorIndex) {
		if (actorIndex == myIndex) {
			isTalking = !isTalking;
		}
	}

	void DoTalk(){
		curValue += Time.deltaTime;
		float fracJourney = curValue / talkSpeed;

		if (fracJourney >= 1f) {
			fracJourney = 0f;
			curValue = 0;
			characterVocalState = targetVocalState;
			targetVocalState = GetNewTargetVocalState ();
		}

		Debug.Log (characterVocalState + " " + fracJourney);

		skm.SetBlendShapeWeight ((int)characterVocalState, (1 - fracJourney) * 100);
		skm.SetBlendShapeWeight ((int)targetVocalState, fracJourney * 100);
	}

	Vocals GetNewTargetVocalState(){
		return (Vocals)System.Enum.GetValues(typeof(Vocals)).GetValue(Random.Range(1, vocalStateCount));
	}
}