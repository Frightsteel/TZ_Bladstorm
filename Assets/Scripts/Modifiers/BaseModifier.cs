using UnityEngine;

public abstract class BaseModifier : ScriptableObject
{
    [SerializeField] protected AbilityData Data;

    public abstract void ApplyModifier(BaseCharacter character);
    public abstract void UnapplyModifier(BaseCharacter character);
}
