using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIWidgetLifeBar : MonoBehaviour
{
    [SerializeField] private BaseCharacter _character;
    [SerializeField] private ProgressBar _progressBar;

    private void OnEnable()
    {
        _character.OnCurrentHealthValueChangedEvent += OnCurrentHealthValueChanged;
    }

    private void OnCurrentHealthValueChanged(float obj)
    {
        var normilizedHealth = (float)obj / _character.MaxHealth;
        _progressBar.SetValue(normilizedHealth);
    }

    private void OnDisable()
    {
        if (_character)
            _character.OnCurrentHealthValueChangedEvent -= OnCurrentHealthValueChanged;
    }
}
