using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class Popup : MonoBehaviour
{
    [SerializeField] private SceneLoader _loader;
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private float _fadeTime;

    private RectTransform _rect;
    private CanvasGroup _canvasGroup;

    private void Awake()
    {
        _rect = GetComponent<RectTransform>();
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    public void StartAnimation()
    {
        gameObject.SetActive(true);
        _rect.transform.localPosition = new Vector3(0, -1000f, 0);
        _rect.DOAnchorPos(new Vector2(0f, 0f), _fadeTime, false).SetEase(Ease.OutElastic).SetUpdate(true).OnComplete(() => TextAnimation());
        _canvasGroup.DOFade(1, _fadeTime).SetUpdate(true);
    }

    private void TextAnimation()
    {
        _text.transform.DOScale(1f, _fadeTime).SetEase(Ease.OutBounce).SetUpdate(true).OnComplete(() => _loader.RestartScene());
    }
}
