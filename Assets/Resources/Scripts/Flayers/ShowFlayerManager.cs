using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShowFlayerManager : MonoBehaviour
{
    public RectTransform[] Contents;

    public TextMeshProUGUI[] TextFlayers;

    public FlayersManager FlayersManager;

    public GameObject Menu;

    public GameObject ContentFlayer;

    public List<FlayersAdapter> FlayersAdaptersShow = new List<FlayersAdapter>();

    public void ShowFlayer(int index1, int index2)
    {
        int Count = 0;
        FlayersManager.FlayersAdapters.ForEach(value =>
        {   
            if (value.Index == index1 || value.Index == index2)
            {
                FlayersAdapter Item = Instantiate(value);
                Item.name = value.name;
                FlayersAdaptersShow.Insert(Count, Item);
                FlayersAdaptersShow[Count].Parent(Contents[Count]);
                FlayersAdaptersShow[Count].transform.SetAsFirstSibling();
                if (Count == 0)
                {
                    TextFlayers[Count].text = "Tarjeton N°" + (index1 + 1);
                } else
                {
                    TextFlayers[Count].text = "Tarjeton N°" + (index2 + 1);
                }
                FlayersAdaptersShow[Count].GetComponent<RectTransform>().localPosition= new Vector3(0, 0, 0);
                FlayersAdaptersShow[Count].GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                Count++;
            }
        });

        Menu.SetActive(false);
        ContentFlayer.SetActive(true);
    }

    public void DesActive()
    {
        gameObject.SetActive(false);
        Menu.SetActive(true);
        ContentFlayer.SetActive(false);
    }
    

}
