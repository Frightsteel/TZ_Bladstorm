using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModifierHolder : MonoBehaviour
{
    [SerializeField] private List<BaseModifier> _modifiers;

    [SerializeField] private BaseModifier _startModifier;

    private BaseCharacter _character;

    private void Awake()
    {
        _modifiers = new List<BaseModifier>();
        _character = GetComponent<BaseCharacter>();
    }

    private void Start()
    {
        Apply(_startModifier);
    }

    public void Apply(BaseModifier modifierToApply)
    {
        _modifiers.Add(modifierToApply);
        modifierToApply.ApplyModifier(_character);
    }

    public void Unapply(BaseModifier modifierToUnapply)
    {
        _modifiers.Remove(modifierToUnapply);
        modifierToUnapply.ApplyModifier(_character);
    }
}
