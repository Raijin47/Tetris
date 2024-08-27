using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Hp hp;
    public Move move;
    public Collider2D collider;
    public Rigidbody2D rb;

    [Space, Header("Settings Mummy")]
    public float timeAttack = 2;
    public int damage = 1;
    public int[] healths = { 1, 2, 3 };
    public float[] times = {0, 60, 180};

    [Space]
    public float delayDestroy = 1.5f;

    private float timer;

    private void Start()
    {
        hp.SetMaxHp(GetHp());
        hp.Restore();
    }

    private int GetHp()
    {
        int id = 0;
        float time = ShooterGame.Instance.time;

        for (int i = times.Length-1; i > 0; i--)
        {
            if (time >= times[i])
            {
                id = i;
                break;
            }
        }

        return healths[id];
    }

    public void Death()
    {
        Spawner.Instance.SpawnCoin(transform.position);
        GlobalEvent.MummyDeath?.Invoke();

        hp.enabled = false;
        move.enabled = false;
        collider.enabled = false;
        rb.simulated = false;

        Destroy(gameObject, delayDestroy);

        Audio.Play(ClipType.mummyDeath);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (ShooterGame.isGame)
        {
            if (collision.rigidbody != null)
            {
                if (collision.rigidbody.gameObject == Player.Instance.gameObject)
                {
                    timer -= Time.deltaTime;

                    if (timer < 0)
                    {
                        timer = timeAttack;
                        Attack();
                    }
                }
            }
        }
    }

    private void Attack()
    {
        if(Bonuses.Instance.Knife())
        {
            print("Knife");
            Death();
        }
        else
        {
            print("enemy Attack");
            Player.Instance.hp.TakeDamage(damage);
        }
    }

    private void OnValidate()
    {
        rb ??= GetComponent<Rigidbody2D>();
    }
}
