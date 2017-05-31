using UnityEngine;
using System.Collections;

public abstract class LockedState : InteractiveObject {

	public string ATrigger;
	public string XTrigger;
	public string YTrigger;

	public abstract override void Interact (GameObject player);
	
}
