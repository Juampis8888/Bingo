using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BallAdapter : MonoBehaviour
{
    public TextMeshProUGUI BallText;

    public void Parent(Transform Parent)
    {
        transform.SetParent(Parent);
    }

    public void Location(float topX, float topY)
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        rectTransform.localPosition = new Vector3(topX, topY, 0);
    }
}

