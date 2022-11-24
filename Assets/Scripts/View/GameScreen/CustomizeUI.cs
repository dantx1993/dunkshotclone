using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomizeUI : UIController
{
    [SerializeField]
    private CustomizeButton _CustomizeButton;
    [SerializeField]
    private GameObject _BallCustomize;
    [SerializeField]
    private GameObject _ThemeCustomize;
    public void OnBackButtonClicked()
    {
        SoundController.Instance.Play("ButtonClick");
        GameManager.Instance.gameState = GameState.GAME_MENU;
    }
    public void OnButtonCustomizeBallClick()
    {
        SoundController.Instance.Play("ButtonClick");
        _CustomizeButton.OnButtonCustomizeBallClicked();
        _BallCustomize.SetActive(true);
        _ThemeCustomize.SetActive(false);
    }
    public void OnButtonCustomizeThemeClicked()
    {
        SoundController.Instance.Play("ButtonClick");
        _CustomizeButton.OnButtonCustomizeThemeClicked();
        _BallCustomize.SetActive(false);
        _ThemeCustomize.SetActive(true);
    }
}
