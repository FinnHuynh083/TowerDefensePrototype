using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

[Serializable]
public class UINotification : MonoBehaviour
{
    [SerializeField] private GameObject _notiObject;
    [SerializeField] private TMP_Text _notiText;
    //[SerializeField] private TMP_Text _NotEnoughGoldText;
    [SerializeField] private float _duration;

    [SerializeField] private float _moveUpStartRange;

    private Tween _fadeTween;
    private Tween _moveUP;
    private Vector3 _positionBeforeTween;

    private void Awake()
    {
        //_PositionBeforeTween = _notiObject.transform.position;
        _positionBeforeTween = gameObject.GetComponentInParent<Transform>().position;
    }
    public void Show()
    {
        //cancel invoke & stop coroutine
        _positionBeforeTween = gameObject.GetComponentInParent<Transform>().position;

        StopAllCoroutines();

        //StopCoroutine(FadeInOut(_duration));
        //StopCoroutine("FadeInOut");

        CancelInvoke(nameof(Hide));

        _notiObject.SetActive(true);
        //set alpha =0

        //Fade(0, 0);

        //move up
        MoveUp(_duration/2);
        //fade in out
        StartCoroutine(FadeInOut(_duration));

        Invoke(nameof(Hide), _duration);
    }
    public void Hide()
    {
        //fade
        //set active false
        _notiObject.SetActive(false);
    }

    private void Fade(float endValue,float duration)
    {
        if (_fadeTween != null)
        {
            _fadeTween.Kill(true);
        }
        //_fadeTween = _notiObject.DoFade
        _fadeTween = _notiText.DOFade(endValue, duration);
    }

    public void FadeIn(float duration)
    {
        Fade(0, 0);
        Fade(1f, duration);
    }

    public void FadeOut(float duration)
    {
        Fade(0f, duration);
    }

    private IEnumerator FadeInOut(float duration)
    {

        FadeIn(duration / 2);
        //fade 1>1 ko chuyen tu mo' sang ro~
        yield return new WaitForSeconds(duration / 2);
        FadeOut(duration / 2);
    }
    //lam them noti manager chua list UINoti

    private void MoveUp(float duration)
    {
        if (_moveUP != null)
        {
            _moveUP.Kill(false);
            _notiObject.transform.position = _positionBeforeTween;
        }
        _moveUP = _notiObject.transform.DOMoveY(_notiObject.transform.position.y, duration).From(_moveUpStartRange);
    }

    //viet lai show ko su dung coroutine/invoke>>sequence
    //viet lai ko dung dotween
}
