using UnityEngine;
using System.Collections;

public abstract class InteractiveObject : MonoBehaviour {

	public string triggerName;
	public abstract void Interact (GameObject player);

}
