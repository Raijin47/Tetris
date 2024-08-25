using UnityEngine;

public class ShooterGame : MonoBehaviour
{
    public static ShooterGame Instance { get; private set; }

    public Spawner spawner;
    public Player player;

    public static bool isGame;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        StartGame();
        Player.Instance.hp.OnDeath.AddListener(GameOver);
    }

    public void StartGame()
    {
        spawner.StartSpawn();
        player.hp.Restore();
        player.StartAttack();
        isGame = true;
    }

    public void GameOver()
    {
        isGame = false;
        print("Game Over");
        spawner.Stop();
        player.Stop();
    }
}
