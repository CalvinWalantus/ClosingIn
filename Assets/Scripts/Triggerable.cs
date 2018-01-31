using UnityEngine;

// Any script that uses a physical trigger should inherit
// from this class. This allows the Trigger class to be 
// universal for any object that can be triggered.

public class Triggerable : MonoBehaviour {

	public virtual void Trigger() {
	
	}
}
