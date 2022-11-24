using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Theme Database", menuName = "Customize/Theme/Database")]
public class ThemeDatabase : ScriptableObject
{
    [SerializeField]
    private List<Theme> _ListThemes;

    public Theme FindTheme(string name)
    {
        return _ListThemes.Find(Theme => Theme.themeName == name);
    }
}
