using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Blinking : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private float _repeatRate=0.5f;
    [SerializeField] private float _blinkDuration=0.5f;
    [SerializeField] private float _showDuration = 2f;
    // Start is called before the first frame update
    void Start()
    {
        //InvokeRepeating(nameof(ChangeVisible), _duration, _repeatRate);
        //don vi s

        //lenh kiem soat thoi gian chop cua 1 vat the
        // chop 10lan/s
        BlinkingObject();
    }

    public void BlinkingObject()
    {
        CancelInvoke(nameof(ChangeVisible));

        InvokeRepeating(nameof(ChangeVisible), _blinkDuration, _repeatRate);
    }
    private void ChangeVisible()
    {
        //spriteRenderer.enabled = !spriteRenderer.enabled;
        _image.enabled = !_image.enabled;
    }
    public void Show()
    {
        CancelInvoke(nameof(Hide));

        gameObject.SetActive(true);
        Invoke(nameof(Hide), _showDuration);
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
