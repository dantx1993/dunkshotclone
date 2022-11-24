using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Popup : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _PopupText;

    private void Start()
    {
        StartCoroutine(IeAnimation());
    }

    public void ShowPopup(int time, bool isPerfect)
    {
        if (isPerfect)
        {
            _PopupText.color = Color.red;
            _PopupText.text = $"Perfect +{time}";
        }
        else
        {
            _PopupText.color = Color.blue;
            _PopupText.text = $"Bounce";
        }
    }

    private IEnumerator IeAnimation()
    {
        LeanTween.move(this.gameObject, this.transform.position + new Vector3(0, 1, 0), 0.5f);
        yield return new WaitForSeconds(0.5f);
        Pool.Instance.Destroy(PoolName.POPUP, this.gameObject);
    }
}
