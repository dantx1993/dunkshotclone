using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private GameObject _Ball;
    [SerializeField]
    private Camera _MainCam;
    private float _Spacing;
    private float _CamBallDiff;
    [SerializeField]
    private GameObject _Basket;

    private float _WorldScreenWidth, _WorldScreenHeight;
    private float _ScreenWidth, _ScreenHeight;

    private float _BallPosition;
    private float _CamPosition;
    private float _CamBoundaryUp;
    private float _CamBoundaryDown;
    private GameObject _CurrentBall;
    private GameObject _CurrentBasket;

    private void Start()
    {
        EventHub.Instance.RegisterEvent(EventName.CHANGE_BALL, SetCurrentBall);
        EventHub.Instance.RegisterEvent(EventName.CHANGE_BASKET, SetCurrentBasket);
        _ScreenHeight = Screen.height;
        _ScreenWidth = Screen.width;
        _WorldScreenHeight = _MainCam.orthographicSize * 2;
        _WorldScreenWidth = _WorldScreenHeight * (_ScreenWidth / _ScreenHeight);
        _Spacing = _WorldScreenHeight * 0.05f;
        _CamBallDiff = _MainCam.transform.position.y - _Basket.transform.position.y;
    }

    private void LateUpdate()
    {
        if (GameManager.Instance.ballState == BallState.BALL_FLYING && (int)GameManager.Instance.gameState >= 2)
        {
            _BallPosition = _Ball.transform.position.y;
            _CamPosition = _MainCam.transform.position.y - _CamBallDiff;
            _CamBoundaryUp = _CamPosition + _Spacing;
            _CamBoundaryDown = _CamPosition - _Spacing;

            if (_BallPosition > _CamBoundaryUp || _BallPosition < _CamBoundaryDown)
            {
                if (_BallPosition > _CamBoundaryUp)
                {
                    _MainCam.transform.position = new Vector3(0, _BallPosition + _CamBallDiff - _Spacing, -10);
                }
                else if (_BallPosition < _CamBoundaryDown && _CamPosition > _Basket.transform.position.y)
                {
                    _MainCam.transform.position = new Vector3(0, _BallPosition + _CamBallDiff + _Spacing, -10);
                }
                else if (_BallPosition < _CamPosition - _CamBallDiff)
                {
                    // TODO: gameover logic
                    Debug.Log("GAMEOVER");
                    Pool.Instance.Destroy(PoolName.BALL, _Ball);
                    SoundController.Instance.Play("GameOver");
                    GameManager.Instance.gameState = GameState.GAME_OVER;
                    GameManager.Instance.GameOver();

                }
            }
        }
    }

    private void SetCurrentBall(object data)
    {
        _CurrentBall = (GameObject)data;
        if (_Ball != _CurrentBall)
        {
            _Ball = _CurrentBall;
        }
    }
    private void SetCurrentBasket(object data)
    {
        _CurrentBasket = (GameObject)data;
        if (_Basket != _CurrentBasket)
        {
            _Basket = _CurrentBasket;
        }
    }
}
