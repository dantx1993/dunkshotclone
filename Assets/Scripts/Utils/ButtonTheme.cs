using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonTheme : MonoBehaviour
{
    [SerializeField]
    private GameObject _ChooseLayer;
    [SerializeField]
    private Theme _Data;

    private void Start()
    {
        EventHub.Instance.RegisterEvent(EventName.CHANGE_THEME, ListThemeChange);
    }

    public void OnButtonThemeClicked()
    {
        _ChooseLayer.SetActive(true);
        GameManager.Instance.theme = _Data;
    }

    private void ListThemeChange(object data)
    {
        string name = data.ToString();
        if (_Data.themeName != name)
        {
            _ChooseLayer.SetActive(false);
        }
    }
}
