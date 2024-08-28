using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TranverseControllerScript : MonoBehaviour
{

    public float rotationSpeed = 100f;
    public RotateBoneScript L1Bone;
    public RotateBoneScript L2Bone;
    public RotateBoneScript L3Bone;
    public RotateBoneScript L4Bone;
    public RotateBoneScript L5Bone;
    public int engaged = 0;
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
            bone.resetRotation();
        }

    }


    [ContextMenu("Move right")]
    public void moveRight()
    {
        bones[engaged].moveRight(rotationSpeed);
    }

    [ContextMenu("Move left")]
    public void moveLeft()
    {
        bones[engaged].moveLeft(rotationSpeed);
    }

}
