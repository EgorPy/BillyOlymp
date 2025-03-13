using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MG_BlocksEngine2.Block.Instruction;
using MG_BlocksEngine2.Block;
using MG_BlocksEngine2.Core;

public class BE2_Ins_BuildFactory : BE2_InstructionBase, I_BE2_Instruction
{
    int mineralsCount;
    public float waitSeconds;
    public int factoryCost;
    bool _firstPlay = true;
    float _counter = 0;
    public new bool ExecuteInUpdate => true;
    public GameObject factoryBuilding;
    BE2_ExecutionManager executionManager;
    ObjectsCreatedCount asd;
    Error error;
    Vector3 delPos;
    public LayerMask layerMask;


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

    void BuildFactory() {
        Vector3 pos = new Vector3(TargetObject.Transform.position.x, 0, TargetObject.Transform.position.z) + (TargetObject.Transform.forward * 20);
        delPos = new Vector3(pos.x, 11f, pos.z);
        if (IsLegalPosition(new Vector3(pos.x, 11f, pos.z), new Vector3(10f, 10f, 10f))) {
            Debug.Log("valid position");
            GameObject obj = Instantiate(factoryBuilding, pos, TargetObject.Transform.rotation);
            PlayerPrefs.SetInt("mineralsCount", mineralsCount - factoryCost);
            asd = GameObject.FindObjectOfType<ObjectsCreatedCount>();
            asd.count++;
            // obj.transform.Translate(0, 0, baseBuilding.GetComponentInChildren<Renderer>().bounds.size.z);
        } else {
            error.errorMessage = "Невозможно построить строение здесь";
            Debug.Log("NOt a valid position for building placement");
        }

        // Vector3 pos = TargetObject.Transform.position;
        // GameObject obj = Instantiate(factoryBuilding, new Vector3(pos.x, 0, pos.z), TargetObject.Transform.rotation);
        // obj.transform.Translate(0, 0, factoryBuilding.GetComponentInChildren<Renderer>().bounds.size.z);
        // obj.transform.Rotate(0, 180, 0);
        // executionManager = GameObject.FindObjectOfType<BE2_ExecutionManager>();
        // executionManager.UpdateTargetObjects();
        // executionManager.UpdateProgrammingEnvsList();
        // executionManager.UpdateBlocksStackList();
    }

    bool IsLegalPosition(Vector3 newPos, Vector3 size) {
        Collider[] hitColliders = Physics.OverlapBox(newPos, size, Quaternion.identity, layerMask);
        if (hitColliders.Length > 0)
        {
            //   Debug.Log(hitInfo.collider.name);
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
            if (mineralsCount >= factoryCost) {
                BuildFactory();
                _counter = waitSeconds;
                _firstPlay = false;
            } else {
                error.errorMessage = $"Нехватает минералов, нужно {factoryCost}";
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
