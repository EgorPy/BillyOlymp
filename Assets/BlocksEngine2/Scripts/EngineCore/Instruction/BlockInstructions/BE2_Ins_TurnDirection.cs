using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MG_BlocksEngine2.Block.Instruction;
using MG_BlocksEngine2.Block;

public class BE2_Ins_TurnDirection : BE2_InstructionBase, I_BE2_Instruction
{
    //protected override void OnAwake()
    //{
    //
    //}

    //protected override void OnStart()
    //{
    //    
    //}

    I_BE2_BlockSectionHeaderInput _input0;
    string _value;
    Vector3 _axis = Vector3.up;
    public new bool ExecuteInUpdate => true;
    bool _firstPlay = true;
    float _counter = 0;

    public new void Function()
    {
        if (_firstPlay)
        {
            _input0 = Section0Inputs[0];
            _value = _input0.StringValue;
            if (_value == "Влево")
            {
                TargetObject.Transform.Rotate(_axis, -90);
            }
            else if (_value == "Вправо")
            {
                TargetObject.Transform.Rotate(_axis, 90);
            }
            _counter = 0.25f;
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
    }

    IEnumerator wait(float seconds) {
        yield return new WaitForSeconds(seconds);
        ExecuteNextInstruction();
    }
}
