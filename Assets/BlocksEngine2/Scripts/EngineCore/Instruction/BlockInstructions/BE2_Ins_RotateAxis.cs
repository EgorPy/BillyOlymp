﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MG_BlocksEngine2.Block.Instruction;
using MG_BlocksEngine2.Block;

public class BE2_Ins_RotateAxis : BE2_InstructionBase, I_BE2_Instruction
{
    //protected override void OnAwake()
    //{
    //
    //}
    
    //protected override void OnStart()
    //{
    //    
    //}

    // I_BE2_BlockSectionHeaderInput _input0;
    // string _axisString;
    I_BE2_BlockSectionHeaderInput _input1;
    public new bool ExecuteInUpdate => true;
    bool _firstPlay = true;
    float _counter = 0;

    public new void Function()
    {
        // _input0 = Section0Inputs[0];
        // _axisString = _input0.StringValue;

        // switch (_axisString)
        // {
        // case "X axis":
        //     TargetObject.Transform.Rotate(Vector3.right, _input1.FloatValue);
        //     break;
        // case "Y axis":
        //     TargetObject.Transform.Rotate(Vector3.up, _input1.FloatValue);
        //     break;
        // case "Z axis":
        //     TargetObject.Transform.Rotate(Vector3.forward, _input1.FloatValue);
        //     break;
        // default:
        //     TargetObject.Transform.Rotate(Vector3.up, _input1.FloatValue);
        //     break;
        // }

        if (_firstPlay)
        {
            _input1 = Section0Inputs[0];
            TargetObject.Transform.Rotate(Vector3.up, _input1.FloatValue / 3.5f);
            ExecuteNextInstruction();
            // _counter = 0.02f;
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

        // ExecuteNextInstruction();
    }

    IEnumerator wait(float seconds) {
        yield return new WaitForSeconds(seconds);
        ExecuteNextInstruction();
    }
}
