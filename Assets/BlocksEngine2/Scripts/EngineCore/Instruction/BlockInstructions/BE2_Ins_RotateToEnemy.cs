using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MG_BlocksEngine2.Block.Instruction;
using MG_BlocksEngine2.Block;

public class BE2_Ins_RotateToEnemy : BE2_InstructionBase, I_BE2_Instruction
{
    I_BE2_BlockSectionHeaderInput _input1;
    public new bool ExecuteInUpdate => true;
    bool _firstPlay = true;
    float _counter = 0;
    float minDistance;
    Transform enemy;
    Error error;
    bool isEnemyFound = false;

    public override void OnStackActive()
    {
        error = GameObject.FindObjectOfType<Error>();
    }

    public new void Function()
    {
        isEnemyFound = false;
        FindEnemy();
        if (isEnemyFound) {
            TargetObject.Transform.LookAt(enemy, Vector3.up);
        } else {
            error.errorMessage = "Нет врагов поблизости";
        }
        ExecuteNextInstruction();
    }

    private void FindEnemy() {
        minDistance = Mathf.Infinity;
        GameObject[] enemyObjects = GameObject.FindGameObjectsWithTag("Enemy");
        foreach(GameObject obj in enemyObjects) {
            float distance = Vector3.Distance(TargetObject.Transform.position, obj.GetComponent<Transform>().position);
            if (distance < minDistance) {
                enemy = obj.transform;
                isEnemyFound = true;
                minDistance = distance;
            }
        }
    }
}
