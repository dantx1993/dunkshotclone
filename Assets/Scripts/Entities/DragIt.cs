using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragIt : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField]
    private float _Power = 1f; // Power of force    
    [SerializeField]
    private float _MaxPower; // Max power when drag
    [SerializeField]
    private Camera _Cam; // Main Cam to get world point

    private Vector2 _Force; // Force when drag
    private Vector2 _StartPoint; // Start point when drag
    private Vector2 _EndPoint; // End point when drag

    /// <summary>
    /// OnBeginDrag overide on IBeginDragHandler Interface
    /// </summary>
    /// <param name="eventData"></param>
    public void OnBeginDrag(PointerEventData eventData)
    {
        if ((int)GameManager.Instance.gameState >= 2 && GameManager.Instance.ballState == BallState.BALL_IN_BASKET)
        {
            if (GameManager.Instance.gameState == GameState.GAME_MENU)
            {
                GameManager.Instance.gameState = GameState.GAME_PLAY;
            }
#if UNITY_EDITOR
            _StartPoint = _Cam.ScreenToWorldPoint(Input.mousePosition);
#else
        _StartPoint = _Cam.ScreenToWorldPoint(Input.GetTouch(0).position);
#endif
            EventHub.Instance.UpdateEvent(EventName.ON_BEGIN_DRAG_EVENT, true);
        }
    }


    /// <summary>
    /// OnDrag overide on IDragHandler Interface
    /// </summary>
    /// <param name="eventData"></param>
    public void OnDrag(PointerEventData eventData)
    {
        if ((int)GameManager.Instance.gameState >= 2 && GameManager.Instance.ballState == BallState.BALL_IN_BASKET)
        {
#if UNITY_EDITOR
            _EndPoint = _Cam.ScreenToWorldPoint(Input.mousePosition);
#else
        _EndPoint = _Cam.ScreenToWorldPoint(Input.GetTouch(0).position);
#endif
            _Force = ClampVector2(_StartPoint, _EndPoint);
            EventHub.Instance.UpdateEvent(EventName.ON_DRAG_EVENT, _Force * _Power);
        }
    }

    /// <summary>
    /// OnEndDrag overide on IEndDragHandler Interface
    /// </summary>
    /// <param name="eventData"></param>
    public void OnEndDrag(PointerEventData eventData)
    {
        if ((int)GameManager.Instance.gameState >= 2 && GameManager.Instance.ballState == BallState.BALL_IN_BASKET)
        {
#if UNITY_EDITOR
            _EndPoint = _Cam.ScreenToWorldPoint(Input.mousePosition);
#else
        _EndPoint = _Cam.ScreenToWorldPoint(Input.GetTouch(0).position);
#endif
            //Debug.Log("Start: " + _StartPoint + "/End: " + _EndPoint);
            _Force = ClampVector2(_StartPoint, _EndPoint);
            EventHub.Instance.UpdateEvent(EventName.ON_END_DRAG_EVENT, _Force * _Power);
        }
    }

    /// <summary>
    /// Get Clamp of 2 Vectors between max and min
    /// </summary>
    /// <param First Vector="start"></param>
    /// <param Second Vector="end"></param>
    /// <returns></returns>
    private Vector2 ClampVector2(Vector2 start, Vector2 end)
    {
        float distance = Vector2.Distance(start, end);
        distance = Mathf.Clamp(distance, 0, _MaxPower);
        return (start - end).normalized * distance;
    }
}
