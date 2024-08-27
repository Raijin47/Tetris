using System;
using TMPro;
using UnityEngine;

public class Skills : MonoBehaviour
{
    [SerializeField] private SkillData[] _skills;


    private void OnEnable()
    {
        GlobalEvent.AddBonus += Add;
    }

    private void OnDisable()
    {
        GlobalEvent.AddBonus -= Add;
    }

    private void Add(int id, int percent)
    {
        if(id == 5)
        {
            for(int i = 0; i < _skills.Length; i++)
                _skills[i].Percent += percent;
            return;
        }

        _skills[id].Percent += percent;
    }
}

[Serializable]
public class SkillData
{
    [SerializeField] private TextMeshProUGUI _textPercent;
    [SerializeField] private TextMeshProUGUI _textCount;

    private int _count;
    private int _percent;
    public int Percent
    {
        get => _percent;
        set
        {
            _percent = value;
            if(_percent > 100)
            {
                Percent = _percent - 100;
                Count++;
            }
            _textPercent.text = $"{_percent}%";
        }
    }

    public int Count
    {
        get => _count;
        set
        {
            _count = value;
            _textCount.text = _count.ToString();
        }
    }
}