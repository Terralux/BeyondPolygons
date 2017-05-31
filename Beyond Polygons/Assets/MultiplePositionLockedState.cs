using UnityEngine;
using System.Collections;

public abstract class MultiplePositionLockedState : LockedState {
	public Transform[] interactionPositions;
	protected bool[] positionOpen;

	protected void Start () {
		positionOpen = new bool[interactionPositions.Length];
	}

	public abstract override void Interact (GameObject player);
}
