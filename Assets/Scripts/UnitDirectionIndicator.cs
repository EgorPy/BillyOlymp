using UnityEngine;
using System.Collections;

public class UnitDirectionIndicator : MonoBehaviour {

	MouseManager mm;

	// Use this for initialization
	void Start () {
		mm = GameObject.FindObjectOfType<MouseManager>();
	}
	
	// Update is called once per frame
	void Update () {
		if(mm.selectedObject != null) {
			Bounds bigBounds = mm.selectedObject.GetComponentInChildren<Renderer>().bounds;

			// // This "diameter" only works correctly for relatively circular or square objects
			// float diameter = bigBounds.size.z;
			// diameter *= 1.25f;

			this.transform.position = new Vector3(mm.selectedObject.transform.position.x, 0f, mm.selectedObject.transform.position.z);
			float yRot = mm.selectedObject.transform.eulerAngles.y;
			this.gameObject.transform.rotation = Quaternion.Euler(0, yRot, 0);
			if (bigBounds.size.x < 4) {
				this.transform.localScale = new Vector3(4.5f, 1f, 4.5f);
			} else {
				this.transform.localScale = new Vector3(bigBounds.size.x + 0.5f, 1f, bigBounds.size.z + 0.5f);
			}
		} else {
			this.transform.position = new Vector3(0, -10, 0);
		}
	}
}
 