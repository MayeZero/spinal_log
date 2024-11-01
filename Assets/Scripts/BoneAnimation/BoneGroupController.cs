using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityDebug = UnityEngine.Debug;

public class BoneGroupController : MonoBehaviour
{
    public GameObject boneL2;
    public GameObject boneL3;
    public GameObject boneL4;
    public GameObject boneL5;

    [SerializeField]
    private BluetoothReceiverSuperClass BTManager;
    private bool firstConnect = true;

    private GameObject[] boneGroup;
    private GameObject focusBone;
    // Start is called before the first frame update
    void Start()
    {
        boneGroup = new GameObject[] { boneL2, boneL3, boneL4, boneL5 };
    }

    // Update is called once per frame
    void Update()
    {
        if (BTManager != null)
        {
            // update depth of each sensor, have to store initial distance with no pressure
            if (BTManager.connected && firstConnect)
            {
                //UnityDebug.Log("11111");
                SetInitialBoneDepth(BTManager.converted_data);
                firstConnect = false;
            }
            else
            {
                SetCurBoneDepth(BTManager.converted_data);
            }
            focusBone = FindFocusBoneDepth();

            float highestAngle = 0;

            // count rotation degree
            foreach (GameObject bone in boneGroup)
            {
                float degree = bone.GetComponent<BoneControllerScript>().SaggitoRotationDegree(focusBone.GetComponent<BoneControllerScript>().averageDepth, focusBone.GetComponent<BoneControllerScript>().boneID);
                highestAngle = Mathf.Max(highestAngle, Mathf.Abs(degree));

            }

            foreach (GameObject bone in boneGroup)
            {
                bone.GetComponent<BoneControllerScript>().Rotation(focusBone.GetComponent<BoneControllerScript>().averageDepth, focusBone.GetComponent<BoneControllerScript>().boneID, highestAngle);
            }

        }

    }

    public void SetInitialBoneDepth(float[] depths)
    {
        for (int i = 0; i < boneGroup.Length; i++)
        {
            boneGroup[i].GetComponent<BoneControllerScript>().SetInitialDepth(depths[i * 2], depths[i * 2 + 1]);
        }
    }

    public void SetCurBoneDepth(float[] depths)
    {
        for (int i = 0; i < boneGroup.Length; i++)
        {
            boneGroup[i].GetComponent<BoneControllerScript>().SetCurDepth(depths[i * 2], depths[i * 2 + 1]);
        }
    }

    GameObject FindFocusBoneDepth()
    {
        float smallestDepth = 0;
        GameObject target = null;
        for (int i = 0; i < boneGroup.Length; i++)
        {
            float depth = boneGroup[i].GetComponent<BoneControllerScript>().averageDepth;
            if (smallestDepth == 0 || smallestDepth > depth)
            {
                smallestDepth = depth;
                target = boneGroup[i];
            }
        }

        return target;
    }

    //public void changeBoneLength(int newBoneLength)
    //{
    //    BoneControllerScript.boneLength = newBoneLength;
    //}

    //public void changeBoneGap(int newBoneGap)
    //{
    //    for (int i = 0; i < boneGroup.Length; i++)
    //    {
    //        boneGroup[i].GetComponent<BoneControllerScript>().changeBoneGap(newBoneGap);
    //    }
    //}

    //public int getBoneLength()
    //{
    //    return boneGroup[0].GetComponent<BoneControllerScript>().boneLength; 
    //}

    //public int getBoneGap()
    //{
    //    return boneGroup[0].GetComponent<BoneControllerScript>().boneGap;
    //}
}
