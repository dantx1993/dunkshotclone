using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trajectory : MonoBehaviour
{
    [SerializeField] 
    private int _DotsNumber; // Number of Dots
    [SerializeField] 
    private GameObject _DotsParent; // Dot parrent in Hierarchy
    [SerializeField] 
    private GameObject _DotPrefab; // Dot Prefab
    [SerializeField] 
    private float _DotSpacing; // Spacing between 2 Dots
    [SerializeField,Range(0.01f, 0.3f)] 
    private float _DotMinScale; // Min scale of Dots
    [SerializeField,Range(0.3f, 1f)] 
    private float _DotMaxScale; // Max Scale of Dots
    [SerializeField] 
    private LayerMask _ReflectionLayer; // Layer to get bouncing

    private Transform[] _DotsList; // Store Dots

    private Vector2 _TempPosition; // Temp variable to store position of step
    private Vector2 _StartReflectPosition; // Start position where dots get bouncing force
    private float _TimeStamp; // Counting time => position
    private float _ScaleFactor; // Scale step of Dots
    // UpdateDots Func Temp Variable
    private bool _IsReflection;
    private RaycastHit2D _Hit;

    void Start()
    {
        // Calculate Scale Factor:
        _ScaleFactor = _DotMaxScale / _DotsNumber;
        // Hide trajectory in the start
        Hide();
        // Prepare dots
        PrepareDots();
    }

    /// <summary>
    /// Function for prepair Dots in start game
    /// </summary>
    void PrepareDots()
    {
        _DotsList = new Transform[_DotsNumber];

        float scale = _DotMaxScale;

        for (int i = 0; i < _DotsNumber; i++)
        {
            _DotsList[i] = Instantiate(_DotPrefab, _DotsParent.transform).transform;

            _DotsList[i].localScale = Vector3.one * scale;
            if (scale > _DotMinScale)
                scale -= _ScaleFactor;
        }
    }
    
    /// <summary>
    /// Update Dots Function call when drag
    /// </summary>
    /// <param Current position of ball="ballPos"></param>
    /// <param Force="forceApplied"></param>
    /// <param Bouncing value="bounciness"></param>
    public void UpdateDots(Vector3 ballPos, Vector2 forceApplied,float bounciness)
    {
        _TimeStamp = _DotSpacing;
        _IsReflection = false;
        for (int i = 0; i < _DotsNumber; i++)
        {
            if (!_IsReflection)
            {
                _TempPosition = Physics2D.gravity * _TimeStamp * _TimeStamp * 0.5f + forceApplied * _TimeStamp + (Vector2)ballPos;
                _Hit = Physics2D.Raycast(_TempPosition, _TempPosition - (Physics2D.gravity * (_TimeStamp + _DotSpacing) * (_TimeStamp + _DotSpacing) * 0.5f + forceApplied * (_TimeStamp + _DotSpacing) + (Vector2)ballPos), _DotSpacing, _ReflectionLayer);
                if (_Hit != false)
                {
                    forceApplied = bounciness * Vector2.Reflect(forceApplied, _Hit.normal);
                    forceApplied = new Vector2(forceApplied.x, -forceApplied.y);
                    _StartReflectPosition = _Hit.point;
                    _TimeStamp = _DotSpacing;
                    _TempPosition = Physics2D.gravity * _TimeStamp * _TimeStamp * 0.5f + forceApplied * _TimeStamp + _StartReflectPosition;
                    _IsReflection = true;
                }
            }
            else
            {
                _TempPosition = Physics2D.gravity * _TimeStamp * _TimeStamp * 0.5f + forceApplied * _TimeStamp + _StartReflectPosition;
            }
            _DotsList[i].position = _TempPosition;
            _TimeStamp += _DotSpacing;
        }
    }
    /// <summary>
    /// Show Trajectory
    /// </summary>
    public void Show()
    {
        _DotsParent.SetActive(true);
    }
    /// <summary>
    /// Hide Trajectory
    /// </summary>
    public void Hide()
    {
        _DotsParent.SetActive(false);
    }
}
