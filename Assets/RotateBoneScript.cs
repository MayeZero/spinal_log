using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateBoneScript : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 0.05f;
    Quaternion initialRotation;
    [SerializeField] MeshRenderer meshRenderer;

    // Start is called before the first frame update
    void Start()
    {
        initialRotation = transform.localRotation;
        Debug.Log("rotation" + Quaternion.identity);
    }

    private void Awake()
    {
        initialRotation = transform.localRotation;
    }

    [ContextMenu("Move right bone")]
    public void moveRightBone()
    {
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }

    public void moveRight(float rotationSpeed)
    {
        transform.Rotate(Vector3.up , rotationSpeed * Time.deltaTime);
    }

    public void moveLeft(float rotationSpeed)
    {
        transform.Rotate(Vector3.down * rotationSpeed * Time.deltaTime);
    }



    public void rotateDegree(float degree)
    {
       transform.localRotation = Quaternion.Euler(0, degree * 500f, 0f);
    }



    [ContextMenu("reset rotation")]
    public void resetRotation()
    {
        transform.localRotation = initialRotation;
    }

    public void SetActive(bool active)
    {
        this.meshRenderer.enabled = active;
    }

}


