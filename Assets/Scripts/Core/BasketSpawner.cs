using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasketSpawner : MonoBehaviour
{
    [SerializeField]
    private Camera _MainCamera;
    [SerializeField]
    private GameObject _BasketPrefab;

    private float _BasketWidth;
    private SpriteRenderer _SpriteRenderer;

    private void Start()
    {
        _SpriteRenderer = _BasketPrefab.GetComponent<BasketController>().renderForRandom;

        float basketWidthMax = _MainCamera.WorldToScreenPoint(new Vector3(_SpriteRenderer.bounds.max.x, 0, 0)).x;
        float basketWidthMin = _MainCamera.WorldToScreenPoint(new Vector3(_SpriteRenderer.bounds.min.x, 0, 0)).x;

        _BasketWidth = basketWidthMax - basketWidthMin;

        EventHub.Instance.RegisterEvent(EventName.SPAWN_BASKET, SpawnBasketAtRandomPostion);
    }

    #region Spawn Basket
    private float _StartPointX = 0;
    private float _EndPointX = 0;
    private Vector3 _CurrentPostion;
    private float _StartPointY = 0;
    private float _EndPointY = 0;
    private float _RandX;
    private float _RandY;
    private Vector3 _SpawnPosition;
    private void SpawnBasketAtRandomPostion(object data)
    {
        Transform currentBasket = (Transform)data;

        _StartPointX = 0;
        _EndPointX = 0;

        _CurrentPostion = _MainCamera.WorldToScreenPoint(currentBasket.transform.position);

        if (_CurrentPostion.x > Screen.width / 2)
        {
            _EndPointX = _CurrentPostion.x - _BasketWidth;
            _StartPointX = _BasketWidth;
        }
        else
        {
            _StartPointX = _CurrentPostion.x + _BasketWidth;
            _EndPointX = Screen.width - _BasketWidth / 2;
        }

        _StartPointY = _CurrentPostion.y + (Screen.height * 0.15f);
        _EndPointY = _CurrentPostion.y + (Screen.height * 0.35f);

        _RandX = Random.Range(_StartPointX, _EndPointX);
        _RandY = Random.Range(_StartPointY, _EndPointY);


        _SpawnPosition = _MainCamera.ScreenToWorldPoint(new Vector3(_RandX, _RandY, _MainCamera.nearClipPlane));


        Pool.Instance.Instantiate(PoolName.BASKET, _BasketPrefab, _SpawnPosition, Quaternion.identity);
    }
    #endregion
}
