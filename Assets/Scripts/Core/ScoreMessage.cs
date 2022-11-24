using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreMessage
{
    private bool _IsBounching;
    private bool _IsPerfect;
    private int _PerfectTime;
    public bool isBounching
    {
        get
        {
            return _IsBounching;
        }
        set
        {
            _IsBounching = value;
        }
    }
    public bool isPerfect
    {
        get
        {
            return _IsPerfect;
        }
        set
        {
            _IsPerfect = value;
        }
    }
    public int perfectTime
    {
        get
        {
            return _PerfectTime;
        }
        set
        {
            _PerfectTime += value;
        }
    }

    public int getScore
    {
        get
        {
            int score = 1;
            if (_IsPerfect)
            {
                score += _PerfectTime > 10 ? 10 : _PerfectTime;
            }
            if (_IsBounching)
            {
                score *= 2;
            }
            return score;
        }
    }

    public ScoreMessage(int _currentPerfect)
    {
        _PerfectTime = _currentPerfect;
        _IsPerfect = true;
        _IsBounching = false;
    }
}
