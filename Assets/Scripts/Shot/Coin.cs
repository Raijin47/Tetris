using UnityEngine;

public class Coin : MonoBehaviour
{
    public int count = 1;

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ShooterGame.Instance.coinEarned++;
            Money.Instance.Add(count);
            Destroy(gameObject);
        }
    }
}
