using UnityEngine;

[CreateAssetMenu(fileName = "New Modifier", menuName = "Modifiers/Bloodlust")]
public class BloodlustModifier : BaseModifier
{
    [SerializeField] private float _maxHpVampirism;
    [SerializeField] private float _maxHpDamage;

    public override void ApplyModifier(BaseCharacter character)
    {
        character.MaxHpVampirism += _maxHpVampirism;
        character.MaxHpDamage += _maxHpDamage;
    }

    public override void UnapplyModifier(BaseCharacter character)
    {
        character.MaxHpVampirism -= _maxHpVampirism;
        character.MaxHpDamage -= _maxHpDamage;
    }
}
