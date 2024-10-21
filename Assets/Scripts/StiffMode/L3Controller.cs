using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using System;
using UnityEngine;
using UnityDebug = UnityEngine.Debug;
using UnityEngine.Analytics;

public class L3Controller : MonoBehaviour
{
    public GameObject L3;
    //private GameObject bone;

    public float changeDepth;

    public float transverseAngle;

    public float saggitalAngle;
    [SerializeField] bool saggital;
    private bool firstConnect = true;
    public static float boneMoveValue = 1f;


    [SerializeField]
    private L3BlueToothDataReceiver BTManager;

    // Update is called once per frame
    void Update()
    {
        
        if(BTManager != null){
            // update depth of each sensor, have to store initial distance with no pressure
            if (BTManager.connected && firstConnect)
            {
                // SetData(BTManager.converted_data);
                firstConnect = false;
            }

            SetData(BTManager.converted_data);

            //Rotation();

            //UnityDebug.Log("average: "+ averageChangeDepth);
            if (changeDepth > 0.5) {
                if (saggital)
                {
                    UpDownMove();
                }
                else
                {
                    Rotation();
                }
            }
        }
        
    }


    public void SetData(float[] input) {
        this.changeDepth = input[0];
        this.transverseAngle = input[1];
        this.saggitalAngle = input[2];
    }

    [ContextMenu("Test updown")]
    void UpDownMove() {
        float moveDist = 0;
        //int maxDistance = 35;

        if (changeDepth > 0.5)
        {
            
            moveDist = changeDepth;

            Vector3 originalPosition = L3.transform.localPosition;

        
            L3.transform.localPosition = new Vector3(originalPosition.x, originalPosition.y, moveDist * boneMoveValue);
        }       
    }

    [ContextMenu("Test rotate")]
    void Rotation() { //transverse rotation

        L3.transform.localRotation = Quaternion.Euler(saggitalAngle, transverseAngle, 0f);
 
        //UnityDebug.Log("rotate++++++++++++++++++++++++++++");
        
    }
}
