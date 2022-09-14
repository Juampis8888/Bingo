using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    public RectTransform Content;

    public ButtonAdapter ButtonAdapter;

    public FlayersManager FlayersManager;

    public GameObject ButtonAcept;

    public float MarggenWidth;

    public float MarggenHeight;

    public bool IsElected;

    public List<ButtonAdapter> ButtonAdapters = new List<ButtonAdapter>();

    public List<ButtonAdapter> ButtonAdaptersElected = new List<ButtonAdapter>();

    public int Count = 0;

    private ShowFlayerManager ShowFlayerManager;

    private void Awake()
    {
        ShowFlayerManager = gameObject.GetComponent<ShowFlayerManager>();
    }

    private void Update()
    {
        if (IsElected)
        {
            if(ButtonAdaptersElected.Count > 2)
            {
                ButtonAdaptersElected[0].NotElectedButton();
                ButtonAdaptersElected.RemoveAt(0);
                IsElected = false;
            }
            else if(ButtonAdaptersElected.Count == 2)
            {
                ButtonAcept.SetActive(true);
                ButtonAcept.GetComponent<Button>().onClick.AddListener(() =>
                {
                    ShowFlayerManager.ShowFlayer(ButtonAdaptersElected[0].index, ButtonAdaptersElected[1].index);
                });
                IsElected = false;
            }
        }
    }

    public void Buttons()
    {
        if (Content.childCount > 1)
        {
            var count = Content.childCount;
            for (int i = 1; i < count; i++)
            {
                DestroyImmediate(Content.GetChild(1).gameObject);
            }
            ButtonAdapter.gameObject.SetActive(true);
        }
        var Index = FlayersManager.BuyCountFlayer;
        int Pos = 0;
        int Count = 0;
        var ButtonAdapterRectTransform = ButtonAdapter.GetComponent<RectTransform>();
        float templateWidth = ButtonAdapterRectTransform.rect.width;
        float templateHeight = ButtonAdapterRectTransform.rect.height;
        int CountMax = Mathf.FloorToInt(Content.rect.width/(templateWidth + MarggenWidth));
        int CountHeight = 0;
        int CountWidth = 0;
        Debug.Log(CountMax);
        while (Count < Index)
        {
            float TopX = MarggenWidth;
            float TopY = MarggenHeight;
            var item = Instantiate(ButtonAdapter);
            item.name = "Button Flayer" + (Count + 1);
            item.Num.text = (Count + 1).ToString();
            item.index = Count;
            item.Parent(Content);
            if(CountWidth >= CountMax)
            {
                Debug.Log(CountMax);
                CountHeight++;
                CountWidth = 0;
            }
            ButtonAdapters.Insert(Count, item);
            TopX = ((CountWidth * templateWidth) + (MarggenWidth * (CountWidth + 1))) ;
            TopY = ((CountHeight * templateHeight) + (MarggenHeight * (CountHeight + 1))) * -1;
            Debug.Log(TopX + " " + TopY);
            item.Location(TopX, TopY);
            item.GetComponent<Button>().onClick.AddListener( () => {
                AddElected(item.index);
            });
            item.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            Pos++;
            Count++;
            CountWidth++;
        }
        ButtonAdapter.gameObject.SetActive(false);
    }

    public void AddElected(int index)
    {
        Debug.Log(index + " " + ButtonAdaptersElected.Count);
        ButtonAdaptersElected.Insert(ButtonAdaptersElected.Count, ButtonAdapters[index]);
        ButtonAdapters[index].ElectedButton();
        IsElected = true;
        Count++;
    }
}
