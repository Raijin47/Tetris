using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    public static Score Instance { get; private set; }

    public TextMeshProUGUI t_Score;
    public TextMeshProUGUI t_MummyDeath;
    public TextMeshProUGUI t_Time;

    public int score;
    public int mummyDeath;
    public float time;


    void Start()
    {
        Instance = this;
        Load();
    }

    private void Load()
    {
        score = PlayerPrefs.GetInt(nameof(score), 0);
        mummyDeath = PlayerPrefs.GetInt(nameof(mummyDeath), 0);
        time = PlayerPrefs.GetFloat(nameof(time), 0);

        UpdateText();
    }

    private void UpdateText()
    {
        t_Score.text = score.ToString();
        t_MummyDeath.text = mummyDeath.ToString();
        t_Time.text = GetTimeText(time);
    }

    public static string GetTimeText(float time)
    {
        int sec = (int)(time % 60);
        int min = (int)(time / 60);
        return $"{min:D2}:{sec:D2}";
    }

    public void SetTime(float count)
    {
        if (count > time)
        {
            time = count;

            PlayerPrefs.SetFloat(nameof(time), time);
            UpdateText();
        }
    }

    public void SetMummyDeath(int count)
    {
        if (count > mummyDeath)
        {
            mummyDeath = count;

            PlayerPrefs.SetInt(nameof(mummyDeath), mummyDeath);
            UpdateText();
        }
    }

    public void SetScore(int count)
    {
        if (count > score)
        {
            score = count;

            PlayerPrefs.SetInt(nameof(score), score);
            UpdateText();
        }
    }
}
