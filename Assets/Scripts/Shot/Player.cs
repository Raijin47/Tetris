using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    public static Player Instance { get; internal set; }

    [Header("Visual")]
    public SpriteRenderer sr;

    [Space, Header("Direction")]
    public Vector2 dir = Vector2.left;
    public float distanceAttack = 15;

    [Space, Header("Bullet")]
    public int maxBullet = 10;
    public int countBullet;

    public int damage = 1;

    public float timeRecharge = 4;
    public float timeDelayBullet = 1;

    [Space, Header("Other Setting")]
    public Hp hp;
    public LayerMask layerEnemy;
    public bool attack;

    [Space, Header("Events")]
    public UnityEvent<int> OnChangeBullet;
    public UnityEvent<float> OnRecharge; 



    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {

    }

    public void StartAttack()
    {
        Left();
        StopAllCoroutines();
        RestoreBullet();
        attack = true;
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
        var delay = new WaitForSeconds(timeDelayBullet);

        while (attack)
        {
            if (HaveBullet())
            {
                Shot();

                if (HaveBullet())
                {
                    yield return delay;
                }
            }
            else
            {
                yield return RechargeCoroutine();
            }
        }
    }

    private void Shot()
    {
        print("shot");
        countBullet--;
        OnChangeBullet?.Invoke(countBullet);

        RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, distanceAttack, layerEnemy);

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

    public bool HaveBullet()
    {
        return countBullet > 0;
    }

    public void RestoreBullet()
    {
        countBullet = maxBullet;
        OnChangeBullet?.Invoke(countBullet);
    }

    private IEnumerator RechargeCoroutine()
    {
        print("recharge");
        float timer = timeRecharge;

        while (timer > 0)
        {
            timer = math.clamp(timer - Time.deltaTime, 0, timeRecharge);
            float percent = 1 - (timer / timeDelayBullet);
            OnRecharge?.Invoke(percent);
            yield return null;
        }

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
}
