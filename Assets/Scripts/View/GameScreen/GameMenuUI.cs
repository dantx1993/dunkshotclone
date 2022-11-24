using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMenuUI : UIController
{
    [SerializeField]
    private Image _Logo;
    [SerializeField]
    private Image _DragIt;
    [SerializeField]
    private GameMenuAnimation _UIAnimationController;

    private Vector3 Vector3One = Vector3.one;

    private void Awake()
    {
        _UIAnimationController.uiAnimation += StartUIAnimation;
        _UIAnimationController.stopUiAnimation += StopUIAnimation;
        StartCoroutine(IeChangePositionWithBasket());
    }

    public void StartUIAnimation()
    {
        StartCoroutine(IeScale(_Logo.gameObject, new Vector3(1.05f, 1.05f, 1), Vector3.one, Vector3.zero));
        StartCoroutine(IeScale(_DragIt.gameObject, new Vector3(1.05f, 1.1f, 1), Vector3.one, new Vector3(0, 0, -17f)));
    }
    public void StopUIAnimation()
    {
        StopAllCoroutines();
    }
    private IEnumerator IeScale(GameObject go, Vector3 scaleUp, Vector3 scaleDown, Vector3 rotateTo, float time = 1)
    {
        LeanTween.scale(go, scaleDown, 0f);
        LeanTween.rotate(go, Vector3.zero, 0f);
        while (true)
        {
            LeanTween.scale(go, scaleUp, time);
            LeanTween.rotate(go, rotateTo, time);
            yield return new WaitForSeconds(time);
            LeanTween.scale(go, scaleDown, time);
            LeanTween.rotate(go, Vector3.zero, time);
            yield return new WaitForSeconds(time);
        }
    }
    private IEnumerator IeChangePositionWithBasket()
    {
        yield return new WaitWhile(() => GameManager.Instance != null && GameManager.Instance.basketSize.x != 0);
        _DragIt.rectTransform.anchoredPosition = new Vector2(GameManager.Instance.firstBasketPositionInScreen.x - 15f, GameManager.Instance.firstBasketPositionInScreen.y - GameManager.Instance.basketSize.y);
    }

    public void OnCustomizeClicked()
    {
        GameManager.Instance.gameState = GameState.CUSTOMIZE;
        SoundController.Instance.Play("ButtonClick");
    }
}
