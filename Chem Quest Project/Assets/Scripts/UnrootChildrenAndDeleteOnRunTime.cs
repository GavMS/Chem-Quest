using UnityEngine;
public class UnrootChildrenAndDeleteOnRunTime : MonoBehaviour {
	void Awake() {
		while (transform.childCount > 0) {
			transform.GetChild(0).parent = null;
		}
		Destroy(this.gameObject);
	}
}