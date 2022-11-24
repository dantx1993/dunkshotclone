using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    private Ball _Data;
    [SerializeField]
    private SpriteRenderer _Render;
    [SerializeField]
    private Rigidbody2D _Rigidbody2D;
    [SerializeField]
    private LayerMask _StopLayer;
    [SerializeField]
    private LayerMask _BouchingLayer;
    [SerializeField]
    private LayerMask _UnperfectLayer;
    [SerializeField]
    private LayerMask _ScoreLayer;
    [SerializeField]
    private LayerMask _EventLayer;
    [SerializeField]
    private GameObject _WallEffect;
    [SerializeField]
    private ParticleSystem _FlameEffect;
    [SerializeField]
    private ParticleSystem _SmokeEffect;
    private Vector2 _Force;

    private ScoreMessage _CurrentScoreMessage;
    private ScoreMessage _ScoreMessage;

    private float _BallHeight;

    [SerializeField]
    private GameObject _PopupPrefab;

    private void Start()
    {
        _BallHeight = Vector3.Distance(new Vector3(0, GetComponent<SpriteRenderer>().bounds.min.y, 0), new Vector3(0, GetComponent<SpriteRenderer>().bounds.center.y, 0));
        EventHub.Instance.RegisterEvent(EventName.ON_END_DRAG_EVENT, OnEndDrag);
        EventHub.Instance.RegisterEvent(EventName.ON_DRAG_EVENT, OnDrag);
        EventHub.Instance.RegisterEvent(EventName.ON_BEGIN_DRAG_EVENT, OnBeginDrag);
        EventHub.Instance.RegisterEvent(EventName.CHANGE_BALL_THEME, ListBallThemeChange);
        GameManager.Instance.ball = this;
        _FlameEffect.Stop();
        _SmokeEffect.Stop();
    }

    #region Drag
    /// <summary>
    /// Callback function on End Drag
    /// </summary>
    /// <param name="data"></param>
    private void OnEndDrag(object data)
    {
        _Force = (Vector2)data;
        ActiveRigigbody();
        _Rigidbody2D.AddForce(_Force, ForceMode2D.Impulse);
        GameManager.Instance.Trajectory.Hide();
        transform.SetParent(null);
        GameManager.Instance.ballState = BallState.BALL_FLYING;
        if (_CurrentScoreMessage == null || !_CurrentScoreMessage.isPerfect)
        {
            SoundController.Instance.Play("ReleaseBall");
        }
        else
        {
            SoundController.Instance.Play("FireBall");
        }
    }

    /// <summary>
    /// Callback function on Dragging
    /// </summary>
    /// <param name="data"></param>
    private void OnDrag(object data)
    {
        _Force = (Vector2)data;
        GameManager.Instance.Trajectory.UpdateDots(transform.position, _Force, _Rigidbody2D.sharedMaterial.bounciness);
    }

    /// <summary>
    /// Callback function on Begin Drag
    /// </summary>
    /// <param name="data"></param>
    private void OnBeginDrag(object data)
    {
        GameManager.Instance.Trajectory.Show();
        transform.SetParent(null);
        _ScoreMessage = new ScoreMessage(_CurrentScoreMessage == null ? 1 : !_CurrentScoreMessage.isPerfect ? 1 : (_CurrentScoreMessage.perfectTime + 1));
    }
    #endregion
    #region Collider
    private void OnTriggerEnter2D(Collider2D other)
    {

        if (0 != (_ScoreLayer.value & 1 << other.gameObject.layer) && transform.parent != other.transform)
        {
            if (!other.GetComponentInParent<BasketController>().isScore)
            {
                other.GetComponentInParent<BasketController>().isScore = true;
                GameManager.Instance.score = _ScoreMessage.getScore;
                _CurrentScoreMessage = _ScoreMessage;
                if (_CurrentScoreMessage.isPerfect)
                {
                    SoundController.Instance.Play("ScorePerfect" + (_CurrentScoreMessage.perfectTime < 10 ? _CurrentScoreMessage.perfectTime : 10));
                    _FlameEffect.Play();
                    _SmokeEffect.Play();
                    ParticleSystem.EmissionModule smokeEmission = _SmokeEffect.emission;
                    ParticleSystem.EmissionModule flameEmission = _FlameEffect.emission;
                    flameEmission.rateOverTime = 5 + (_CurrentScoreMessage.perfectTime < 4 ? _CurrentScoreMessage.perfectTime : 4 - 1) * 10;
                    smokeEmission.rateOverTime = 5 + (_CurrentScoreMessage.perfectTime < 4 ? _CurrentScoreMessage.perfectTime : 4 - 1) * 10;
                }
                else
                {
                    SoundController.Instance.Play("ScoreSimple");
                    _FlameEffect.Stop();
                    _SmokeEffect.Stop();
                }
                // if (_CurrentScoreMessage != null)
                // {
                //     Debug.Log("===>this");
                //     if (_CurrentScoreMessage.isPerfect)
                //     {
                //         Debug.Log("===>perfect");
                //         Popup perfectPopup = Pool.Instance.Instantiate(PoolName.POPUP, _PopupPrefab, new Vector3(other.transform.position.x, other.transform.position.y + 1, other.transform.position.z), Quaternion.identity).GetComponent<Popup>();
                //         perfectPopup.ShowPopup(_CurrentScoreMessage.perfectTime, true);
                //     }
                //     if (_CurrentScoreMessage.isBounching)
                //     {
                //         Debug.Log("===>bounce");
                //         Popup bouncePopup = Pool.Instance.Instantiate(PoolName.POPUP, _PopupPrefab, new Vector3(other.transform.position.x, other.transform.position.y + 1, other.transform.position.z), Quaternion.identity).GetComponent<Popup>();
                //         bouncePopup.ShowPopup(_CurrentScoreMessage.perfectTime, false);
                //     }
                // }
            }
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (0 != (_EventLayer.value & 1 << other.gameObject.layer) && transform.parent != other.transform.parent.transform)
        {
            EventHub.Instance.RemoveEvent(EventName.ON_DRAG_EVENT, other.GetComponentInParent<BasketController>().ChangeAngle);
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (0 != (_BouchingLayer.value & 1 << other.gameObject.layer) && transform.parent != other.transform.parent.transform)
        {
            _ScoreMessage.isBounching = true;
            SoundController.Instance.Play("BallBounce");
            Pool.Instance.Instantiate(PoolName.WALL_EFFECT, _WallEffect, other.GetContact(0).point, Quaternion.Euler(other.GetContact(0).normal.x == 1 ? new Vector3(0, 0, 0) : new Vector3(0, 180, 0)));
        }
        if (0 != (_UnperfectLayer.value & 1 << other.gameObject.layer) && transform.parent != other.transform.parent.transform)
        {
            _ScoreMessage.isPerfect = false;
            SoundController.Instance.Play("BallBounce");
        }
        if (0 != (_StopLayer.value & 1 << other.gameObject.layer) && transform.parent != other.transform.parent.transform)
        {
            if (other.gameObject.GetComponentInParent<BasketController>().isScore)
            {
                EventHub.Instance.RegisterEvent(EventName.ON_DRAG_EVENT, other.gameObject.GetComponentInParent<BasketController>().ChangeAngle);
                GameManager.Instance.currentBasket = other.gameObject.GetComponentInParent<BasketController>().gameObject;
                //GameManager.Instance.currentBasket.GetComponent<BasketController>().UnactiveBasket();
                GameManager.Instance.ballState = BallState.BALL_IN_BASKET;
                transform.position = new Vector3(other.transform.position.x, other.transform.position.y + _BallHeight / 2, transform.position.z);
                transform.SetParent(other.transform.parent.transform);
                DeactiveRigidbody();

            }
            SoundController.Instance.Play("CollisionNet");
        }
    }
    #endregion
    #region Logic
    public void ActiveRigigbody()
    {
        _Rigidbody2D.isKinematic = false;
    }
    public void DeactiveRigidbody()
    {
        _Rigidbody2D.velocity = Vector3.zero;
        _Rigidbody2D.angularVelocity = 0f;
        _Rigidbody2D.isKinematic = true;
    }
    private void ListBallThemeChange(object data)
    {
        string name = data.ToString();
        Ball result = GameManager.Instance.ballDatabase.FindBall(name);
        _Data = result;
        _Render.sprite = _Data.render;
        ParticleSystem.MinMaxGradient grad = new ParticleSystem.MinMaxGradient(_Data.flameColors[0], _Data.flameColors[1]);
        grad.mode = ParticleSystemGradientMode.TwoColors;
        ParticleSystem.MainModule mainModule = _FlameEffect.main;
        mainModule.startColor = grad;
    }
    #endregion
}
