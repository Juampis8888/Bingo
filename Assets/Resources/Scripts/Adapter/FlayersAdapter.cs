using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FlayersAdapter : MonoBehaviour
{
    public List<TextMeshProUGUI> FlayerText = new List<TextMeshProUGUI>();

    public List<int> FlayersTaken = new List<int>(); 

    public List<int> ListNum { get; set; } = new List<int>();

    public List<int> NumTaken = new List<int>();

    public int Index;

    private FlayersManager FlayersManager;

    private void Awake()
    {
        FlayersManager = GameObject.Find("Flayers").GetComponent<FlayersManager>();
    }

    public void AddList()
    {
        for (int i = 0; i < FlayersManager.CountBalls; i++)
        {
            ListNum.Insert(i, i + 1);
        }

        for (int i = 0; i < FlayerText.Count; i++)
        {
            if (i == 12)
            {
                FlayersTaken.Insert(i, 1);
            }
            else
            {
                FlayersTaken.Insert(i, 0);
            }
        }
    }

    public void FlayerNew()
    {
        int Count = 0;
        while (Count < FlayerText.Count)
        {
            if (Count == 12)
            {
                FlayerText[Count].text = "BINGO";
                NumTaken.Insert(Count, -1);
                Count++;
            }
            else
            {
                var RandomIndex = Random.Range(1, ListNum.Count);
                var Value = ListNum[RandomIndex];
                if (!NumTaken.Exists(value => value == Value))
                {

                    FlayerText[Count].text = Value.ToString();
                    NumTaken.Insert(Count, Value);
                    Count++;
                }
            }
        }
    }

    public void Parent(Transform Parent)
    {
        transform.SetParent(Parent);
    }

    public void Location(float top)
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        rectTransform.localPosition = new Vector3(top, 0, 0);
    }

    public void ChangeValue(int index)
    {
        FlayersTaken[index] = 1;
    }
}
