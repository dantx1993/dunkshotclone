using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonBall : MonoBehaviour
{
    [SerializeField]
    private Image _BallIcon;
    [SerializeField]
    private TextMeshProUGUI _BallText;
    [SerializeField]
    private GameObject _ChooseLayer;
    [SerializeField]
    private Ball _Data;

    private void Start()
    {
        EventHub.Instance.RegisterEvent(EventName.CHANGE_BALL_THEME, ListBallThemeChange);
    }
    private void OnEnable()
    {
        _BallText.text = _Data.ballName;
        _BallIcon.sprite = _Data.render;
    }
    private IEnumerator IeScale()
    {
        while (true)
        {
            LeanTween.scale(_BallIcon.gameObject, new Vector3(1.1f, 1.1f, 1), 0.5f);
            yield return new WaitForSeconds(0.5f);
            LeanTween.scale(_BallIcon.gameObject, Vector3.one, 0.5f);
            yield return new WaitForSeconds(0.5f);
        }
    }
    public void OnButtonBallClicked()
    {
        _ChooseLayer.SetActive(true);
        StartCoroutine(IeScale());
        GameManager.Instance.ballTheme = _Data;
    }
    private void ListBallThemeChange(object data)
    {
        string name = data.ToString();
        if (_Data.ballName != name)
        {
            _ChooseLayer.SetActive(false);
            StopAllCoroutines();
        }
    }
}
