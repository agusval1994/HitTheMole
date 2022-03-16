using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Text")]
    public Text text_score;
    public Text text_money;
    public Text text_time;
    public Text text_startTime;

    [Header("PopUp")]
    public GameObject startPopUp;
    public GameObject finishPopUp;
    public GameObject timesUpPopUp;

    [Header("FinishText")]
    public Text finishMessage;
    public Text finalMoney;
    public Text text_to_add;
    public Text finalHitMole;
    public Image newScore;

    [Header("Bools")]
    public bool playing = false;
    public bool start = false;
    bool last10seconds = false;
    bool firt3seconds = false;

    [HideInInspector]
    public float gameTime = 61f;

    LevelManager levelManager;
    GameManager gameManager;
    FlashBombEffect flashBombEffect;
    AudioManager audioManager;

    float starTime = 4f;

    void Start()
    {
        levelManager = GetComponent<LevelManager>();
        gameManager = FindObjectOfType<GameManager>();
        flashBombEffect = FindObjectOfType<FlashBombEffect>();
        audioManager = FindObjectOfType<AudioManager>();
        UpdateUI();
    }

    void Update()
    {
        if (start)
        {
            FindObjectOfType<HammerManager>().enabled = false;
            starTime -= Time.deltaTime;
            int intStartTime = (int)starTime;
            text_startTime.text = intStartTime.ToString();

            if (!firt3seconds)
            {
                StartCoroutine(First3Seconds());
                firt3seconds = true;
            }

            if (starTime <= 1)
            {
                start = false;
                startPopUp.SetActive(false);
                levelManager.StartGame();
                playing = true;
                FindObjectOfType<HammerManager>().enabled = true;
            }
        }

        if (playing)
        {
            gameTime -= Time.deltaTime;
            int intTimeLeft = (int)gameTime;
            text_time.text = intTimeLeft.ToString();

            if (gameTime <= 11)
            {
                if (!last10seconds)
                {
                    last10seconds = true;
                    StartCoroutine(Last10Seconds());
                }
                text_time.color = Color.red;
            }            

            if (gameTime <= 0)
            {
                playing = false;
                FinishGame();
            }
        }
    }

    public void UpdateUI()
    {
        text_score.text = levelManager.score.ToString();
        text_money.text = levelManager.coinsEarned.ToString();
    }

    public void FlashEffect()
    {
        flashBombEffect.FlashEffect();
    }

    public void FinishGame()
    {
        levelManager.FinishGame();
        StartCoroutine(FinishRoutine());
    }

    public void Message()
    {
        int actualCoins = gameManager.money;
        int coinsEarned = int.Parse(text_money.text);

        int record = gameManager.score;
        int score = int.Parse(text_score.text);

        finalMoney.text = (actualCoins + coinsEarned).ToString();
        text_to_add.text = "+" + coinsEarned.ToString();
        finalHitMole.text = score.ToString();

        gameManager.money += coinsEarned;

        if (score > record)
        {
            gameManager.score = score;
            newScore.gameObject.SetActive(true);
            finishMessage.text = "Amaizing!!!";
        }
        else
        {
            finishMessage.text = "Good Work";
        }

        gameManager.SaveData();
    }

    IEnumerator FinishRoutine()
    {

        Animator timesUpAnim = timesUpPopUp.GetComponent<Animator>();
        Animator resultAnim = finishPopUp.GetComponent<Animator>();

        timesUpPopUp.SetActive(true);
        timesUpAnim.SetInteger("Time", 1);

        audioManager.Play("ClockEndGame", "Fx");

        yield return new WaitForSeconds(1f);
        
        timesUpAnim.SetInteger("Time", 2);

        finishPopUp.SetActive(true);
        resultAnim.SetBool("Result", true);

        Message();
    }

    IEnumerator First3Seconds()
    {
        audioManager.Play("ClockTicTac", "Fx");

        yield return new WaitForSeconds(1f);

        if (starTime > 1)
        {
            StartCoroutine(First3Seconds());
        }
    }

    IEnumerator Last10Seconds()
    {
        audioManager.Play("ClockTicTac", "Fx");

        yield return new WaitForSeconds(1f);

        if(gameTime > 0)
        {
            StartCoroutine(Last10Seconds());
        }
    }
}
