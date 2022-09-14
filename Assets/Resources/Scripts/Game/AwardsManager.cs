using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AwardsManager : MonoBehaviour
{
    public AwardsAdapter AwardsAdapter;

    public RectTransform Content;

    public ConsultManager ConsultManager;

    public Color ColorBingoRaw;

    void Start()
    {
        var Award = ConsultManager.AwardsBingo;
        int Count = 0, index = 0;
        var Position = 0;
        var AwardsRectTransform = AwardsAdapter.GetComponent<RectTransform>();
        float templateHeight = AwardsRectTransform.rect.height;

        Award.ForEach(value =>
        {
            float top = ((Position * templateHeight)) * -1;
            var item = Instantiate(AwardsAdapter);
            item.name = "Flayer" + (Position + 1);
            item.Parent(Content);
            item.Location(top);
            item.AwardText.text = value.TextBingo;
            item.ValueAwards.text = value.ValueWin.ToString("c");
            value.Win.ForEach(valueBingo =>
            {
                item.ChangeColorAwards(index, valueBingo);
                index++;
            });

            item.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            Position++;
            Count++;
            index = 0;

        });
        var height = Count * templateHeight;
        Content.localPosition = new Vector3(Content.localPosition.x, 0, Content.localPosition.z);
        Content.sizeDelta = new Vector2(Content.rect.width, height);
        AwardsAdapter.gameObject.SetActive(false);
    } 

    public void UpdateAwards()
    {
        if (Content.childCount > 1)
        {
            int count = Content.childCount;

            for (int i = 1; i < count; i++)
            {
                Debug.Log(Content.GetChild(1).name);
                DestroyImmediate(Content.GetChild(1).gameObject);
            }
        }
        var Award = ConsultManager.AwardsBingo;
        int Count = 0, index = 0;
        var Position = 0;
        var AwardsRectTransform = AwardsAdapter.GetComponent<RectTransform>();
        float templateHeight = AwardsRectTransform.rect.height;
        AwardsAdapter.gameObject.SetActive(true);
        Award.ForEach(value =>
        {
            float top = ((Position * templateHeight)) * -1;
            var item = Instantiate(AwardsAdapter);
            item.name = "Flayer" + (Position + 1);
            item.Parent(Content);
            item.Location(top);
            item.AwardText.text = value.TextBingo;
            item.ValueAwards.text = value.ValueWin.ToString("c");
            value.Win.ForEach(valueBingo =>
            {
                item.ChangeColorAwards(index, valueBingo);
                index++;
            });

            item.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            Position++;
            Count++;
            index = 0;

        });
        var height = Count * templateHeight;
        Content.localPosition = new Vector3(Content.localPosition.x, 0, Content.localPosition.z);
        Content.sizeDelta = new Vector2(Content.rect.width, height);
        AwardsAdapter.gameObject.SetActive(false);
    }
}
