using UnityEngine;

public class Move : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed = 2;
    //public float distanceStop = 1.5f;

    public Vector3 dir;
    //public bool isMove = true;

    private void Start()
    {
        dir = (Player.Instance.transform.position - transform.position).normalized;
    }

    void Update()
    { 
        //if (isMove && Vector2.Distance(transform.position, target) > distanceStop)
        //{
        //    transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        //}

        if (ShooterGame.isGame)
        {
            rb.velocity = speed * dir;
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }

    private void OnValidate()
    {
        if(rb == null)
            rb = GetComponent<Rigidbody2D>();   
    }
}
