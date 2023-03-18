using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Data", menuName = "Ability/Data")]
public class AbilityData : ScriptableObject
{
    public string Name;
    public string Title;
    public Sprite Icon;
}
