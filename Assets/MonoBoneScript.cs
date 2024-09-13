using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class MonoBoneScript : MonoBehaviour
{
    // Start is called before the first frame update

    Vector3 initialPosition;
    float initalZ;
    int limitDepth;

    void Start()
    {
        initialPosition = gameObject.transform.localPosition;
        initalZ = gameObject.transform.localPosition.z;

        Debug.Log("Initial: " + initialPosition);
    }

    private void Awake()
    {
        initialPosition = gameObject.transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [ContextMenu("Move bone down")]
    public void moveDown()
    {
        transform.position = transform.position + Vector3.down;
    }

    [ContextMenu("Move bone up")]
    public void moveUp()
    {
        transform.position = transform.position + Vector3.up;
    }


    [ContextMenu("Reset Position")]
    public void resetPosition()
    {
        Debug.Log(transform.localPosition + " reseting to " + initialPosition);
        transform.localPosition = initialPosition;
        Debug.Log("After " + transform.localPosition);
    }

    public void moveDown(float value)
    {
        transform.position = transform.position + Vector3.down * value;
    }


    private float translateValue()
    {
        return 0;
    }




}
