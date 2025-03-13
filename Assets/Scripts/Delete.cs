using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using MG_BlocksEngine2.Core;
using MG_BlocksEngine2.DragDrop;
using MG_BlocksEngine2.Environment;

public class Delete : MonoBehaviour
{
    public bool isDestroyed = false;
    BE2_ExecutionManager executionManager;
    BE2_Raycaster raycaster;
    BE2_ProgrammingEnv programmingEnv;


    // Update is called once per frame
    void Update()
    {
        if (isDestroyed) {
            executionManager = GameObject.FindObjectOfType<BE2_ExecutionManager>();
            raycaster = GameObject.FindObjectOfType<BE2_Raycaster>();
            programmingEnv = GameObject.FindObjectOfType<BE2_ProgrammingEnv>();

            raycaster.RemoveRaycaster(this.gameObject.transform.parent.gameObject.GetComponent<GraphicRaycaster>());
            programmingEnv.UpdateBlocksList();
            Destroy(this.gameObject.transform.parent.gameObject);
            executionManager.UpdateTargetObjects();
            executionManager.UpdateProgrammingEnvsList();
            executionManager.UpdateBlocksStackList();
        }
    }
}
