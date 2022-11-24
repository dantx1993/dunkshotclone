using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Theme", menuName = "Customize/Theme/New Theme")]
public class Theme : ScriptableObject
{
    [SerializeField]
    private string _ThemeName;
    public string themeName => _ThemeName;
    [SerializeField]
    private Color _BackgroundColor;
    public Color backgroundColor => _BackgroundColor;
    [SerializeField]
    private Sprite _Background;
    public Sprite background => _Background;
    [SerializeField]
    private Sprite _ActiveHoop1;
    public Sprite activeHoop1 => _ActiveHoop1;
    [SerializeField]
    private Sprite _ActiveHoop2;
    public Sprite activeHoop2 => _ActiveHoop2;
    [SerializeField]
    private Sprite _UnactiveHoop1;
    public Sprite unactiveHoop1 => _UnactiveHoop1;
    [SerializeField]
    private Sprite _UnactiveHoop2;
    public Sprite unactiveHoop2 => _UnactiveHoop2;
}
