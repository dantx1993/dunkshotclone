using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseUI : UIController
{
    public void OnButtonMainClicked()
    {
        SoundController.Instance.Play("ButtonClick");
        SceneManager.LoadScene("Main");
    }
    public void OnButtonResumeClicked()
    {
        SoundController.Instance.Play("ButtonClick");
        GameManager.Instance.gameState = GameState.GAME_PLAY;
    }
    public void OnButtonCustomizeClicked()
    {

    }
    public void OnButtonLeaderBoardClicked()
    {

    }
    public void OnButtonSettingClicked()
    {

    }
}
