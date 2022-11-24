using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "Ball Database", menuName = "Customize/Ball/Database")]
public class BallDatabase : ScriptableObject
{
    [SerializeField]
    private List<Ball> _ListBall;

    public Ball FindBall(string name)
    {
        Ball result = _ListBall.Find(Ball => Ball.ballName == name);
        return result;
    }
}
