using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MG_BlocksEngine2.Block.Instruction;
using MG_BlocksEngine2.Block;
using MG_BlocksEngine2.Environment;

public class BE2_Ins_SpacecraftShoot : BE2_InstructionBase, I_BE2_Instruction
{
    //protected override void OnAwake()
    //{
    //
    //}

    public new bool ExecuteInUpdate => true;
    bool _firstPlay = true;
    float _counter = 0;

    // protected override void OnStart()
    // {
    // }

    public new void Function()
    {
        // if (TargetObject is BE2_TargetObjectSpacecraft3D)
        // {
        if (_firstPlay)
        {
            (TargetObject as BE2_TargetObjectSpacecraft3D).Shoot();
            _counter = 0.2f;
            _firstPlay = false;
        }

        if (_counter > 0)
        {
            TargetObject.Transform.GetComponentInChildren<GunBotGunRotate>().Rotate();
            _counter -= Time.deltaTime;
        }
        else
        {
            _counter = 0;
            _firstPlay = true;
            ExecuteNextInstruction();
        }
        // }
    }

    IEnumerator wait(float seconds)
        {
            yield return new WaitForSeconds(seconds);
            ExecuteNextInstruction();
        }
}
