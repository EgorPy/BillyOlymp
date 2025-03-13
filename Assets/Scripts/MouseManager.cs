using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

using RTS_Camera;
using MG_BlocksEngine2.Environment;
// using MG_BlocksEngine2.Core;

public class MouseManager : MonoBehaviour {

    public BE2_ProgrammingEnv programmingEnv;
	public BE2_Canvas _blocksSelectionCanvas;
	public LayerMask triggerLayerToIgnore;
	BE2_HideBlocksSelection hb;
	// BE2_ExecutionManager executionManager;
	// public GameObject programmingEnv;
	CameraMovement RTS_Camera;
	public GameObject selectedObject;
	public GameObject panelBilly;
	public GameObject panelBase;
	public GameObject panelCameraBot;
	public GameObject panelFactory;
	public GameObject panelVally;
	public GameObject panelGunBot;
	Color objColor;
	private int fingerID = -1;
	private bool enabled;

	private void Awake()
	{
	#if !UNITY_EDITOR
    fingerID = 0; 
	#endif
	}

	// Use this for initialization
	void Start () {
		// programmingEnv = GameObject.FindObjectOfType<BE2_ProgrammingEnv>();
		enabled = true;
		RTS_Camera = GameObject.FindObjectOfType<CameraMovement>();
		hb = GameObject.FindObjectOfType<BE2_HideBlocksSelection>();
	}
	
	// Update is called once per frame
	void Update () {
		if (selectedObject != null) {
			Vector3 nextTargetPosition = selectedObject.transform.position;
			Vector3 nextTargetPosition2 = selectedObject.transform.position - RTS_Camera.transform.GetChild(0).forward * 35f;
			if (RTS_Camera.IsInBounds(nextTargetPosition2)) {
                RTS_Camera._targetPosition = nextTargetPosition;
				// Debug.Log(RTS_Camera._targetPosition);
				// RTS_Camera.skipIteration = true;

                // lastPosition2 = transform.GetChild(0).position;
            } else {
                // _targetPosition = lastPosition;
                // _targetPosition = lastPosition;
                RTS_Camera.SetPosition(RTS_Camera.lastPosition);
                // this.gameObject.GetComponent<CameraZoom>().SetZoom(-0.1f);
            }

			// RTS_Camera.SetTargetPosition(selectedObject.transform.position);
		}
        if (Input.GetMouseButton(0) && enabled) {
			// if (EventSystem.current.IsPointerOverGameObject(fingerID)) // && EventSystem.current.currentSelectedGameObject != null)	// is the touch on the GUI
    		// {
       		// GUI Action
			// Debug.Log();
			PointerEventData pointer = new PointerEventData(EventSystem.current);
     		pointer.position = Input.mousePosition;

     		List<RaycastResult> raycastResults = new List<RaycastResult>();
     		EventSystem.current.RaycastAll(pointer, raycastResults);

     		if(raycastResults.Count > 0)
     		{
         		foreach(var go in raycastResults)
         		{  
            		// Debug.Log(go.gameObject.name,go.gameObject);
					// Debug.Log(go.gameObject.tag);
					if (go.gameObject.tag == "Program") {
						return;
					}
         		}
     		}
    		// }
			
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

		    RaycastHit hitInfo;

		    if (Physics.Raycast(ray, out hitInfo, 200f, ~triggerLayerToIgnore)) {
			    //Debug.Log("Mouse is over: " + hitInfo.collider.name );

			    // The collider we hit may not be the "root" of the object
			    // You can grab the most "root-est" gameobject using
			    // transform.root, though if your objects are nested within
			    // a larger parent GameObject (like "All Units") then this might
			    // not work.  An alternative is to move up the transform.parent
			    // hierarchy until you find something with a particular component.

			    GameObject o = hitInfo.transform.root.gameObject; //.transform.GetChild(0).gameObject;
				GameObject hitObject = hitInfo.transform.root.transform.GetChild(0).gameObject;

                if (hitObject.tag == "Selectable")
                {
					// ClearSelection();
					GameObject hitProgram = hitInfo.transform.root.transform.GetChild(1).gameObject;
                    SelectObject(hitObject, hitProgram, o);
                } else {
					ClearSelection();
				}
		    } else {
		        ClearSelection();
		    }
        }
	}

	public void SelectObject(GameObject obj, GameObject hitProgram, GameObject o) {
		if(selectedObject != null) {
			if(obj == selectedObject)
				return;
			// if (programmingEnv != null) {
			// 	programmingEnv.Visible = false;
			// }
			ClearSelection();
		}

		selectedObject = obj;

		panelBase.SetActive(false);
		panelBilly.SetActive(false);
		panelCameraBot.SetActive(false);
		panelFactory.SetActive(false);
		panelVally.SetActive(false);
		panelGunBot.SetActive(false);
		// Debug.Log(o.name);
		switch (o.name) {
			case "BaseComponents":
				panelBase.SetActive(true);
				break;
			case "BillyComponents":
				panelBilly.SetActive(true);
				break;
			case "CameraBotComponents":
				panelCameraBot.SetActive(true);
				break;
			case "FactoryComponents":
				panelFactory.SetActive(true);
				break;
			case "VillyComponents(Clone)":
				panelVally.SetActive(true);
				break;
			case "GunBotComponents(Clone)":
				panelGunBot.SetActive(true);
				break;
			default:
				break;
		}
		if (obj == null) {
			return;
		}
		if (programmingEnv != null) {
			programmingEnv.Visible = false;
			hb._blocksSelectionCanvas.gameObject.SetActive(false);
		}
		programmingEnv = hitProgram.GetComponentInChildren<BE2_ProgrammingEnv>();
		StartCoroutine(disableClick(0.5f));
		
        // programmingEnv.targetObject = obj.GetComponent<BE2_TargetObject>();
		// executionManager.UpdateTargetObjects();
		// I_BE2_TargetObject targetObject = gos[i];

		// Renderer[] rs = selectedObject.GetComponentsInChildren<Renderer>();
		// foreach(Renderer r in rs) {
		// 	Material m = r.material;
		// 	objColor = m.color;
		// 	m.color = Color.green;
		// 	m.DisableKeyword("_EMISSION");
		// 	r.material = m;
		// }
	}

	public void ToggleProgrammingEnvVisiblity() {
		if (programmingEnv != null) {
			if (!programmingEnv.Visible) {
				programmingEnv.Visible = false;
			} else {
				programmingEnv.Visible = true;
			}
		}
	}

	public void ClearSelection() {
		// if (programmingEnv != null) {
		// 	programmingEnv.Visible = false;
		// }
		if (programmingEnv != null) {
			if (programmingEnv.Visible) {
				_blocksSelectionCanvas.gameObject.SetActive(false);
        		programmingEnv.Visible = false;
			} else {
				selectedObject = null;
			}
		}

		StartCoroutine(disableClick(0.5f));
		return;
		if (selectedObject == null) {
			return;
		}

		// Renderer[] rs = selectedObject.GetComponentsInChildren<Renderer>();
		// foreach(Renderer r in rs) {
		// 	Material m = r.material;
		// 	m.color = objColor;
		// 	m.EnableKeyword("_EMISSION");
		// 	r.material = m;
		// }
		_blocksSelectionCanvas.gameObject.SetActive(false);
        programmingEnv.Visible = false;
		if (programmingEnv.Visible) {
			// hb.HideBlocksSelection();
			selectedObject = null;
		}
		if (!programmingEnv.Visible) {
			// hb.HideBlocksSelection();
			selectedObject = null;
		}
	}

	IEnumerator disableClick(float seconds) {
		enabled = false;
        yield return new WaitForSeconds(seconds);
		enabled = true;
	}
}
