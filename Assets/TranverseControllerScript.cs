using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TranverseControllerScript : MonoBehaviour
{

    public float rotationSpeed = 0.05f;
    public RotateBoneScript L1Bone;
    public RotateBoneScript L2Bone;
    public RotateBoneScript L3Bone;
    public RotateBoneScript L4Bone;
    public RotateBoneScript L5Bone;
    public int engaged = 0;
    public string rotateDir = "left";
    public RotateBoneScript[] bones = new RotateBoneScript[5];

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("test bone rotation");
        //bones.Add(L1Bone);
        //bones.Add(L2Bone);
        //bones.Add(L3Bone);
        //bones.Add(L4Bone);
        //bones.Add(L5Bone);
        bones = new RotateBoneScript[5];
        bones[0] = L1Bone;
        bones[1] = L2Bone;
        bones[2] = L3Bone;
        bones[3] = L4Bone;
        bones[4] = L5Bone;

    }

    // Update is called once per frame
    void Update()
    {
        
    }



    [ContextMenu("Reset rotation")]
    public void resetRotation()
    {
        foreach (var bone in bones)
        {
            bone.SetActive(true);
            bone.resetRotation();
        }

    }


    [ContextMenu("Move bone")]
    public void rotateBone()
    {
        rotate(engaged, rotationSpeed);
    }




    public void rotate(int index, float degree)
    {
        resetRotation();
        for (int i = 0; i < bones.Length; i++)
        {
            if (i != index)
            {
                bones[i].SetActive(false);
            }
        }

        bones[index].SetActive(true);
        bones[index].rotateDegree(degree);
    }

    //public void Rotation(float leftDepth, float rightDepth)
    //{
    //    float yDegree = TransverseRotationDegree(leftDepth, rightDepth);
    //    //UnityDebug.Log("xDegree: " + xDegree);
    //    //UnityDebug.Log("yDegree: " + yDegree);
    //    Vector3 newRotation = transform.localEulerAngles;
    //    newRotation.y = yDegree;
    //    //transform.localEulerAngles = newRotation;
    //    //UnityDebug.Log(boneID + " localEulerAngles: " + transform.localEulerAngles);          
    //    transform.localRotation = Quaternion.Euler(xDegree * 400f, yDegree * 500f, 0f);
    //    //transform.Rotate(xDegree*40000f, 0, 0, Space.Self);
    //}


    public float TransverseRotationDegree(float initialLeftDepth, float leftDepth, float rightDepth)
    { //transverse rotation
        float halfDistance = System.Math.Abs(leftDepth - rightDepth) / 2;
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


}
