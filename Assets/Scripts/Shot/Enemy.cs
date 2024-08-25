using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float timeAttack = 2;
    public int damage = 1;
    private float timer;

    public void Death()
    {
        Destroy(gameObject);
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
        print("enemy Attack");
        Player.Instance.hp.TakeDamage(damage);
    }
}
