using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MG_BlocksEngine2.Block.Instruction;
using MG_BlocksEngine2.Block;
using System.Globalization;

public class BE2_Op_DistanceToGunBot : BE2_InstructionBase, I_BE2_Instruction
{
    bool isGunBotFound;
    float minDistance;
    string distance;
    Transform gunBot;

    public new string Operation()
    {
        // return GameObject.FindGameObjectsWithTag("Enemy").Length.ToString();
        isGunBotFound = false;
        FindGunBot();
        if (isGunBotFound) {
            distance = Mathf.Floor(Vector3.Distance(TargetObject.Transform.position, gunBot.position)).ToString();
        } else {
            distance = "-1";
        }
        // Debug.Log(distance);
        return distance;
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
