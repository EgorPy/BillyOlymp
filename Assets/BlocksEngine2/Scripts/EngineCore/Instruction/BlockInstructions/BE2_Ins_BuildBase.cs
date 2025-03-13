using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MG_BlocksEngine2.Block.Instruction;
using MG_BlocksEngine2.Block;
using MG_BlocksEngine2.Core;

public class BE2_Ins_BuildBase : BE2_InstructionBase, I_BE2_Instruction
{
    int mineralsCount;
    public float waitSeconds;
    public LayerMask layerMask;
    public int baseCost;
    bool _firstPlay = true;
    float _counter = 0;
    public new bool ExecuteInUpdate => true;
    public GameObject baseBuilding;
    BE2_ExecutionManager executionManager;
    ObjectsCreatedCount asd;
    Error error;

    Vector3 delPos;

    // building placement variables
    public int gridSize = 20;
    bool validPosition = true;

    protected override void OnButtonStop()
    {
        _firstPlay = true; 
        _counter = 0;
    }

    public override void OnStackActive()
    {
        _firstPlay = true;
        _counter = 0;
        error = GameObject.FindObjectOfType<Error>();
    }

    void BuildBase() {
        Vector3 pos = new Vector3(TargetObject.Transform.position.x, 0, TargetObject.Transform.position.z) + (TargetObject.Transform.forward * 20);
        delPos = new Vector3(pos.x, 11f, pos.z);
        if (IsLegalPosition(new Vector3(pos.x, 11f, pos.z), new Vector3(10f, 10f, 10f))) {
            // Debug.Log("valid position");
            GameObject obj = Instantiate(baseBuilding, pos, TargetObject.Transform.rotation);
            asd = GameObject.FindObjectOfType<ObjectsCreatedCount>();
            asd.count++;
            PlayerPrefs.SetInt("mineralsCount", mineralsCount - baseCost);
            // obj.transform.Translate(0, 0, baseBuilding.GetComponentInChildren<Renderer>().bounds.size.z);
        } else {
            error.errorMessage = "Невозможно построить строение здесь";
            // Debug.Log("NOt a valid position for building placement");
        }
        

        // // Vector3 pos = TargetObject.Transform.position;
        // // Quaternion rot = TargetObject.Transform.rotation;
        // int x = (int)(TargetObject.Transform.position.x / gridSize) * gridSize;
        // int z = (int)(TargetObject.Transform.position.z / gridSize) * gridSize;
        // // int y = (int)(Mathf.Round(TargetObject.Transform.localRotation.y / 90) * 90);
        // // Debug.Log(TargetObject.Transform.localRotation.y);
        // // Debug.Log(TargetObject.Transform.rotation.y);
        // GameObject obj = Instantiate(baseBuilding, new Vector3(x, 0, z), TargetObject.Transform.rotation);
        // // obj.transform.Rotate(0, y, 0);
        // obj.transform.Translate(0, 0, 20);
        // // Debug.Log(TargetObject.Transform.rotation);
        // // Debug.Log(TargetObject.Transform.position);
        // // Debug.Log(TargetObject.Transform.localRotation);
        // // Debug.Log(TargetObject.Transform.localPosition);
        // // Debug.Log(rot.y);
        // // executionManager = GameObject.FindObjectOfType<BE2_ExecutionManager>();
        // // executionManager.UpdateTargetObjects();
        // // executionManager.UpdateProgrammingEnvsList();
        // // executionManager.UpdateBlocksStackList();
    }

    bool IsLegalPosition(Vector3 newPos, Vector3 size) {
        Collider[] hitColliders = Physics.OverlapBox(newPos, size, Quaternion.identity, layerMask);
        if (hitColliders.Length > 0)
        {
            for (int i = 0; i < hitColliders.Length; i++) {

              Debug.Log(hitColliders[i].GetComponent<Collider>().name);
            }
                Debug.Log("HIT");
              return false;
        }
        else
        {
              Debug.Log("Nothing hit");
              return true;
        }
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(delPos, new Vector3(10f, 10f, 10f));
    }

    public new void Function() {
        if (_firstPlay)
        {
            mineralsCount = PlayerPrefs.GetInt("mineralsCount");
            if (mineralsCount >= baseCost) {
                BuildBase();
                _counter = waitSeconds;
                _firstPlay = false;
            } else {
                error.errorMessage = $"Нехватает минералов, нужно {baseCost}";
            }
        }

        if (_counter > 0)
        {
            _counter -= Time.deltaTime;
        }
        else
        {
            _counter = 0;
            _firstPlay = true;
            ExecuteNextInstruction();
        }
    }
}
