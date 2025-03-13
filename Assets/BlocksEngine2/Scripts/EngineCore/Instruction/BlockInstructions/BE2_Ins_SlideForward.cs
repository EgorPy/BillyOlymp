using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MG_BlocksEngine2.Block.Instruction;
using MG_BlocksEngine2.Block;

public class BE2_Ins_SlideForward : BE2_InstructionBase, I_BE2_Instruction
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
    float _value;
    float _absValue;
    bool _firstPlay = true;
    public new bool ExecuteInUpdate => true;

    public float speed = 1;

    protected override void OnButtonStop()
    {
        _firstPlay = true;
        _timer = 0;
        _counter = 0;
    }

    public override void OnStackActive()
    {
        _firstPlay = true;
        _timer = 0;
        _counter = 0;
    }

    float _timer = 0;
    float _counter = 0;
    Vector3 _initialPosition;
    
    public new void Function()
    {
        if (_firstPlay)
        {
            _input0 = Section0Inputs[0];
            _value = _input0.FloatValue;
            _absValue = Mathf.Abs(_value) / 10;
            _initialPosition = TargetObject.Transform.position;
            _firstPlay = false;
            StartCoroutine(stop(_absValue / 10));
        }

        if (_counter < _absValue)
        {
            // v2.8 - adjusted the SlideForward function so the TargetObject always end in the same position
            // if (_timer < 1)
            // {
                // _timer += Time.deltaTime / 0.2f;
                _counter += Time.deltaTime;

                // if (_timer > 1)
                //     _timer = 1;

                // TargetObject.Transform.position = 
                if (_value > 0) {
                    TargetObject.Transform.GetComponent<Rigidbody>().AddForce(TargetObject.Transform.forward * speed, ForceMode.VelocityChange);
                } else if (_value < 0) {
                    TargetObject.Transform.GetComponent<Rigidbody>().AddForce(TargetObject.Transform.forward * -speed, ForceMode.VelocityChange);
                }
                SpeedControl();
                // Vector3.Lerp(_initialPosition, _initialPosition + (TargetObject.Transform.forward * (_value / _absValue)), _timer);
            // }
            // else
            // {
            //     _timer = 0;
            //     _counter++;
            //     _firstPlay = true;
            // }
        }
        else
        {
            finish();
        }
    }

    private void SpeedControl() {
        Rigidbody rb = TargetObject.Transform.GetComponent<Rigidbody>();
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        // limit velocity
        if (flatVel.magnitude > speed) {
            Vector3 limitedVel = flatVel.normalized * speed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    private void finish() {
        TargetObject.Transform.GetComponent<Rigidbody>().velocity = Vector3.zero;
        TargetObject.Transform.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        TargetObject.Transform.GetComponent<Rigidbody>().Sleep();
        // if (TargetObject.Transform.name == "Billy") {
        // }
        ExecuteNextInstruction();
        _counter = 0;
        _timer = 0;
        _firstPlay = true;
    }

    IEnumerator stop(float seconds) {
        yield return new WaitForSeconds(seconds);
        finish();
    }
}
