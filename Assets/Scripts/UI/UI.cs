using UnityEngine;

public class UI : MonoBehaviour
{
    public GameObject[] pages;
    public int id;

    void Start()
    {
        SetPage(0);
    }

    public void GameOver(bool win)
    {
        int id = win ? 1 : 0;
        SetOveridePage(7);
    }

    public void SetPage(int v)
    {
        id = v;

        if (v == 0)
        {
            ShooterGame.Instance.GameActivate(false);
            Time.timeScale = 1;
        }

        for (int i = 0; i < pages.Length; i++)
        {
            pages[i].SetActive(i == v);
        }

        Audio.Play(0);
    }

    public void SetOveridePage(int i)
    {
        pages[i].SetActive(true);
    }

    private void OnValidate()
    {
        SetPage(id);
    }
}
