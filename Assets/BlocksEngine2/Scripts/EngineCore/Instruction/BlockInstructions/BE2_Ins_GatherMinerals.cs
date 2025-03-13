using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MG_BlocksEngine2.Block.Instruction;
using MG_BlocksEngine2.Block;

public class BE2_Ins_GatherMinerals : BE2_InstructionBase, I_BE2_Instruction
{

    public float mineralGatheringDistance;
    int mineralsCount;
    float minDistance;
    public float waitSeconds;
    bool _firstPlay = true;
    float _counter = 0;
    public new bool ExecuteInUpdate => true;
    Error error;

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

    private void Start() {
        mineralsCount = PlayerPrefs.GetInt("mineralsCount");
    }

    bool canGatherMinerals() {
        minDistance = Mathf.Infinity;
        GameObject[] minerals = GameObject.FindGameObjectsWithTag("Mineral");
        foreach(GameObject mineral in minerals) {
            float distance = Vector3.Distance(TargetObject.Transform.position, mineral.GetComponent<Transform>().position);
            if (distance < mineralGatheringDistance) {
                minDistance = distance;
            }
        }
        if (minDistance == Mathf.Infinity) {
            return false;
        } else {
            return true;
        }
    }

    public new void Function() {
        if (canGatherMinerals()) {
            if (_firstPlay)
            {
                mineralsCount = PlayerPrefs.GetInt("mineralsCount");
                mineralsCount++;
                PlayerPrefs.SetInt("mineralsCount", mineralsCount);
                _counter = waitSeconds;
                _firstPlay = false;
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
        } else {
            // Debug.Log("Not in range");
            error.errorMessage = "Нет минералов поблизости";
            ExecuteNextInstruction();
        }
    }
}
