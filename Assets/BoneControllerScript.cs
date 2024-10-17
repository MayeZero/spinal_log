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
    private float THRESHOLD = 0.015f;
    [SerializeField] bool saggitalSide;
    public static int boneLength = 15;
    public static int boneGap = 200;


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
        //int boneLength = 50;

        if (initialLeftDepth - leftDepth <= 0.02)
        {
            return rotateAngle;
        }
        else
        {
            if (leftDepth == 0 || rightDepth == 0)
            {
                rotateAngle = Mathf.Sin(leftDepth / boneLength); // larger length, smaller angle 
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
        //float boneGap = 40; // change here


        float difference = Math.Abs(averageDepth - focusBoneDepth);
        //rotateAngle = averageDepth - focusBoneDepth;
        rotateAngle = Mathf.Tan(difference / boneGap);     // larger boneGap, smaller angle. 
        if (boneID != focusBoneID)
        {
            //UnityDebug.Log("boneID: " + boneID + "focusbone: " + focusBoneID + " rotateAngle: " + -rotateAngle);
            rotateAngle = Math.Max(-THRESHOLD, -rotateAngle);
        }
        else
        {
            //UnityDebug.Log("boneID: " + boneID + "focusbone: " + focusBoneID + " rotateAngle: " + rotateAngle);
            rotateAngle = Math.Min(THRESHOLD, rotateAngle);
        }
        return rotateAngle;



    }

    public void enabledTranverse(int focusBoneID)
    {
        if (boneID != focusBoneID)
        {
            this.gameObject.SetActive(false);
        } else
        {
            this.gameObject.SetActive(true);
        }
    }

    public void Rotation(float focusBoneDepth, int focusBoneID, float highestAngle)
    {

        float xDegree = 0f;
        float yDegree = 0f;

        if (saggitalSide)
        {
            xDegree = SaggitoRotationDegree(focusBoneDepth, focusBoneID) + highestAngle; 
        } else
        {
            enabledTranverse(focusBoneID);
            yDegree = TransverseRotationDegree();
        }


        Vector3 newRotation = transform.localEulerAngles;
        newRotation.x = xDegree; // Assuming rotation in the sagittal plane is around the x-axis
        newRotation.y = yDegree;

        transform.localRotation = Quaternion.Euler(xDegree * 400f, yDegree * 500f, 0f);
    }

    public float TestxDegree;
    public float TestyDegree;
    [ContextMenu("Test Rotation")]
    public void TestRotation()
    {
        if (!saggitalSide)
        {
            TestxDegree = 0f;
        }
        else
        {
            TestyDegree = 0f;
        }


        Vector3 newRotation = transform.localEulerAngles;
        newRotation.x = TestxDegree; // Assuming rotation in the sagittal plane is around the x-axis
        newRotation.y = TestyDegree;

        transform.localRotation = Quaternion.Euler(TestxDegree * 400f, TestyDegree * 500f, 0f);
    }

    //public static void changeBoneLength(int newBoneLength)
    //{
    //    boneLength = newBoneLength;
    //}

    //public static void changeBoneGap(int newBoneGap)
    //{
    //    boneGap = newBoneGap;
    //}
}
