using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] private Image _filler;
    [SerializeField] private TextMeshProUGUI _textValue;

    public void SetValue(float valueNormalized)
    {
        _filler.fillAmount = valueNormalized;

        var valueInPercent = Mathf.RoundToInt(valueNormalized * 100f);
        _textValue.text = $"{valueInPercent}%";
    }
}
