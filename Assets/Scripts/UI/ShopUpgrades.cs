using UnityEngine;

public class ShopUpgrades : MonoBehaviour
{
    public Upgrades upgrades;
    public UpgradeItem[] items;

    public int priceSpeedAttack = 10;
    public int pricePower = 15;
    public int priceHp = 5;

    private void Start()
    {
        Visual();
    }

    public void Visual()
    {
        for (int i = 0; i < items.Length; i++)
        {
            items[i].SetPrice(GetPrice(items[i].upgradeType));
            items[i].SetLevel(GetLevel(items[i].upgradeType));
        }
    }

    private int GetLevel(UpgradeType upgradeType)
    {
        int level = 0;

        switch (upgradeType)
        {
            case UpgradeType.speedAttack:
                level = upgrades.speedAttack.level;
                break;

            case UpgradeType.damage:
                level = upgrades.damage.level;
                break;

            case UpgradeType.hp:
                level = upgrades.hp.level;
                break;

            default:
                break;
        }

        return level;
    }

    private int GetPrice(UpgradeType upgradeType)
    {
        switch (upgradeType)
        {
            case UpgradeType.speedAttack:
                return priceSpeedAttack;
            case UpgradeType.damage:
                return pricePower;
            case UpgradeType.hp:
                return priceHp;
            default:
                return 0;
        }
    }

    internal void Buy(UpgradeType upgradeType)
    {
        int price = 0;

        switch (upgradeType)
        {
            case UpgradeType.speedAttack:
                price = priceSpeedAttack;
                break;

            case UpgradeType.damage:
                price = pricePower;
                break;

            case UpgradeType.hp:
                price = priceHp;
                break;

        }

        if (Money.Instance.Spend(price))
        {
            upgrades.Add(upgradeType);
        }

        Visual();
    }
}
