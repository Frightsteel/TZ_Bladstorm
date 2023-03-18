using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//temp
public class DisableInTime : MonoBehaviour
{
    [SerializeField] private float _timeToDisable;

    private Cooldown _disableCooldown;

    private void Awake()
    {
        _disableCooldown = new Cooldown(_timeToDisable, name);
    }

    private void OnEnable()
    {
        _disableCooldown.StartCooldown();
    }

    private void Update()
    {
        if (_disableCooldown.IsCooldowned)
            gameObject.SetActive(false);
    }
}
