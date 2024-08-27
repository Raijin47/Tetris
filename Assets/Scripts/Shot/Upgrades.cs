using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;

public enum UpgradeType
{
    speedAttack,
    damage,
    hp,
}

public class Upgrades : MonoBehaviour
{
    public Player player;

    public UpgradeSetting speedAttack;
    public UpgradeSetting damage;
    public UpgradeSetting hp;

    public UnityEvent OnChange;

    public void Reset()
    {
        damage.level = 0;
        speedAttack.level = 0;
        hp.level = 0;

        SetUpgrades();
    }

    public void Add(UpgradeType type)
    {
        switch (type)
        {
            case UpgradeType.speedAttack:
                AddSpeedAttack();
                break;
            case UpgradeType.damage:
                AddDamage();
                break;
            case UpgradeType.hp:
                AddHp();
                break;
        }

        Audio.Play(ClipType.upgrade);
        SetUpgrades();
    }

    private void AddSpeedAttack()
    {
        speedAttack.level++;
    }

    private void AddDamage()
    {
        damage.level++;
    }

    private void AddHp()
    {
        hp.level++;
    }

    private void SetUpgrades()
    {
        player.hp.SetMaxHp((int)hp.GetUpgrade());
        player.HpVisual();
        player.speedAttack = speedAttack.GetUpgrade();
        player.damage = (int)damage.GetUpgrade();

        OnChange?.Invoke();
    }
}

[System.Serializable]
public class UpgradeSetting
{
    public float start = 1;
    public float add = 1;
    public bool mult = false;

    [Space]
    public int level = 0;

    [Space]
    public float currentUpgrade;

    public float GetUpgrade()
    {
        if (mult)
            return GetUpgradeMult();
        else
            return GetUpgradeAdd();
    }

    private float GetUpgradeAdd()
    {
        currentUpgrade = start + level * add;
        return currentUpgrade;
    }

    public float GetUpgradeMult()
    {
        currentUpgrade = start + math.pow(add, level);
        return currentUpgrade;
    }
}
