using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverUI : UIController
{
    [SerializeField]
    private TextMeshProUGUI _HighScoreText;
    [SerializeField]
    private TextMeshProUGUI _ScoreText;
    protected override void Start()
    {
        base.Start();
        EventHub.Instance.RegisterEvent(EventName.HIGH_SCORE, ListenHighScore);
        EventHub.Instance.RegisterEvent(EventName.SCORE, ListenScore);
        _HighScoreText.text = $"BEST SCORE:\n{StorageUserInfo.Instance.highScore}";
    }
    private void ListenHighScore(object data)
    {
        _HighScoreText.text = $"BEST SCORE:\n{data.ToString()}";
    }
    private void ListenScore(object data)
    {
        _ScoreText.text = data.ToString();
    }
    public void OnButtonReplayClicked()
    {
        SoundController.Instance.Play("ButtonClick");
        SceneManager.LoadScene("Main");
    }
}
