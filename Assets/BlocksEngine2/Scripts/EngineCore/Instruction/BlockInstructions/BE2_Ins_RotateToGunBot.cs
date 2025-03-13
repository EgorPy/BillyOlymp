using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MG_BlocksEngine2.Block.Instruction;
using MG_BlocksEngine2.Block;

public class BE2_Ins_RotateToGunBot : BE2_InstructionBase, I_BE2_Instruction
{
    I_BE2_BlockSectionHeaderInput _input1;
    public new bool ExecuteInUpdate => true;
    bool _firstPlay = true;
    float _counter = 0;
    float minDistance;
    Transform gunBot;
    Error error;
    bool isGunBotFound = false;

    public override void OnStackActive()
    {
        error = GameObject.FindObjectOfType<Error>();
    }

    public new void Function()
    {
        isGunBotFound = false;
        FindGunBot();
        if (isGunBotFound) {
            TargetObject.Transform.LookAt(gunBot, Vector3.up);
        } else {
            error.errorMessage = "Нет пушкаботов поблизости";
        }
        ExecuteNextInstruction();
    }

    private void FindGunBot() {
        minDistance = Mathf.Infinity;
        GameObject[] enemyObjects = GameObject.FindGameObjectsWithTag("GunBot");
        foreach(GameObject obj in enemyObjects) {
            float distance = Vector3.Distance(TargetObject.Transform.position, obj.GetComponent<Transform>().position);
            if (distance < minDistance) {
                gunBot = obj.transform;
                isGunBotFound = true;
                minDistance = distance;
            }
        }
    }
}
