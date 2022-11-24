using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomizeButton : MonoBehaviour
{
    [SerializeField]
    private Color _OnColor;
    [SerializeField]
    private Color _OffColor;

    [SerializeField]
    private Image _CustomizeBall;
    [SerializeField]
    private Image _CustomizeTheme;

    void OnEnable()
    {
        OnButtonCustomizeBallClicked();
    }

    public void OnButtonCustomizeBallClicked()
    {
        _CustomizeBall.color = _OnColor;
        _CustomizeTheme.color = _OffColor;
    }
    public void OnButtonCustomizeThemeClicked()
    {
        _CustomizeTheme.color = _OnColor;
        _CustomizeBall.color = _OffColor;
    }
}
