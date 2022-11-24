using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField]
    private Trajectory _Trajectory;
    public Trajectory Trajectory => _Trajectory;
    private BallController _Ball;
    public BallController ball
    {
        get
        {
            return _Ball;
        }
        set
        {
            if (_Ball != value)
            {
                _Ball = value;
                EventHub.Instance.UpdateEvent(EventName.CHANGE_BALL, _Ball.gameObject);
            }
        }
    }

    private int _NumberOfDestroyBall = 1;

    [SerializeField]
    private GameObject _CurrentBasket;
    public GameObject currentBasket
    {
        get
        {
            return currentBasket;
        }
        set
        {
            if (_CurrentBasket != value)
            {
                if (_CurrentBasket != null)
                {
                    if (_NumberOfDestroyBall > 0)
                    {
                        Destroy(_CurrentBasket);
                        _NumberOfDestroyBall--;
                    }
                    else
                    {
                        Pool.Instance.Destroy(PoolName.BASKET, _CurrentBasket);
                    }
                    EventHub.Instance.UpdateEvent(EventName.SPAWN_BASKET, value.transform);
                }
                _CurrentBasket = value;
                EventHub.Instance.UpdateEvent(EventName.CHANGE_BASKET, _CurrentBasket);
            }
        }
    }
    private GameState _GameState = GameState.GAME_MENU;
    public GameState gameState
    {
        get
        {
            return _GameState;
        }
        set
        {
            _GameState = value;
            Debug.Log($"====> {_GameState}");
            EventHub.Instance.UpdateEvent(EventName.CHANGE_GAME_STATE, _GameState);
        }
    }

    private int _Score = 0;
    public int score
    {
        get
        {
            return _Score;
        }
        set
        {
            _Score += value;
            EventHub.Instance.UpdateEvent(EventName.SCORE, _Score);
        }
    }
    private int _HighScore;

    [SerializeField]
    private Camera _MainCam;

    private Vector3 _FirstBasketPosition;
    private Vector2 _BasketSize;

    public Vector3 firstBasketPositionInScreen
    {
        get
        {
            return _MainCam.WorldToScreenPoint(_FirstBasketPosition);
        }
    }

    public Vector2 basketSize
    {
        get
        {
            return _BasketSize;
        }
    }

    private BallState _BallState = BallState.BALL_IN_BASKET;
    public BallState ballState
    {
        get
        {
            return _BallState;
        }
        set
        {
            _BallState = value;
            EventHub.Instance.UpdateEvent(EventName.CHANGE_BALL_STATE, _BallState);
        }
    }

    [SerializeField]
    private Ball _BallTheme;
    public Ball ballTheme
    {
        get
        {
            return _BallTheme;
        }
        set
        {
            _BallTheme = value;
            StorageUserInfo.Instance.ballName = _BallTheme.ballName;
            EventHub.Instance.UpdateEvent(EventName.CHANGE_BALL_THEME, _BallTheme.ballName);
        }
    }
    [SerializeField]
    private BallDatabase _BallDatabase;
    public BallDatabase ballDatabase => _BallDatabase;

    [SerializeField]
    private Theme _Theme;
    public Theme theme
    {
        get
        {
            return _Theme;
        }
        set
        {
            _Theme = value;
            StorageUserInfo.Instance.themeName = _Theme.themeName;
            EventHub.Instance.UpdateEvent(EventName.CHANGE_THEME, _Theme.themeName);
        }
    }
    [SerializeField]
    private ThemeDatabase _ThemeDatabase;
    public ThemeDatabase themeDatabase => _ThemeDatabase;

    [SerializeField]
    private Popup _PopupPrefabs;

    protected override void Awake()
    {
        base.Awake();
        //StorageUserInfo.Instance.Reset();
        StorageUserInfo.Instance.Load();
        _BallTheme = _BallDatabase.FindBall(StorageUserInfo.Instance.ballName);
        _Theme = _ThemeDatabase.FindTheme(StorageUserInfo.Instance.themeName);
        _FirstBasketPosition = _CurrentBasket.transform.position;
        Vector3 BasketMax = _MainCam.WorldToScreenPoint(_CurrentBasket.GetComponent<BasketController>().renderForRandom.bounds.max);
        Vector3 basketMin = _MainCam.WorldToScreenPoint(_CurrentBasket.GetComponent<BasketController>().renderForRandom.bounds.min);
        _BasketSize = new Vector2(BasketMax.x - basketMin.x, BasketMax.y - basketMin.y);
    }

    public void GameOver()
    {
        if (_Score > StorageUserInfo.Instance.highScore)
        {
            StorageUserInfo.Instance.highScore = _Score;
        }
    }
}
