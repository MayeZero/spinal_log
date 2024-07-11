using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Window_Graph : MonoBehaviour
{
    [SerializeField] private Sprite circleSprite;
    private RectTransform graphContainer;
    private void Awake()
    {
        graphContainer = transform.Find("GraphContainer").GetComponent<RectTransform>();
        //CreateCircle(new Vector2(200, 200));
        List<int> valueList = new List<int>() { 5, 22, 34, 23, 5, 66, 76, 32, 22, 67, 45 };
        ShowGraph(valueList);
    }

    private GameObject CreateCircle(Vector2 anchoredPosition)
    {
        GameObject gameObject = new GameObject("circle", typeof(Image));
        gameObject.transform.SetParent(graphContainer, false);
        gameObject.GetComponent<Image>().sprite = circleSprite;
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = anchoredPosition;
        rectTransform.sizeDelta = new Vector2(11, 11);
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);
        return gameObject;

    }

    private void ShowGraph(List<int> valueList)
    {
        float graphHeight = graphContainer.sizeDelta.y;
        float xSize = 50f;
        float yMaximum = 100f;

        GameObject lastCircleGameObject = null;

        for (int i = 0; i < valueList.Count; i++)
        {
            float xPosition = i * xSize + xSize;
            float yPosition = (valueList[i] / yMaximum) * graphHeight;
            GameObject circleGameobject =  CreateCircle(new Vector2(xPosition, yPosition));
            if (lastCircleGameObject != null)
            {
                CreateDotConnections(lastCircleGameObject.GetComponent<RectTransform>().anchoredPosition, circleGameobject.GetComponent<RectTransform>().anchoredPosition);
            }
            lastCircleGameObject = circleGameobject;
        }
    }

    private void CreateDotConnections(Vector2 dotA, Vector2 dotB)
    {
        GameObject gameObject = new GameObject("dotConnection", typeof(Image));
        gameObject.transform.SetParent(graphContainer, false);
        gameObject.GetComponent<Image>().color = new Color(1, 1, 1, .5f);

        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
       
        Vector2 dir = (dotB - dotA).normalized;
        float distance = Vector2.Distance(dotA, dotB);
        
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);
        rectTransform.sizeDelta = new Vector2(distance, 3f);
        rectTransform.anchoredPosition = dotA + dir * distance * 0.5f;
        /// From Claude
        float angleInDegrees = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (angleInDegrees < 0)
            angleInDegrees += 360;

        rectTransform.localEulerAngles = new Vector3(0, 0, angleInDegrees);

    }

}
