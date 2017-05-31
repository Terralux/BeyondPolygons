using UnityEngine;
using System.Collections;

public class LockedBarArea : MultiplePositionLockedState {

	void Start () {
		base.Start ();
	}

	#region implemented abstract members of MultiplePositionLockedState

	public override void Interact (GameObject player)
	{
		int N = interactionPositions.Length;

		for (int j = N - 1; j > 0; j--) {
			for (int i = 0; i < j; i++) {
				if (Vector3.Distance (player.transform.position, interactionPositions [i].position) > Vector3.Distance (player.transform.position, interactionPositions [i + 1].position)) {
					Transform temp = interactionPositions [i];
					interactionPositions [i] = interactionPositions [i + 1];
					interactionPositions [i + 1] = temp;

					bool tempOpen = positionOpen [i];
					positionOpen [i] = positionOpen [i + 1];
					positionOpen [i + 1] = tempOpen;
				}
			}
		}

		int index = 0;

		while (!positionOpen [index]) {
			index++;
		}

		player.GetComponent<Movement> ().MoveToAndActiveTrigger (interactionPositions [index], triggerName);
	}

	#endregion
}
