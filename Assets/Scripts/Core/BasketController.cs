using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasketController : MonoBehaviour
{
    [SerializeField]
    private bool _IsScore = false;
    [SerializeField]
    private bool _IsFirstBasket = false;
    [SerializeField]
    private Transform _StopPoint;
    public Transform stopPoint => _StopPoint;
    public bool isScore
    {
        get
        {
            return _IsScore;
        }
        set
        {
            _IsScore = value;
        }
    }

    [SerializeField]
    private SpriteRenderer _RenderForRandom;
    public SpriteRenderer renderForRandom => _RenderForRandom;

    private Vector2 _Force;

    [SerializeField]
    private GameObject _Effect;
    public GameObject effect => _Effect;

    private Theme _Data;

    [SerializeField]
    private SpriteRenderer _Hoop1;
    [SerializeField]
    private SpriteRenderer _Hoop2;
    [SerializeField]
    private Color _ClassicColorActive;
    [SerializeField]
    private Color _ClassicColorUnactive;
    private void Start()
    {
        EventHub.Instance.RegisterEvent(EventName.CHANGE_THEME, ListenThemeChange);
    }
    private void OnEnable()
    {
        if (!_IsFirstBasket)
            _IsScore = false;
        if (_Data != null)
        {
            _Hoop1.sprite = _Data.activeHoop1;
            _Hoop2.sprite = _Data.activeHoop2;
            if (_Data.themeName == "Classic")
            {
                _Hoop1.color = _ClassicColorActive;
                _Hoop2.color = _ClassicColorActive;
            }
        }
        StartCoroutine(IeWaitGetData());
    }

    private IEnumerator IeWaitGetData()
    {
        yield return new WaitWhile(() => GameManager.Instance == null);

        _Data = GameManager.Instance.theme;
        if (_Data != null)
        {
            _Hoop1.sprite = _Data.activeHoop1;
            _Hoop2.sprite = _Data.activeHoop2;
            if (_Data.themeName == "Classic")
            {
                _Hoop1.color = _ClassicColorActive;
                _Hoop2.color = _ClassicColorActive;
            }
        }
    }

    public void UnactiveBasket()
    {
        if (_Data.themeName != "Classic")
        {
            _Hoop1.sprite = _Data.unactiveHoop1;
            _Hoop2.sprite = _Data.unactiveHoop2;
        }
        else
        {
            _Hoop1.color = _ClassicColorUnactive;
            _Hoop2.color = _ClassicColorUnactive;
        }

    }
    public void ChangeAngle(object data)
    {
        _Force = (Vector2)data;
        transform.rotation = Quaternion.FromToRotation(new Vector3(0, 1, 0), new Vector3(_Force.x, _Force.y, 0));
    }

    private void ListenThemeChange(object data)
    {
        string name = data.ToString();
        Theme result = GameManager.Instance.themeDatabase.FindTheme(name);
        _Data = result;
        _Hoop1.sprite = _Data.activeHoop1;
        _Hoop2.sprite = _Data.activeHoop2;
        // if (!_IsScore)
        // {
        //     _Hoop1.sprite = _Data.activeHoop1;
        //     _Hoop2.sprite = _Data.activeHoop2;
        // }
        // else
        // {
        //     _Hoop1.sprite = _Data.unactiveHoop1;
        //     _Hoop2.sprite = _Data.unactiveHoop2;
        // }

    }
}
