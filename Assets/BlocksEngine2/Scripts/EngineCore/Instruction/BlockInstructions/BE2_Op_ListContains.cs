﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using MG_BlocksEngine2.Block.Instruction;
using MG_BlocksEngine2.Block;
using MG_BlocksEngine2.Environment;
using MG_BlocksEngine2.Core;
using MG_BlocksEngine2.Utils;

// v2.10 - Dropdown and InputField references in the default instructions replaced by BE2_Dropdown and BE2_InputField to enable the use of legacy or TMP components as Block inputs
// v2.9 - new block
public class BE2_Op_ListContains : BE2_InstructionBase, I_BE2_Instruction
{
    BE2_Dropdown _dropdown;
    string _lastValue;

    protected override void OnStart()
    {
        _variablesManager = BE2_VariablesListManager.instance;
        _dropdown = BE2_Dropdown.GetBE2Component(GetSectionInputs(0)[0].Transform);
        _dropdown.onValueChanged.AddListener(delegate { _lastValue = _dropdown.GetSelectedOptionText(); });

        BE2_MainEventsManager.Instance.StartListening(BE2EventTypes.OnAnyVariableAddedOrRemoved, PopulateDropdown);

        PopulateDropdown();
    }

    void PopulateDropdown()
    {
        _dropdown.ClearOptions();
        foreach (KeyValuePair<string, List<string>> variable in _variablesManager.lists)
        {
            _dropdown.AddOption(variable.Key);
        }
        _dropdown.RefreshShownValue();
        _dropdown.value = _dropdown.GetIndexOf(_lastValue);
    }

    // v2.9 - the _variablesManager field is mandatory for variable operation blocks to be correcly serialized 
    BE2_VariablesListManager _variablesManager;

    public new string Operation()
    {
        return _variablesManager.ListContainsValue(Section0Inputs[0].StringValue, Section0Inputs[1].StringValue) ? "1" : "0";
    }
}
