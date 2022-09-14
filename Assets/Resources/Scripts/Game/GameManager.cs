using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{   
    public bool IsPlay { get; set; } = false;

    public Color ColorWin;

    public TextMeshProUGUI Award;

    public GameObject ShowFlayers;

    public ButtonManager ButtonManager;

    public BalanceManager BalanceManager;

    public AnimationManager AnimationManager;

    public AwardsManager AwardsManager;

    public BallsManager BallsManager;

    public AudioSource AudioSource;

    public AudioSource ThemBingo;

    public AudioManager AudioManager;

    private ConsultManager ConsultManager;


    private void Awake()
    {
        ConsultManager = GameObject.Find("GameManager").GetComponent<ConsultManager>();
    }

    void Start()
    {
        Award.text = ConsultManager.AwardsBingo[0].ValueWin.ToString("c");
    }

    public void Win(TextMeshProUGUI NumWin)
    {
        Animator Animator = NumWin.gameObject.GetComponentInParent<Animator>();
        Animator.Play("Num");
        NumWin.color = ColorWin;
    }

    public void ComparateAwards(FlayersAdapter flayersAdapter)
    {
        var BingoAwards = ConsultManager.AwardsBingo;
        int i;
        bool exit = false;

        for (int j = 0; j < BingoAwards.Count; j++)
        {
            for(i = 0; i < BingoAwards[j].Win.Count; i++)
            {
                if(BingoAwards[j].Win[i] == 0)
                {
                    if (i == BingoAwards[j].Win.Count - 1)
                    {
                        Debug.Log("Gano " + " " + BingoAwards[j].Win);
                        if(!BingoAwards[j].FlayersWins.Exists(wins => wins.NumberFlayers == flayersAdapter.Index))
                        {
                            var Awards = new Awards.FlayersWin
                            {
                                isWin = true,
                                NumberFlayers = flayersAdapter.Index
                            };
                            BingoAwards[j].FlayersWins.Add(Awards);
                            if (!BingoAwards[j].IsBingo & BingoAwards[j].ValueWin > 0)
                            {
                                ThemBingo.volume = 0.1f;
                                if(AudioManager.IsActive)
                                    AudioSource.Play();
                                Debug.Log("Gano " + " " + BingoAwards[j].ValueWin);
                                StartCoroutine(AnimationManager.ChangeColor(flayersAdapter, BingoAwards[j]));
                                if (BingoAwards[j].ValueWin >= 0)
                                {
                                    BingoAwards[j].ValueWin = (BingoAwards[j].ValueWin - 50000);
                                }

                                if (BingoAwards[j].ValueWin < 0)
                                {
                                    BingoAwards[j].ValueWin = 0;
                                }
                                Award.text = ConsultManager.AwardsBingo[0].ValueWin.ToString("c");
                                AwardsManager.UpdateAwards();
                                BallsManager.Stop();
                            }
                            else if (BingoAwards[j].IsBingo & BingoAwards[j].ValueWin > 0)
                            {
                                ThemBingo.volume = 0.1f;
                                if (AudioManager.IsActive)
                                    AudioSource.Play();
                                Debug.Log("Bingo");
                                StartCoroutine(AnimationManager.ChangeColor(flayersAdapter, BingoAwards[j]));
                                if (BingoAwards[j].ValueWin >= 0)
                                {
                                    BingoAwards[j].ValueWin = (BingoAwards[j].ValueWin - 100000);
                                }

                                if (BingoAwards[j].ValueWin < 0)
                                {
                                    BingoAwards[j].ValueWin = 0;
                                }
                                AwardsManager.UpdateAwards();
                                Award.text = ConsultManager.AwardsBingo[0].ValueWin.ToString("c");
                                BallsManager.Stop();
                                exit = true;
                                break;
                            }
                            else
                            {
                                Debug.Log("Ya no puedes obtener este premio");
                            }
                        }
                        else
                        {
                            Debug.Log("Ya ganaste este premio con este tarjeton");
                        }
                    }
                    exit = true;
                    continue;
                }
                else if(BingoAwards[j].Win[i] == 1)
                {
                    if(flayersAdapter.FlayersTaken[i] == 0)
                    {
                        break;
                    }
                    else if(flayersAdapter.FlayersTaken[i] == 1)
                    { 
                        if(i == BingoAwards[j].Win.Count - 1)
                        {
                            if (!BingoAwards[j].FlayersWins.Exists(wins => wins.NumberFlayers == flayersAdapter.Index) )
                            {
                                var Awards = new Awards.FlayersWin
                                {
                                    isWin = true,
                                    NumberFlayers = flayersAdapter.Index
                                };
                                BingoAwards[j].FlayersWins.Add(Awards);
                                if (!BingoAwards[j].IsBingo & BingoAwards[j].ValueWin > 0 )
                                {
                                    ThemBingo.volume = 0.1f;
                                    if (AudioManager.IsActive)
                                        AudioSource.Play();
                                    Debug.Log("Gano " + " " + BingoAwards[j].ValueWin);
                                    BallsManager.Stop();
                                    StartCoroutine(AnimationManager.ChangeColor(flayersAdapter, BingoAwards[j]));
                                    if (BingoAwards[j].ValueWin >= 0)
                                    {
                                        BingoAwards[j].ValueWin = (BingoAwards[j].ValueWin - 50000);
                                    }

                                    if (BingoAwards[j].ValueWin < 0)
                                    {
                                        BingoAwards[j].ValueWin = 0;
                                    }

                                    Award.text = ConsultManager.AwardsBingo[0].ValueWin.ToString("c");
                                    AwardsManager.UpdateAwards();
                                }
                                else if (BingoAwards[j].IsBingo & BingoAwards[j].ValueWin > 0)
                                {
                                    ThemBingo.volume = 0.1f;
                                    if (AudioManager.IsActive)
                                        AudioSource.Play();
                                    Debug.Log("Bingo");
                                    BallsManager.Stop();
                                    Award.text = ConsultManager.AwardsBingo[0].ValueWin.ToString("c");
                                    StartCoroutine(AnimationManager.ChangeColor(flayersAdapter, BingoAwards[j]));
                                    if (BingoAwards[j].ValueWin >= 0)
                                    {
                                        BingoAwards[j].ValueWin = (BingoAwards[j].ValueWin - 100000);
                                    }

                                    if(BingoAwards[j].ValueWin < 0)
                                    {
                                        BingoAwards[j].ValueWin = 0;
                                    }
                                    AwardsManager.UpdateAwards();
                                    
                                    exit = true;
                                    break;
                                }
                                else
                                {
                                    Debug.Log("Ya no puedes obtener este premio");
                                }
                            }
                            else
                            {
                                Debug.Log("Ya ganaste este premio con este tarjeton");
                            }
                        }
                    }

                }
            }
            if (exit)
            {
                break;
            }
        }
    }

    public void ActiveFlayersView()
    {
        ShowFlayers.SetActive(true);
        ButtonManager.Buttons();
    }
}
