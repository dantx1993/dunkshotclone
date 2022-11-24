using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameplayUI : UIController
{
    [SerializeField]
    private TextMeshProUGUI _ScoreText;
    [SerializeField]
    private Button _PauseButton;

    protected override void Start()
    {
        base.Start();
        EventHub.Instance.RegisterEvent(EventName.SCORE, ScoreUp);
        EventHub.Instance.RegisterEvent(EventName.CHANGE_BALL_STATE, BallFlying);
    }

    private void ScoreUp(object data)
    {
        _ScoreText.text = data.ToString();
    }

    private BallState _BallState;
    private void BallFlying(object data)
    {
        _BallState = (BallState)data;
        if (_BallState == BallState.BALL_FLYING)
        {
            _PauseButton.gameObject.SetActive(false);
        }
        else
        {
            _PauseButton.gameObject.SetActive(true);
        }
    }

    public void OnPauseClicked()
    {
        SoundController.Instance.Play("ButtonClick");
        GameManager.Instance.gameState = GameState.GAME_PAUSE;
    }
}
