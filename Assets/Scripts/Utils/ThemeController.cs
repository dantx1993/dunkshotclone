using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThemeController : MonoBehaviour
{
    [SerializeField]
    private Camera _MainCam;
    [SerializeField]
    private SpriteRenderer _Background;

    void Start()
    {
        EventHub.Instance.RegisterEvent(EventName.CHANGE_THEME, ListenThemeChange);
    }
    private void OnEnable()
    {
        StartCoroutine(IeWaitGetData());
    }

    private IEnumerator IeWaitGetData()
    {
        yield return new WaitWhile(() => GameManager.Instance == null);
        _MainCam.backgroundColor = GameManager.Instance.theme.backgroundColor;
        _Background.sprite = GameManager.Instance.theme.background;
    }

    private void ListenThemeChange(object data)
    {
        string name = data.ToString();
        Theme result = GameManager.Instance.themeDatabase.FindTheme(name);
        _MainCam.backgroundColor = result.backgroundColor;
        _Background.sprite = result.background;
    }
}
