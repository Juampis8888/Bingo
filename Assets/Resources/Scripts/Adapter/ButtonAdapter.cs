using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonAdapter : MonoBehaviour
{
    public TextMeshProUGUI Num;

    public int index;

    public bool isElected = false;

    public Color ColorElected;

    public Image ImageButton;

    public void Parent(Transform Parent)
    {
        transform.SetParent(Parent);
    }

    public void Location(float topX, float topY)
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        rectTransform.localPosition = new Vector3(topX, topY, 0);
    }

    public void ElectedButton()
    { 
        ImageButton.color = ColorElected;
        isElected = true;
    }


    public void NotElectedButton()
    {
        ImageButton.color = Color.white;
        isElected = false;
    }
}
