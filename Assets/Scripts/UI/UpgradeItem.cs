using TMPro;
using UnityEngine;

public class UpgradeItem : MonoBehaviour
{
    public ShopUpgrades shopUpgrades;

    public UpgradeType upgradeType;

    public TextMeshProUGUI t_price;
    public TextMeshProUGUI t_level;

    public void Buy()
    {
        shopUpgrades.Buy(upgradeType);
    }

    public void SetPrice(int count)
    {
        t_price.text = count.ToString();
    }

    public void SetLevel(int count)
    {
        t_level.text = count.ToString();
    }

    private void OnValidate()
    {
        shopUpgrades ??= FindFirstObjectByType<ShopUpgrades>();
    }
}
