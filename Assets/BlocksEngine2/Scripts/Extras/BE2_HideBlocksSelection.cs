using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using MG_BlocksEngine2.UI;
using MG_BlocksEngine2.Core;

namespace MG_BlocksEngine2.Environment
{
    // v2.10 - bugfix: hide programmingEnv on WebGl not working properly
    // v2.7 - added a class to the extras that implements the logic for show/hide the Blocks Selection panel  
    public class BE2_HideBlocksSelection : MonoBehaviour
    {
        public BE2_Canvas _blocksSelectionCanvas;
        Vector3 _hidePosition;
        Dictionary<RectTransform, Vector3> _envs = new Dictionary<RectTransform, Vector3>();
	    MouseManager mm;

        void Start()
        {
		    mm = GameObject.FindObjectOfType<MouseManager>();

            // _blocksSelectionCanvas = GetComponentInParent<BE2_Canvas>();
            // if (_blocksSelectionCanvas != null) {
            _hidePosition = (_blocksSelectionCanvas.transform.GetChild(0) as RectTransform).anchoredPosition;
            // }

            GetComponent<Button>().onClick.AddListener(ToggleBlocksSelection);

            foreach (BE2_UI_SelectionButton button in FindObjectsOfType<BE2_UI_SelectionButton>())
            {
                button.GetComponent<Button>().onClick.AddListener(ShowBlocksSelection);
            }

            _envs.Clear();
            foreach (I_BE2_ProgrammingEnv env in BE2_ExecutionManager.Instance.ProgrammingEnvsList)
            {
                _envs.Add(env.Transform.GetComponentInParent<BE2_Canvas>().Canvas.transform.GetChild(0) as RectTransform, (env.Transform.GetComponentInParent<BE2_Canvas>().Canvas.transform.GetChild(0) as RectTransform).anchoredPosition);
            }

            HideBlocksSelection();
        }

        // void Update()
        // {

        // }

        public void ToggleBlocksSelection() {
            if(mm.selectedObject != null) {
                if (!_blocksSelectionCanvas.gameObject.activeSelf) {
                    // ShowBlocksSelection();
                    _blocksSelectionCanvas.gameObject.SetActive(true);
                    mm.programmingEnv.Visible = true;
                } else if (_blocksSelectionCanvas != null) {
                    // HideBlocksSelection();
                    _blocksSelectionCanvas.gameObject.SetActive(false);
                    mm.programmingEnv.Visible = false;
                }
            }
        }

        public void HideBlocksSelection()
        {
            _envs.Clear();
            foreach (I_BE2_ProgrammingEnv env in BE2_ExecutionManager.Instance.ProgrammingEnvsList)
            {
                // Debug.Log(env);
                // Debug.Log(env.Transform);
                try {
                    // if (env == mm.programmingEnv) {
                    //     mm.programmingEnv.Visible = true;
                    // }
                    _envs.Add(env.Transform.GetComponentInParent<BE2_Canvas>().Canvas.transform.GetChild(0) as RectTransform, (env.Transform.GetComponentInParent<BE2_Canvas>().Canvas.transform.GetChild(0) as RectTransform).anchoredPosition);
                } catch (MissingReferenceException exception) {
                }
            }

            if (_blocksSelectionCanvas != null) {
                _blocksSelectionCanvas.gameObject.SetActive(false);
            }

            // foreach (KeyValuePair<RectTransform, Vector3> env in _envs)
            // {
            //     env.Key.anchoredPosition = _hidePosition;
            // }
            
        }

        public void ShowBlocksSelection()
        {
            if (!_blocksSelectionCanvas.gameObject.activeSelf)
            {
                _blocksSelectionCanvas.gameObject.SetActive(true);

                foreach (KeyValuePair<RectTransform, Vector3> env in _envs)
                {
                    if (env.Key != null) {
                        env.Key.anchoredPosition = env.Value;
                    }
                }
            }
        }
    }
}