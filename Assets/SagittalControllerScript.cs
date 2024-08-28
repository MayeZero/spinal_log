using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SagittalControllerScript : MonoBehaviour
{
    // Start is called before the first frame update
    public MonoBoneScript L1Bone;
    public MonoBoneScript L2Bone;
    public MonoBoneScript L3Bone;
    public MonoBoneScript L4Bone;
    public MonoBoneScript L5Bone;
    public MonoBoneScript[] bones = new MonoBoneScript[4];
    public int engagedSection;
    public float force;


    void Start()
    {
        Debug.Log("L1 Bone" + L1Bone);
        Debug.Log("L2 Bone" + L2Bone);
        Debug.Log("L3 Bone" +  L3Bone);
        Debug.Log("L4 Bone" + L4Bone);
        //bones.Add(L1Bone);
        //bones.Add(L2Bone);
        //bones.Add(L3Bone);
        //bones.Add(L4Bone);
        //bones.Add(L5Bone);
        bones[0] = L2Bone;
        bones[1] = L3Bone;
        bones[2] = L4Bone;
        bones[3] = L5Bone;
        force = 0.5f;


    }

    // Update is called once per frame
    void Update()
    {

    }

    [ContextMenu("Move up Engaged Bone")]
    public void moveBoneUp()
    {
        bones[engagedSection].moveUp();
    }


    [ContextMenu("Move down Engaged Bone")]
    public void moveBoneDown()
    {
        bones[engagedSection].moveDown();
    }


    [ContextMenu("Reset position")]
    public void resetPosition()
    {
        foreach (var bone in bones)
        {
            bone.resetPosition();
        }

        Debug.Log("Show all: " + bones[0].transform.localPosition);
    }


    [ContextMenu("Move down curve")]
    public void moveCurveBone()
    {
        resetPosition();
        for (int i = 0; i < bones.Length; i++)
        {
            Debug.Log(force / (Mathf.Abs(i - engagedSection) + 1)); // Prints numbers from 0 to 9
            bones[i].moveDown(force / (Mathf.Abs(i - engagedSection) + 1));
        }
    }



    /// <summary>
    /// Move bone down the curve with given force and engagedSection
    /// </summary>
    /// <param name="value"> force input </param>
    /// <param name="engagedSection"> engaged section index </param>
    public void moveCurveBone(float value, int engagedSection)
    {
        resetPosition();
        for (int i = 0; i < bones.Length; i++)
        {
            Debug.Log(value / (Mathf.Abs(i - engagedSection) + 1)); // Prints numbers from 0 to 9
            bones[i].moveDown(value / (Mathf.Abs(i - engagedSection) + 1));
        }
    }



}

