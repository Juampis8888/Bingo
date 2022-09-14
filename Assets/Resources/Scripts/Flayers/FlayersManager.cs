using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FlayersManager : MonoBehaviour
{
    public int ValueFlayers;

    public int CountFlayersRest;

    public int BuyCountFlayer;

    public int CountBalls;

    public TextMeshProUGUI TMPActive;

    public TextMeshProUGUI ValueFlayersText;

    public TextMeshProUGUI CountFlayersText;

    public TextMeshProUGUI NameFlayers;

    public RectTransform Content;

    public FlayersAdapter FlayersAdapter;

    public int Index { get; set; } = 0;

    public List<FlayersAdapter> FlayersAdapters = new List<FlayersAdapter>();

    public ShowFlayerManager ShowFlayerManager;

    private GameManager GameManager;

    private void Awake()
    {
        GameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void Start()
    {
        TMPActive.text = BuyCountFlayer.ToString();
        ValueFlayersText.text = ValueFlayers.ToString("c");
        CountFlayersText.text = CountFlayersRest.ToString();
        ShowFlayers();
    }
    
    private void ShowFlayers()
    {
        int Count = 0;
        var Position = 0;
        var FlayersRectTransform = FlayersAdapter.GetComponent<RectTransform>();
        float templateWidth = FlayersRectTransform.rect.width;
        for(int i = 0; i < BuyCountFlayer; i++)
        {
            float top = ((Position * templateWidth));
            var item = Instantiate(FlayersAdapter);
            item.name = "Flayer" + i;
            item.Parent(Content);
            item.Location(top);
            item.AddList();
            item.FlayerNew();
            item.Index = i;

            Debug.Log(FlayersAdapters.Contains(item));
            if (FlayersAdapters.Contains(item))
            {
                
                i--;
                continue;
            }

            FlayersAdapters.Insert(i, item);

            item.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);

            Position++;
            Count++;
        }
        var width = Count * templateWidth;
        Content.localPosition = new Vector3(0, Content.localPosition.y, Content.localPosition.z);
        Content.sizeDelta = new Vector2(width, Content.rect.height);
        FlayersAdapter.gameObject.SetActive(false);
    }

    public IEnumerator FindBall(int value)
    {
        if (ShowFlayerManager.gameObject.activeSelf)
        {
            ShowFlayerManager.FlayersAdaptersShow.ForEach(Flayers =>
            {
                if (Flayers.NumTaken.Exists(Num => Num == value))
                {
                    Index = Flayers.NumTaken.FindIndex(Value => Value == value);
                    Flayers.ChangeValue(Index);
                    GameManager.Win(Flayers.FlayerText[Index]);
                    GameManager.ComparateAwards(Flayers);
                }
            });
        }

        for (int i=0; i< FlayersAdapters.Count; i++)
        {
           if(FlayersAdapters[i].NumTaken.Exists(Value => Value == value))
           {
                Index = FlayersAdapters[i].NumTaken.FindIndex(Value => Value == value);
                FlayersAdapters[i].ChangeValue(Index);
                GameManager.Win(FlayersAdapters[i].FlayerText[Index]);
                GameManager.ComparateAwards(FlayersAdapters[i]);
                MoveContent(i);
                yield return new WaitForSeconds(3.5f);

            }
        }

        yield break;
    }


    public void MoveContent(int index)
    {
        var FlayersRectTransform = FlayersAdapter.GetComponent<RectTransform>();
        float templateWidth = FlayersRectTransform.rect.width;
        var top = (templateWidth * index) * -1;
        var ContentRectTransform = Content.GetComponent<RectTransform>();
        ContentRectTransform.localPosition = new Vector3(top, 0, 0);
        NameFlayers.text = "Tarjeton N°"+ "" + (index + 1);
    }

    public void NewFlayers()
    {
        if (CountFlayersRest > 0)
        {
            FlayersAdapter.gameObject.SetActive(true);
            int Count = Content.childCount - 1;
            var FlayersRectTransform = FlayersAdapter.GetComponent<RectTransform>();
            float templateWidth = FlayersRectTransform.rect.width;
            float top = ((Count * templateWidth));
            var item = Instantiate(FlayersAdapter);
            item.name = "Flayer" + Count;
            item.Parent(Content);
            item.Location(top);
            item.AddList();
            item.FlayerNew();
            item.Index = Count;
            item.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            CountFlayersRest--;
            CountFlayersText.text = CountFlayersRest.ToString();
            BuyCountFlayer++;
            TMPActive.text = BuyCountFlayer.ToString();
            FlayersAdapter.gameObject.SetActive(false);
            GameManager.BalanceManager.UpdateBalance(-ValueFlayers);
            ComparateFlayers(item, Count);
        }
    }

    public void ComparateFlayers(FlayersAdapter item, int Index)
    {
        if (FlayersAdapters.Contains(item))
        {
            GameManager.BalanceManager.UpdateBalance(ValueFlayers);
            NewFlayers();
        }
        else
        {
            Debug.Log("New Flayer");
            FlayersAdapters.Insert(Index, item);
            
        }
    }

    

}
