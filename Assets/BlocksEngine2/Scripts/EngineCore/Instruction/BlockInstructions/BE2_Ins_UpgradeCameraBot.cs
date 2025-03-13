using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MG_BlocksEngine2.Block.Instruction;
using MG_BlocksEngine2.Block;
using MG_BlocksEngine2.Core;
using MG_BlocksEngine2.DragDrop;
using MG_BlocksEngine2.Environment;

public class BE2_Ins_UpgradeCameraBot : BE2_InstructionBase, I_BE2_Instruction
{
    int mineralsCount;
    float minDistance;
    public float waitSeconds;
    public int camerabotCost;
    public float upgradeCameraBotDistance;
    public GameObject vally;
    bool _firstPlay = true;
    float _counter = 0;
    public new bool ExecuteInUpdate => true;
    BE2_ExecutionManager executionManager;
    float speed = 1f;
    GameObject obj;
    MouseManager mm;

    protected override void OnButtonStop()
    {
        _firstPlay = true; 
        _counter = 0;
    }

    public override void OnStackActive()
    {
        _firstPlay = true;
        _counter = 0;
    }

    void Start() {
        mm = GameObject.FindObjectOfType<MouseManager>();
    }

    GameObject UpgradeCameraBot() {
        Vector3 pos = TargetObject.Transform.position;
        obj = Instantiate(vally, new Vector3(pos.x, 1.5f, pos.z), TargetObject.Transform.rotation);
        // obj.transform.Translate(0, 0, camerabot.GetComponentInChildren<Renderer>().bounds.size.z);
        // obj.transform.Rotate(0, 180, 0);
        // obj.transform.Translate(5.5f, 0f, 4f);
        executionManager = GameObject.FindObjectOfType<BE2_ExecutionManager>();
        executionManager.UpdateTargetObjects();
        executionManager.UpdateProgrammingEnvsList();
        executionManager.UpdateBlocksStackList();
        return obj;
        // for (int i = 0; i < 4; i++) {
        //     obj.GetComponentInChildren<Rigidbody>().AddForce(obj.transform.forward * speed, ForceMode.VelocityChange);
        // }
        // obj.GetComponentInChildren<Rigidbody>().velocity = Vector3.zero;
        // obj.GetComponentInChildren<Rigidbody>().angularVelocity = Vector3.zero;
        // obj.GetComponentInChildren<Rigidbody>().Sleep();
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

    bool canUpgradeCameraBot() {
        minDistance = Mathf.Infinity;
        GameObject[] camerabots = GameObject.FindGameObjectsWithTag("CameraBot");
        foreach(GameObject camerabot in camerabots) {
            float distance = Vector3.Distance(TargetObject.Transform.position, camerabot.transform.GetChild(0).GetComponent<Transform>().position);
            Debug.Log(distance);
            if (distance < upgradeCameraBotDistance) {
                minDistance = distance;
                // delete CameraBot to build Vally
                mm.selectedObject = null;
                BE2_ExecutionManager.Instance.ProgrammingEnvsList.Remove(camerabot.GetComponentInChildren<BE2_ProgrammingEnv>());
                BE2_ExecutionManager.Instance.ProgrammingEnvsList.Remove(camerabot.GetComponentInChildren<I_BE2_ProgrammingEnv>());
                // BE2_ExecutionManager.Instance.ProgrammingEnvsList.Remove(camerabot.GetComponentInChildren<Canvas>());
                executionManager = GameObject.FindObjectOfType<BE2_ExecutionManager>();
                executionManager.RemoveFromBlocksStackList(camerabot.GetComponentInChildren<I_BE2_BlocksStack>());
                executionManager.UpdateTargetObjects();
                executionManager.UpdateProgrammingEnvsList();
                executionManager.UpdateBlocksStackList();
                Destroy(camerabot);
                return true;
            }
        }
        if (minDistance == Mathf.Infinity) {
            return false;
        } else {
            return true;
        }
    }

    public new void Function() {
        if (_firstPlay)
        {
            if (canUpgradeCameraBot()) {
                mineralsCount = PlayerPrefs.GetInt("mineralsCount");
                if (mineralsCount >= camerabotCost) {
                    PlayerPrefs.SetInt("mineralsCount", mineralsCount - camerabotCost);
                    obj = UpgradeCameraBot();
                    _counter = waitSeconds;
                    _firstPlay = false;
                }
            } else {
                // Debug.Log("Not in range");
                ExecuteNextInstruction();
                return;
            }
        }

        if (_counter > 0)
        {
            // if (_counter % 2 == 0) {
            if (_counter > (waitSeconds / 1.02f)) {
                obj.GetComponentInChildren<Rigidbody>().AddForce(obj.transform.forward * speed, ForceMode.VelocityChange);
            }
            // }
            _counter -= Time.deltaTime;
        }
        else
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
