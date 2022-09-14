using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AnimationManager : MonoBehaviour
{
    public GameObject Win;

    public GameObject Value;

    public TextMeshProUGUI Award;

    public AnimatorAdapter AnimatorAdapter;

    public Color[] ColorsWin;

    private Animator AnimatorWin;

    private Animator AnimatorValue;

    void Start()
    {
        AnimatorWin = Win.GetComponent<Animator>();
        AnimatorValue = Value.GetComponent<Animator>();
    }

    public void Active()
    {
        gameObject.SetActive(true);
        //AnimatorWin.Play("Win 1");
    }

    public void Awards(int Awards)
    {
        Debug.Log(Awards);
        Award.text = Awards.ToString("c");
    }

    public IEnumerator ChangeColor(FlayersAdapter FlayerWin, Awards.Award AwardsAdapter)
    {
        int Count = 0;
        int j;
        while(Count < FlayerWin.NumTaken.Count)
        {
            for(int i = 0; i < FlayerWin.FlayersTaken.Count; i++)
            {
                if(AwardsAdapter.Win[i] == 0)
                {
                    Count++;
                    continue;
                    
                }
                else if(AwardsAdapter.Win[i] == 1)
                {
                    for(j = 0; j < ColorsWin.Length-1; j++)
                    {
                        yield return new WaitForSeconds(0.1f);
                        FlayerWin.FlayerText[i].color = Color.Lerp(ColorsWin[j], ColorsWin[j + 1], 0.1f);
                    }
                    Count++;
                }
            }

            if(Count == FlayerWin.NumTaken.Count)
            {
                Active();
                Awards(AwardsAdapter.ValueWin);
                break;
            }
        }
    }

    void Update()
    {
        if (AnimatorAdapter.isFinish)
        {
            Value.SetActive(true);
        }
    }
}
