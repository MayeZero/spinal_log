//using System;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class Spine
//{
//    String[] labelsBones = { "L2", "L3", "L4", "L5" };
//    public ArrayList<Vertebra> vertebrae = new ArrayList<Vertebra>();
//    int numBones = labelsBones.length;

//    float[] sensors_loads = new float[8]; //30
//    float[] sensors_displacements = new float[8]; //30
//    int numSensors = 8; //30
//    int numSections = numSensors / 2; // numSensors/3
//    float[] sectional_displacements = new float[4]; //10
//    float[] sectional_loads = new float[4]; //10
//    int focusSectionIndex;
//    int focusVertebraIndex;

//    float distLRSensors = 20; //distance between left and right sensors

//    void setData(float[] sensors_loads, float[] sensors_displacements)
//    { //displacements store the individual displacement of each sensor
//        this.sensors_loads = sensors_loads;
//        this.sensors_displacements = sensors_displacements;

//        computeVertebraePoses();
//        computeVertebraeLoads();
//    }

//    void computeVertebraePoses()
//    {
//        //Computing sections displacements and finding the section with the largest displacement in the same loop
//        this.focusSectionIndex = 0;

//        for (int i = 0; i < this.numSections; i++)
//        {
//            int indexLeft = i * 2 + 0;
//            int indexRight = i * 2 + 1;
//            //////////////////////////////////////////////////////////////////////////////
//            this.sectional_displacements[i] = (this.sensors_displacements[indexLeft] + this.sensors_displacements[indexRight]) / 2; //this algorithm may need revision
//            //////////////////////////////////////////////////////////////////////////////
//            if (Math.Abs(this.sectional_displacements[i]) > Math.Abs(this.sectional_displacements[this.focusSectionIndex]))
//            {
//                this.focusSectionIndex = i;
//            }
//        }

//        //////////////Computing individual vertebral displacement and orientation in both planes

//        for (int i = 0; i < numBones; i++)
//        {

//            float vertebralDisp;
//            float sagTilt;
//            float heightsDiff;  //difference between left side and right side
//            float transTilt;

//            float leftDisp, rightDisp;

//            if (i == 0)
//            { //the first bone
//                sagTilt = (float)((sectional_displacements[i] - sectional_displacements[i + 1]) * 0.05); ///0.05 random factor, tune it manually
//            }
//            else if (i == numBones - 1)
//            { //the last bone
//                sagTilt = (float)((sectional_displacements[i - 1] - sectional_displacements[i]) * 0.05);
//            }
//            else
//            {
//                sagTilt = (float)((sectional_displacements[i - 1] - sectional_displacements[i + 1]) * 0.05);
//            }

//            vertebralDisp = sectional_displacements[i];

//            leftDisp = sensors_displacements[i * 2];
//            rightDisp = sensors_displacements[(i * 2) + 1];
//            vertebrae.get(i).setDisplacement(vertebralDisp);

//            vertebrae.get(i).setSagittalTilt(sagTilt);

//            heightsDiff = leftDisp - rightDisp;
//            transTilt = (float)Math.Atan(heightsDiff / distLRSensors);
//            vertebrae.get(i).setTransverseTilt(transTilt);

//            vertebrae.get(i).setLeftDisplacement(leftDisp);
//            vertebrae.get(i).setRightDisplacement(rightDisp);
//        }
//    }

//    void computeVertebraeLoads()
//    {

//        for (int i = 0; i < this.numSections; i++)
//        {
//            int indexLeft = i * 2 + 0;
//            int indexRight = i * 2 + 1;
//            //////////////////////////////////////////////////////////////////////////////
//            this.sectional_loads[i] = (this.sensors_loads[indexLeft] + this.sensors_loads[indexRight]) / 2; //this algorithm may need revision
//            //////////////////////////////////////////////////////////////////////////////
//        }

//        for (int i = 0; i < numBones; i++)
//        {

//            float vertebralLoad;
//            float leftLoad, rightLoad;

//            vertebralLoad = sectional_loads[i];
//            leftLoad = sensors_loads[i * 2];
//            rightLoad = sensors_loads[(i * 2) + 1];

//            vertebrae.get(i).setLoad(vertebralLoad);
//            vertebrae.get(i).setLeftLoad(leftLoad);
//            vertebrae.get(i).setRightLoad(rightLoad);
//        }
//    }



//    float getSensorDisplacement(int sensorIndex)
//    {
//        return this.sensors_displacements[sensorIndex];
//    }

//    float getSensorLoad(int sensorIndex)
//    {
//        return this.sensors_loads[sensorIndex];
//    }

//    int getFocusVertebraIndex()
//    {
//        return this.focusVertebraIndex;
//    }

//    float getFocusVertebraLoad()
//    {
//        return getVertebra(focusVertebraIndex).getLoad(); //temporary function, implement a proper sectional_force array or vertebral force for each vertebra
//    }

//    float getFocusVertebraDisplacement()
//    {
//        return getVertebra(focusVertebraIndex).getDisplacement();
//    }

//    Vertebra getVertebra(String label)
//    {

//        for (Vertebra _item : vertebrae)
//        {
//            if (_item.getLabel().equals(label))
//                return _item;
//        }
//        return null;
//    }

//    Vertebra getVertebra(int numLabel)
//    {
//        return vertebrae.get(numLabel);
//    }

//    int getNumBones()
//    {
//        return this.numBones;
//    }
//}

