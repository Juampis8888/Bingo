using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AwardsAdapter : MonoBehaviour
{
    public List<Image> ImageAwards = new List<Image>();

    public TextMeshProUGUI ValueAwards;

    public TextMeshProUGUI AwardText;

    public void Parent(Transform Parent)
    {
        transform.SetParent(Parent);
    }

    public void Location(float top)
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        rectTransform.localPosition = new Vector3(0,top, 0);
    }

    public void ChangeColorAwards(int index, int value)
    {
        if(value == 1)
        {
            ImageAwards[index].color = Color.red;
        }
        else if(value == 0)
        {
            ImageAwards[index].color = Color.white;
        }
    }

}
