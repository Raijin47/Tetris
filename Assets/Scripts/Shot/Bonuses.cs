using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class Bonuses : MonoBehaviour
{
    public static Bonuses Instance { get; internal set; }

    public Skills skills;

    [Space, Header("Granade")]
    public Image imageUseGranade;
    public bool useGranade;
    public float radius = 3;
    public GameObject particleGranade;

    [Space, Header("Heal")]
    public int healAmount = 3;
    public ParticleSystem particleHeal;

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

            GranadeBonus();
        }
    }

    internal void Reset()
    {
        useGranade= false;
        timerPierced = 0;
        skills.Reset();
        PiercedBonus();
        particleHeal.Stop();
    }

    private void PiercedBonus()
    {
        timerPierced -= Time.deltaTime;

        Player.Instance.pierced = timerPierced > 0;

        imagePercentPierced.transform.parent.gameObject.SetActive(timerPierced > 0);
        imagePercentPierced.fillAmount = math.clamp(timerPierced / timePierced, 0, 1);
    }

    private void GranadeBonus()
    {
        if (useGranade)
        {
            if (Input.GetMouseButtonDown(0))
            {
                UseGranade();
            }
        }
    }

    private void UseGranade()
    {
        Audio.Play(ClipType.granade);

        useGranade = false;
        Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Instantiate(particleGranade, pos, Quaternion.identity, transform);

        Collider2D[] colliders = Physics2D.OverlapCircleAll(pos, radius);

        foreach (Collider2D collider in colliders)
        {
            if (collider.TryGetComponent(out Enemy enemy))
            {
                enemy.Death();
            }
        }

        imageUseGranade.transform.parent.gameObject.SetActive(false);
    }

    public void Granade()
    {
        if (!useGranade && Spend(2))
        {
            useGranade = true;
            imageUseGranade.transform.parent.gameObject.SetActive(true);
        }
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
            particleHeal.Stop();
            particleHeal.Play();
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
