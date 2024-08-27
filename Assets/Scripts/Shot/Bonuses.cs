using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class Bonuses : MonoBehaviour
{
    public static Bonuses Instance { get; internal set; }

    public Skills skills;

    [Space, Header("Heal")]
    public int healAmount = 3;

    [Space, Header("Pierced")]
    public Image imagePercentPierced;
    public float timePierced = 10;
    public float timerPierced;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (ShooterGame.isGame)
        {
            PiercedBonus();
        }
    }

    internal void Reset()
    {
        timerPierced = 0;
        skills.Reset();
        PiercedBonus();
    }

    private void PiercedBonus()
    {
        timerPierced -= Time.deltaTime;

        Player.Instance.pierced = timerPierced > 0;

        imagePercentPierced.transform.parent.gameObject.SetActive(timerPierced > 0);
        imagePercentPierced.fillAmount = math.clamp(timerPierced / timePierced, 0, 1);
    }

    public void Granade()
    {

    }

    public void Pierced()
    {
        if (Spend(0))
        {
            timerPierced = timePierced;
        }
    }

    public void Recharge()
    {
        if (Spend(1))
        {
            Player.Instance.RechargeBonus();
        }
    }

    public void Hp()
    {
        if (Spend(3))
        {
            Player.Instance.hp.Heal(healAmount);
        }
    }

    public bool Knife()
    {
        return Spend(4);
    }

    public bool Spend(int idBonus)
    {
        if (skills._skills[idBonus].Count >= 1)
        {
            skills._skills[idBonus].Count--;

            Audio.Play(ClipType.bonus);

            return true;
        }

        return false;
    }
}
