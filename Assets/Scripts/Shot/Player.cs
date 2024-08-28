using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    public static Player Instance { get; internal set; }

    [Header("Visual")]
    public Animator animator;
    public SpriteRenderer sr;
    public VisualPercent hpVisual;
    public ParticleSystem[] shootParticleSystems;

    [Space, Header("Direction")]
    public Vector2 dir = Vector2.left;
    public float distanceAttack = 15;

    [Space, Header("Bullet")]
    public int maxBullet = 10;
    public int countBullet;

    public int damage = 1;

    public float delayAttack = 0.2f;
    public float timeRecharge = 4;
    public float speedAttack = 0.5f;

    [Space, Header("Other Setting")]
    public Upgrades upgrades;
    public Hp hp;
    public LayerMask layerEnemy;
    public bool attack;
    public GameObject rechargeBtn;

    [Space, Header("Bonus")]
    public float timeRechargeBonus = 0.5f;
    public bool pierced = false;

    [Space, Header("Events")]
    public UnityEvent<int> OnChangeBullet;
    public UnityEvent<float> OnRecharge;

    private Coroutine rechargeCoroutine;


    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {

    }

    public void StartAttack()
    {
        foreach (var item in shootParticleSystems)
        {
            item.Stop();
        }

        animator.Play("Idle", 0, 0);
        upgrades.Reset();
        Left();
        StopAllCoroutines();
        RestoreBullet();
        attack = true;
        rechargeBtn.SetActive(false);
        StartCoroutine(AttackCoroutine());
    }

    public void Left()
    {
        dir = Vector2.left;
        sr.flipX = false;
    }

    public void Right()
    {
        dir = Vector2.right;
        sr.flipX = true;
    }

    private IEnumerator AttackCoroutine()
    {
        var minDelay = new WaitForSeconds(0.1f);
        var delayShot = new WaitForSeconds(delayAttack);

        while (attack)
        {
            if (HaveBullet())
            {
                yield return new WaitForSeconds(1 / speedAttack);

                animator.SetTrigger("Shot");

                yield return delayShot;

                Shot();

                if (HaveBullet())
                {
                   
                }
                else
                {
                    rechargeBtn.SetActive(true);
                }
            }
            else
            {
                yield return minDelay;
                //yield return RechargeCoroutine();
            }
        }
    }

    private void Shot()
    {
        Audio.Play(ClipType.shot);
        print("shot");
        countBullet--;
        OnChangeBullet?.Invoke(countBullet);
        int id = dir.x == -1 ? 0: 1;

        shootParticleSystems[id].Stop();
        shootParticleSystems[id].Play();

        //RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, distanceAttack, layerEnemy);
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, dir, distanceAttack, layerEnemy);

        for (int i = 0; i < hits.Length; i++)
        {
            RaycastHit2D hit = hits[i];

            if (i == 0 || (i == 1 && pierced))
            {
                if (hit.collider != null)
                {
                    print(hit.collider.gameObject);

                    if (hit.collider.TryGetComponent(out Hp hpComponent))
                    {
                        hpComponent.TakeDamage(damage);

                        Debug.Log($"Попал в объект: {hit.collider.gameObject.name}, нанесено урона: {damage}");
                    }
                }
            }
        }
    }

    public bool HaveBullet()
    {
        return countBullet > 0;
    }

    public void RestoreBullet()
    {
        countBullet = maxBullet;
        OnChangeBullet?.Invoke(countBullet);
    }

    public void Recharge()
    {
        rechargeBtn.SetActive(false);
        rechargeCoroutine = StartCoroutine(RechargeCoroutine(timeRecharge));
    }

    public void RechargeBonus()
    {
        rechargeBtn.SetActive(false);

        if (rechargeCoroutine != null)
            StopCoroutine(rechargeCoroutine);

        StartCoroutine(RechargeCoroutine(timeRechargeBonus));
    }

    private IEnumerator RechargeCoroutine(float time)
    {
        animator.SetTrigger("Reload");
        print("recharge");
        float timer = time;

        while (timer > 0)
        {
            timer = timer - Time.deltaTime;
            float percent = math.clamp(1 - (timer / time), 0, 1);
            OnRecharge?.Invoke(percent);
            yield return null;
        }

        Audio.Play(ClipType.recharge);
        RestoreBullet();
    }

    private void OnValidate()
    {
        if (sr == null)
            sr = GetComponentInChildren<SpriteRenderer>();

        if (hp == null)
            hp = GetComponent<Hp>();
    }

    internal void Stop()
    {
        attack = false;
        StopAllCoroutines();
    }

    internal void HpVisual()
    {
        hpVisual.SetCount(hp.maxHp);
    }
}
