using System;
using TMPro;
using UnityEngine;

public class ShooterGame : MonoBehaviour
{
    public static ShooterGame Instance { get; private set; }

    public UI ui;
    public Spawner spawner;
    public Player player;

    public GameObject gameShoter;

    public static bool isGame;

    [Space, Header("Other")]

    public TextMeshProUGUI[] t_MummyDeath;
    public TextMeshProUGUI[] t_Score;
    public TextMeshProUGUI[] t_Time;
    public TextMeshProUGUI[] t_CoinEarned;

    public int mummyDeath;
    public int score;
    public float time;
    public int coinEarned;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {

    }

    private void Update()
    {
        if (isGame)
        {
            time += Time.deltaTime;

            UpdateText();
        }
    }

    private void UpdateText()
    {
        foreach (var item in t_MummyDeath)
        {
            item.text = mummyDeath.ToString();
        }

        foreach (var item in t_Score)
        {
            item.text = score.ToString();
        }

        string textTime = Score.GetTimeText(time);

        foreach (var item in t_Time)
        {
            item.text = textTime;
        }

        foreach (var item in t_CoinEarned)
        {
            item.text = coinEarned.ToString();
        }
    }

    private void OnEnable()
    {
        GlobalEvent.StartGame += StartGame;
        player.hp.OnDeath.AddListener(GameOver);
        GlobalEvent.MummyDeath += MummyDeath;
    }

    private void OnDisable()
    {
        GlobalEvent.StartGame -= StartGame;
        player.hp.OnDeath.RemoveListener(GameOver);
        GlobalEvent.MummyDeath -= MummyDeath;
    }

    private void MummyDeath()
    {
        score += 7;
        mummyDeath += 1;
    }

    public void StartGame()
    {
        Money.Instance.Reset();
        ui.SetPage(1);
        GameActivate(true);
        spawner.ClearSpawnedObjects();
        spawner.StartSpawn();

        Bonuses.Instance.Reset();

        player.hp.Restore();
        player.StartAttack();

        score = 0;
        time = 0;
        mummyDeath = 0;
        coinEarned = 0;

        UpdateText();

        isGame = true;
    }

    public void GameActivate(bool activ)
    {
        gameShoter.SetActive(activ);
    }

    public void GameOver()
    {
        isGame = false;
        Time.timeScale = 0;
        GlobalEvent.GameOver?.Invoke();
        spawner.Stop();
        player.Stop();
        ui.SetOveridePage(4);

        Score.Instance.SetMummyDeath(mummyDeath);
        Score.Instance.SetScore(score);
        Score.Instance.SetTime(time);

        Audio.Play(ClipType.gameOver);
    }
}
