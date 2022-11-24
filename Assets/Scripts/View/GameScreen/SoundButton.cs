using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundButton : MonoBehaviour
{
    [SerializeField]
    private Transform _OffPosition;
    [SerializeField]
    private Transform _OnPosition;
    [SerializeField]
    private GameObject _Toggle;
    [SerializeField]
    private Image _ButtonColor;
    [SerializeField]
    private Color _OnColor;
    [SerializeField]
    private Color _OffColor;

    private bool isOn = true;

    private void Start()
    {
        EventHub.Instance.RegisterEvent(EventName.CHANGE_SOUND_SETTING, ChangeSoundSetting);
    }

    public void OnButtonClicked()
    {
        LeanTween.moveLocal(_Toggle, isOn ? _OffPosition.localPosition : _OnPosition.localPosition, 0.5f);
        _ButtonColor.color = isOn ? _OnColor : _OffColor;
        isOn = !isOn;
    }

    private void ChangeSoundSetting(object data)
    {
        isOn = (bool)data;
        LeanTween.moveLocal(_Toggle, isOn ? _OffPosition.localPosition : _OnPosition.localPosition, 0.5f);
        _ButtonColor.color = isOn ? _OnColor : _OffColor;
    }
}
