using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField]
    private GameObject _Root;
    [SerializeField]
    private GameState _UIState;

    protected virtual void Start()
    {
        EventHub.Instance.RegisterEvent(EventName.CHANGE_GAME_STATE, ActiveUI);
        _Root.SetActive(GameManager.Instance.gameState == _UIState ? true : false);
    }

    private GameState _State;
    public void ActiveUI(object data)
    {
        _State = (GameState)data;
        if (_State != _UIState)
        {
            _Root.SetActive(false);
        }
        else
        {
            _Root.SetActive(true);
        }
    }
}
