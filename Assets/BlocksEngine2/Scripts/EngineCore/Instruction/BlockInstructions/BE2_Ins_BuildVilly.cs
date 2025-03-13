using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MG_BlocksEngine2.Block.Instruction;
using MG_BlocksEngine2.Block;
using MG_BlocksEngine2.Core;

public class BE2_Ins_BuildVilly : BE2_InstructionBase, I_BE2_Instruction
{
    int mineralsCount;
    public float waitSeconds;
    public int villyCost;
    bool _firstPlay = true;
    float _counter = 0;
    public new bool ExecuteInUpdate => true;
    public GameObject villy;
    BE2_ExecutionManager executionManager;
    ObjectsCreatedCount asd;
    float speed = 1f;
    GameObject obj;
    Error error;
    // Vector3 delPos;
    // public LayerMask layerMask;
    public float baseDistance;
    float minDistance;

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

    GameObject BuildVilly() {
        // find base pos
        Vector3 pos = Vector3.zero;
        GameObject[] bases = GameObject.FindGameObjectsWithTag("Base");
        foreach(GameObject b in bases) {
            float distance = Vector3.Distance(TargetObject.Transform.position, b.GetComponent<Transform>().position);
            if (distance < baseDistance) {
                pos = new Vector3(b.transform.position.x, 0f, b.transform.position.z) - (b.transform.forward * 16);
            }
        }

        // delPos = pos;
        // if (IsLegalPosition(new Vector3(pos.x, 5.5f, pos.z), new Vector3(10f, 10f, 10f))) {
        Debug.Log("valid position");
        GameObject go = Instantiate(villy, pos, TargetObject.Transform.rotation);
        PlayerPrefs.SetInt("mineralsCount", mineralsCount - villyCost);
        // GameObject[] objects = Resources.FindObjectsOfTypeAll<GameObject>().Where(obj => obj.name.Contains("Villy"));
        var objects = Resources.FindObjectsOfTypeAll<GameObject>();
        go.transform.GetChild(0).name = "Villy " + objects.Length;
        asd = GameObject.FindObjectOfType<ObjectsCreatedCount>();
        asd.count++;
        return go;
            // obj.transform.Translate(0, 0, baseBuilding.GetComponentInChildren<Renderer>().bounds.size.z);
        // } else {
        //     error.errorMessage = "Невозможно построить строение здесь";
        //     Debug.Log("NOt a valid position for building placement");
        // }

        // Vector3 pos = TargetObject.Transform.position;
        // GameObject obj = Instantiate(villy, new Vector3(pos.x, 2.8f, pos.z), TargetObject.Transform.rotation);
        // // obj.transform.Translate(0, 0, villy.GetComponentInChildren<Renderer>().bounds.size.z);
        // // obj.transform.Rotate(0, 180, 0);
        // obj.transform.Translate(5.5f, 0f, 4f);
        // executionManager = GameObject.FindObjectOfType<BE2_ExecutionManager>();
        // executionManager.UpdateTargetObjects();
        // executionManager.UpdateProgrammingEnvsList();
        // executionManager.UpdateBlocksStackList();
        // return obj;


        // for (int i = 0; i < 4; i++) {
        //     obj.GetComponentInChildren<Rigidbody>().AddForce(obj.transform.forward * speed, ForceMode.VelocityChange);
        // }
        // obj.GetComponentInChildren<Rigidbody>().velocity = Vector3.zero;
        // obj.GetComponentInChildren<Rigidbody>().angularVelocity = Vector3.zero;
        // obj.GetComponentInChildren<Rigidbody>().Sleep();
    }

    bool canBuildVilly() {
        minDistance = Mathf.Infinity;
        GameObject[] bases = GameObject.FindGameObjectsWithTag("Base");
        foreach(GameObject b in bases) {
            float distance = Vector3.Distance(TargetObject.Transform.position, b.GetComponent<Transform>().position);
            if (distance < baseDistance) {
                minDistance = distance;
            }
        }
        if (minDistance == Mathf.Infinity) {
            return false;
        } else {
            return true;
        }
    }

    // bool IsLegalPosition(Vector3 newPos, Vector3 size) {
    //     Collider[] hitColliders = Physics.OverlapBox(newPos, size, Quaternion.identity, layerMask);
    //     if (hitColliders.Length > 0)
    //     {
    //         //   Debug.Log(hitInfo.collider.name);
    //             Debug.Log("HIT");
    //           return false;
    //     }
    //     else
    //     {
    //           Debug.Log("Nothing hit");
    //           return true;
    //     }
    // }

    void SlideForward(GameObject obj, float _absValue) {
        float counter = 0;
        if (counter < _absValue)
        {
            // v2.8 - adjusted the SlideForward function so the TargetObject always end in the same position
            // if (_timer < 1)
            // {
                // _timer += Time.deltaTime / 0.2f;
                // Debug.Log(counter);
                counter += Time.deltaTime;

                // if (_timer > 1)
                //     _timer = 1;

                // TargetObject.Transform.position = 
                obj.GetComponentInChildren<Rigidbody>().AddForce(obj.transform.forward * speed, ForceMode.VelocityChange);
                
                // SpeedControl(obj);
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
            obj.GetComponentInChildren<Rigidbody>().velocity = Vector3.zero;
            obj.GetComponentInChildren<Rigidbody>().angularVelocity = Vector3.zero;
            // if (TargetObject.Transform.name == "Billy") {
            obj.GetComponentInChildren<Rigidbody>().Sleep();
            // }
        }
    }

    private void SpeedControl(GameObject obj) {
        Rigidbody rb = obj.GetComponentInChildren<Rigidbody>();
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        // limit velocity
        if (flatVel.magnitude > speed) {
            Vector3 limitedVel = flatVel.normalized * speed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    public new void Function() {
        if (_firstPlay)
        {
            mineralsCount = PlayerPrefs.GetInt("mineralsCount");
            if (canBuildVilly()) {
                // Debug.Log("true");
                if (mineralsCount >= villyCost) {
                    PlayerPrefs.SetInt("mineralsCount", mineralsCount - villyCost);
                    obj = BuildVilly();
                    _counter = waitSeconds;
                    _firstPlay = false;
                } else {
                    error.errorMessage = $"Нехватает минералов, нужно {villyCost}";
                }
            } else {
                error.errorMessage = $"Нет Базы поблизости";
                ExecuteNextInstruction();
            }
        }

        if (_counter > 0)
        {
            // if (_counter % 2 == 0) {
            if (_counter > (waitSeconds / 1.08f)) {
                obj.GetComponentInChildren<Rigidbody>().AddForce(obj.transform.forward * speed, ForceMode.VelocityChange);
            }
            // }
            _counter -= Time.deltaTime;
        }
        else if (obj != null)
        {
            obj.GetComponentInChildren<Rigidbody>().velocity = Vector3.zero;
            obj.GetComponentInChildren<Rigidbody>().angularVelocity = Vector3.zero;
            // if (TargetObject.Transform.name == "Billy") {
            obj.GetComponentInChildren<Rigidbody>().Sleep();
            _counter = 0;
            _firstPlay = true;
            ExecuteNextInstruction();
        }
    }
}
