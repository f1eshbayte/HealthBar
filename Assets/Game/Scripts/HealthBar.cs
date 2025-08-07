using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [Header("Heal Flash Settings")]
    [SerializeField] private float _healFlashToColorTime = 0.15f;
    [SerializeField] private float _healFlashHoldTime = 0.1f;
    [SerializeField] private float _healFlashBackTime = 0.3f;
    [SerializeField] private Color _healFlashColor = Color.green;
    
    [Header("Damage Flash Settings")]
    [SerializeField] private float _damageFlashToColorTime = 0.1f;
    [SerializeField] private float _damageFlashBackTime = 0.1f;
    [SerializeField] private Color _damageFlashColor = Color.white;

    [Header("Slider Settings")]
    [SerializeField] private Slider _slider; 
    [SerializeField] private Image _fillImage;

    [Header("Slider Scale Settings")]
    [SerializeField] private float _durationToScale = 0.2f;
    [SerializeField] private float _changeIfHeal = 1.1f;
    [SerializeField] private float _changeIfDamage = 0.9f;
    [SerializeField] private float _changeScaleDuration = 0.2f;
    
    private RectTransform _rectTransform;
    private Vector3 _origScale;
    private Tween _fadeTween;
    private Color _origColor;
    
    private float _previousHealth = 1f;

    private void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
        _origScale = _rectTransform.localScale;
        _origColor = _fillImage.color;
    }

    public void UpdateHealth(float targetNormalizedHealth)
    {
        if (Mathf.Approximately(targetNormalizedHealth, _previousHealth))
            return;

        bool isHeal = targetNormalizedHealth > _previousHealth;
        _slider.DOValue(targetNormalizedHealth, _durationToScale);

        AnimateScale(isHeal);

        if (!isHeal)
            FlashOnDamage();
        else
            FlashOnHeal();

        _previousHealth = targetNormalizedHealth;
    }

    private void AnimateScale(bool isHeal)
    {
        Vector3 targetScale = isHeal ? _origScale * _changeIfHeal : _origScale * _changeIfDamage;

        _rectTransform
            .DOScale(targetScale, _changeScaleDuration)
            .OnComplete(() => _rectTransform.DOScale(_origScale, _changeScaleDuration));
    }

    private void FlashOnDamage()
    {
        if (_fillImage == null) return;

        _fillImage.color = _origColor;
        _fadeTween?.Kill(true);

        _fadeTween = DOTween.Sequence()
            .Append(_fillImage.DOColor(_damageFlashColor, _damageFlashToColorTime))
            .Append(_fillImage.DOColor(_origColor, _damageFlashBackTime));
    }


    private void FlashOnHeal()
    {
        if (_fillImage == null) return;

        _fillImage.color = _origColor;
        _fadeTween?.Kill(true);

        _fadeTween = DOTween.Sequence()
            .Append(_fillImage.DOColor(_healFlashColor, _healFlashToColorTime))
            .AppendInterval(_healFlashHoldTime)
            .Append(_fillImage.DOColor(_origColor, _healFlashBackTime));
    }


}