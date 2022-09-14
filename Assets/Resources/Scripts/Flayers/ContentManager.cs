using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ContentManager : MonoBehaviour
{
    public GameObject ButtonExit;

    public ShowFlayerManager ShowFlayerManager;

    public RectTransform Content;

    public RectTransform ContentButton;

    public ButtonAdapter ButtonAdapter;

    public TextMeshProUGUI TextFlayer;

    public int MarggenWidth;

    public int MarggenHeight;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ExitFlayer()
    {
        AddButtons(Content.GetChild(0).GetComponent<FlayersAdapter>().Index);
        Destroy(Content.GetChild(0).gameObject);
    }

    public void AddButtons(int index)
    {   
        if(ContentButton.childCount > 1)
        {
            var count = ContentButton.childCount;
            for (int i = 1; i < count; i++)
            {
                DestroyImmediate(ContentButton.GetChild(1).gameObject);
            }
            ButtonAdapter.gameObject.SetActive(true);
        }
        
        var Index = ShowFlayerManager.FlayersManager.BuyCountFlayer;
        int Pos = 0;
        int Count = 0;
        var ButtonAdapterRectTransform = ButtonAdapter.GetComponent<RectTransform>();
        float templateWidth = ButtonAdapterRectTransform.rect.width;
        float templateHeight = ButtonAdapterRectTransform.rect.height;
        int CountMax = Mathf.FloorToInt(Content.rect.width / (templateWidth +MarggenWidth ));
        int CountHeight = 0;
        int CountWidth = 0;
        Debug.Log(CountMax);
        while (Count < Index)
        {
            if (Count != index)
            {
                ButtonAdapter.gameObject.SetActive(true);
                float TopX = MarggenWidth;
                float TopY = MarggenHeight;
                var item = Instantiate(ButtonAdapter);
                item.name = "Button Flayer" + (Count + 1);
                item.Num.text = (Count + 1).ToString();
                item.index = Count;
                item.Parent(ContentButton);
                if (CountWidth == CountMax)
                {
                    CountHeight++;
                    CountWidth = 0;
                }
                TopX = ((CountWidth * templateWidth) + (MarggenWidth * (CountWidth + 1)));
                TopY = ((CountHeight * templateHeight) + (MarggenHeight * (CountHeight + 1))) * -1;
                Debug.Log(TopX + " " + TopY);
                item.Location(TopX, TopY);
                item.GetComponent<Button>().onClick.AddListener(() =>
                {
                    ShowFlayer(item.index);
                    ContentButton.gameObject.SetActive(false);
                });
                item.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                Pos++;
                CountWidth++;
            }
            Count++;
        }
        ButtonAdapter.gameObject.SetActive(false);
        ContentButton.gameObject.SetActive(true);
    }

    public void ShowFlayer(int index)
    {
        int Count = 0;
        ShowFlayerManager.FlayersManager.FlayersAdapters.ForEach(value =>
        {
            if (value.Index == index)
            {
                FlayersAdapter Item = Instantiate(value);
                Item.name = value.name;
                ShowFlayerManager.FlayersAdaptersShow.Insert(Count, Item);
                ShowFlayerManager.FlayersAdaptersShow[Count].Parent(Content);
                ShowFlayerManager. FlayersAdaptersShow[Count].transform.SetAsFirstSibling();
                TextFlayer.text = "Tarjeton N°" + (index + 1);
                ShowFlayerManager.FlayersAdaptersShow[Count].GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
                ShowFlayerManager.FlayersAdaptersShow[Count].GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                Count++;
            }
        });
    }


}
