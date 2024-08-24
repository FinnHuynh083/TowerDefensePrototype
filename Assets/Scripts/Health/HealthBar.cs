using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private GameObject _healthBar;
    [SerializeField] private TMP_Text _healthText;
    [SerializeField] private Health _health;
    [SerializeField] private float _visibleDuration;

    public Camera _mainCamera;

    private float _currentRatio = 1;
    private Slider _healthSlider;
    private void Start()
    {
        _healthSlider = _healthBar.GetComponent<Slider>();
        UpdateHealthBar();
    }
    public void UpdateHealthBar()
    {
        float _currentHealth = _health.CurrentHealth;
        float _maxHealth = _health.MaxHealth;

        _currentRatio = _currentHealth / _maxHealth;

        _healthSlider.value = _currentRatio;
        _healthText.SetText($"{_currentHealth}/{_maxHealth}");
    }

    public void Hide()
    {
        _healthBar.SetActive(false);
    }

    public void Show()
    {
        CancelInvoke();
        _healthBar.SetActive(true);
        Invoke(nameof(Hide), _visibleDuration);
        _healthBar.transform.LookAt(_mainCamera.transform.position);
    }
}
