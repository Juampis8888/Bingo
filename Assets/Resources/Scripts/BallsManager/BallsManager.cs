using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BallsManager : MonoBehaviour
{
    private IEnumerator CoroutineTaken;

    private IEnumerator CoroutineFind;

    public RectTransform Content;

    public TextMeshProUGUI BallText;

    public TextMeshProUGUI BallTextShow;

    public FlayersManager FlayersManager;

    public BallAdapter BallAdapter;

    public List<int> ListNum { get; set; } = new List<int>();

    public List<int> TakenNum { get; set; } = new List<int>();

    private int CountHeight = 0;

    private int CountWidth = 0;

    private int Position = 0;

    private void Awake()
    {
        for(int i=0; i < FlayersManager.CountBalls; i++)
        {
            ListNum.Insert(i, i + 1);
        }    
    }

    void Start()
    {
        CoroutineTaken = TakenBalls();
        StartCoroutine(CoroutineTaken);
    }

    public IEnumerator TakenBalls()
    {   
        if(ListNum.Count > 0)
        { 
            int Count = 0;
            int Index = 0;
            while(Index < FlayersManager.CountBalls)
            {
                var IndexRandom = Random.Range(0, ListNum.Count);
                if (BallTextShow.gameObject.transform.parent.parent.gameObject.activeSelf)
                {
                    BallTextShow.text = ListNum[IndexRandom].ToString();
                }
                else
                {
                    BallText.text = ListNum[IndexRandom].ToString();
                }
                Count++;
                yield return new WaitForSeconds(0.01f);

                if(Count > FlayersManager.CountBalls)
                {
                    IndexRandom = Random.Range(0, ListNum.Count);
                    var Value = ListNum[IndexRandom];
                    if(!TakenNum.Exists(value => value == Value))
                    {
                        if (BallTextShow.gameObject.transform.parent.parent.gameObject.activeSelf)
                        {
                            BallTextShow.text = ListNum[IndexRandom].ToString();
                        }
                        else
                        {
                            BallText.text = ListNum[IndexRandom].ToString();
                        }
                        TakenNum.Insert(Index, ListNum[IndexRandom]);
                        AddBallContent(ListNum[IndexRandom]);
                        CoroutineFind = FlayersManager.FindBall(Value);
                        StartCoroutine(CoroutineFind);
                        ListNum.Remove(ListNum[IndexRandom]);
                        Index++;
                        Count = 0;
                        yield return new WaitForSeconds(5f);
                    }else
                    {
                        Count = 0;
                    }
                }
            }
        }
        yield return null;
    }

    private void AddBallContent(int Value)
    {
        BallAdapter.gameObject.SetActive(true);
        var BallRectTransform = BallAdapter.GetComponent<RectTransform>();
        float templateWidth = BallRectTransform.rect.width;
        float templateHeight = BallRectTransform.rect.height;
        int CountMax = Mathf.FloorToInt(Content.rect.width/templateWidth);
        var item = Instantiate(BallAdapter);
        float TopX = 0;
        float TopY = 0;
        item.name = "Ball" + Position;
        item.BallText.text = Value.ToString();
        item.Parent(Content);
        if (CountWidth >= CountMax)
        {
            CountHeight++;
            CountWidth = 0;
        }
        TopY = (CountHeight * templateHeight) * -1;
        TopX = (CountWidth * templateWidth);
        item.Location(TopX, TopY);
        item.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        Position++;
        CountWidth++;
        BallAdapter.gameObject.SetActive(false);
        /*var width = Count * templateWidth;
        Content.localPosition = new Vector3(0, Content.localPosition.y, Content.localPosition.z);
        Content.sizeDelta = new Vector2(width, Content.rect.height);
        FlayersAdapter.gameObject.SetActive(false);*/
    }

    public void Stop()
    {
        StopCoroutine(CoroutineTaken);
        StopCoroutine(CoroutineFind);
    }

    public void StartTaken()
    {
        StartCoroutine(CoroutineTaken);
    }

}
