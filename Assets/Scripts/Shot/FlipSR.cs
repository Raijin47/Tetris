using UnityEngine;

public class FlipSR : MonoBehaviour
{
    public SpriteRenderer sr;

    private void Start()
    {
        Vector3 dir = (Player.Instance.transform.position - transform.position).normalized;

        sr.flipX = dir.x < 0;
    }

    private void OnValidate()
    {
        if(sr == null)
            sr = GetComponentInChildren<SpriteRenderer>();
    }
}
