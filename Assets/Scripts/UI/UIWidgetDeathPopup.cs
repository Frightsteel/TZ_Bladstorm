using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIWidgetDeathPopup : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Popup _popup;

    private void OnEnable()
    {
        _player.OnDeathEvent += OnDeath;
    }

    private void OnDeath()
    {
        _popup.StartAnimation();
    }

    private void OnDisable()
    {
        _player.OnDeathEvent += OnDeath;
    }
}
