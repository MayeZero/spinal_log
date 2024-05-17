using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vertebra
{
    public String label;

    float displacement;

    float leftDisplacement;
    float rightDisplacement;
    float middleDisplacement;

    float load;
    float leftLoad;
    float rightLoad;
    float middleLoad;

    Vertebra(String label)
    {
        this.label = label;
    }

    void setDisplacement(float value)
    {
        this.displacement = value;
    }

    void setLeftDisplacement(float value)
    {
        this.leftDisplacement = value;
    }

    void setRightDisplacement(float value)
    {
        this.rightDisplacement = value;
    }

    void setMiddleDisplacement(float value)
    {
        this.middleDisplacement = value;
    }

    void setLoad(float value)
    {
        this.load = value;
    }

    void setLeftLoad(float value)
    {
        this.leftLoad = value;
    }

    void setRightLoad(float value)
    {
        this.rightLoad = value;
    }

    void setMiddleLoad(float value)
    {
        this.middleLoad = value;
    }

    float getDisplacement()
    {
        return this.displacement;
    }

    float getLeftDisplacement()
    {
        return this.leftDisplacement;
    }

    float getRightDisplacement()
    {
        return this.rightDisplacement;
    }

    float getMiddleDisplacement()
    {
        return this.middleDisplacement;
    }
    float getLoad()
    {
        return this.load;
    }

    String getLabel()
    {
        return this.label;
    }
}
