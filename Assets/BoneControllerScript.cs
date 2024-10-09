using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BoneControllerScript : MonoBehaviour
{
    // Start is called before the first frame update
    public int boneID;
    //private GameObject bone;
    private float leftDepth;
    private float rightDepth;
    public float averageDepth;
    private float initialLeftDepth;
    private float initialRightDepth;

    //public Material objectMaterial;
    //public Material whiteMaterial;
    //public Material redMaterial;
    private float DEPTH_THRESHOLD = 27.0f; // Depth at which color change starts
    private float MAX_DEPTH = 20.0f; // The maximum depth for full color change

    void Start()
    {
        this.boneID = int.Parse(gameObject.name.Substring(1));

    }

    void Update()
    {

        if (initialLeftDepth != 0)
        {
            if (averageDepth != 0)
            {
                UpDownMove();
            }
        }
    }

    public void SetInitialDepth(float leftInput, float rightInput)
    {
        this.initialLeftDepth = leftInput;
        this.initialRightDepth = rightInput;
    }

    public void SetCurDepth(float leftInput, float rightInput)
    {
        this.leftDepth = leftInput;
        this.rightDepth = rightInput;
        this.averageDepth = (leftDepth + rightDepth) / 2;
    }

    void UpDownMove()
    {
        float moveDist = 0;
        //int maxDistance = 35;

        if (initialLeftDepth - leftDepth <= 0.02)
        {
            moveDist = 0;
        }
        else
        {
            moveDist = initialLeftDepth - averageDepth;

            Vector3 originalPosition = transform.localPosition;

            transform.localPosition = new Vector3(originalPosition.x, originalPosition.y, -moveDist * 0.005f);
        }
    }


    
    public float TransverseRotationDegree()
    { //transverse rotation
        float halfDistance = Math.Abs(leftDepth - rightDepth) / 2;
        float rotateAngle = 0;
        int boneLength = 50;

        if (initialLeftDepth - leftDepth <= 0.02)
        {
            return rotateAngle;
        }
        else
        {
            if (leftDepth == 0 || rightDepth == 0)
            {
                rotateAngle = Mathf.Sin(leftDepth / boneLength);
            }
            else
            {
                rotateAngle = Mathf.Sin(halfDistance / boneLength);
            }

            if (leftDepth > rightDepth)
            {
                //bone.transform.localRotation = Quaternion.Euler(0f, rotateAngle *500f, 0f);
                //UnityDebug.Log("----origin: " + originalDegree + ", rotateAngle: " + rotateAngle);
                return rotateAngle;
            }
            else
            {
                //bone.transform.localRotation = Quaternion.Euler(0f, -rotateAngle *500f, 0f);
                //UnityDebug.Log("----origin: " + originalDegree + ", rotateAngle: " + -rotateAngle);
                return -rotateAngle;
            }
        }
    }

    public float SaggitoRotationDegree(float focusBoneDepth, int focusBoneID)
    {
        float rotateAngle = 0;
        float boneGap = 40; // change here


        float difference = averageDepth - focusBoneDepth;
        //rotateAngle = averageDepth - focusBoneDepth;
        rotateAngle = Mathf.Tan(difference / boneGap);
        if (boneID < focusBoneID)
        {
            //UnityDebug.Log("boneID: " + boneID + "focusbone: " + focusBoneID + " rotateAngle: " + -rotateAngle);
            return -rotateAngle;
        }
        else
        {
            //UnityDebug.Log("boneID: " + boneID + "focusbone: " + focusBoneID + " rotateAngle: " + rotateAngle);
            return rotateAngle;
        }



    }


    [ContextMenu("testing tranvers")]

    public void Rotation(float focusBoneDepth = 1f, int focusBoneID = 1)
    {
        //float xDegree = SaggitoRotationDegree(focusBoneDepth, focusBoneID);
        float yDegree = TransverseRotationDegree();

        Vector3 newRotation = transform.localEulerAngles;
        //newRotation.x = xDegree; // Assuming rotation in the sagittal plane is around the x-axis
        newRotation.y = yDegree;

        transform.localRotation = Quaternion.Euler(0, yDegree * 500f, 0f);
    }
}
