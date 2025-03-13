using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MG_BlocksEngine2.Block.Instruction;
using MG_BlocksEngine2.Block;
using System.Globalization;

public class BE2_Op_EnemiesCount : BE2_InstructionBase, I_BE2_Instruction
{
    public new string Operation()
    {
        return GameObject.FindGameObjectsWithTag("Enemy").Length.ToString();
    }
}
