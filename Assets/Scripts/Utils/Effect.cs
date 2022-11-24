using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour
{
    [SerializeField]
    private Animator _Animator;
    [SerializeField]
    private bool _IsDestroy = true;

    void OnEnable()
    {
        StartCoroutine(IeWaitToDestroy());
    }

    private IEnumerator IeWaitToDestroy()
    {
        yield return new WaitForSeconds(_Animator.GetCurrentAnimatorClipInfo(0).Length + 0.5f);
        if (_IsDestroy)
            Pool.Instance.Destroy(PoolName.WALL_EFFECT, this.gameObject);
        else
            gameObject.SetActive(false);
    }
}
