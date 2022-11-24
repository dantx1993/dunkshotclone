using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Ball", menuName = "Customize/Ball/New Ball")]
[System.Serializable]
public class Ball : ScriptableObject
{
    [SerializeField]
    private string _BallName;
    public string ballName => _BallName;
    [SerializeField]
    private Sprite _BallRender;
    public Sprite render => _BallRender;
    [SerializeField]
    private Color[] _FlameColors;
    public Color[] flameColors => _FlameColors;
    [SerializeField]
    private bool _IsActive;
    public bool isActive => _IsActive;
}
