using UnityEngine;
using UnityEngine.Events;

public class Hp : MonoBehaviour
{
    public int maxHp = 10;
    public int hp;

    public UnityEvent<int> OnChange;
    public UnityEvent OnDeath;

    private void Start()
    {
        Restore();
    }

    public void Restore()
    {
        hp = maxHp;
        OnChange.Invoke(hp);
    }

    public void TakeDamage(int damage)
    {
        hp -= damage;
        if (hp < 0) hp = 0;
        OnChange.Invoke(hp);
        if (!IsAlive()) Die();
    }

    public void Heal(int amount)
    {
        hp += amount;
        if (hp > maxHp) hp = maxHp;
        OnChange.Invoke(hp);
    }

    public void Die()
    {
        OnDeath.Invoke();
    }

    public bool IsAlive()
    {
        return hp > 0;
    }
}
