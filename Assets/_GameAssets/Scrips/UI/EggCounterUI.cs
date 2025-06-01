using DG.Tweening;
using TMPro;
using UnityEngine;

public class EggCounterUI : MonoBehaviour
{
    [Header("Referances")]
    [SerializeField] private TMP_Text _eggCounterText;
    [Header("Settings")]
    [SerializeField] private Color _eggCounterColor;
    [SerializeField] private float _colorDuraction;
    [SerializeField] private float _scaleDuration;

    private RectTransform _eggCounterRectTransform;

    private void Awake()
    {
        _eggCounterRectTransform = _eggCounterText.gameObject.GetComponent<RectTransform>();

    }

    public void SetEggCounterText(int counter, int max)
    {
        _eggCounterText.text = counter.ToString() + "/" + max.ToString();
    }

    public void SetEggCompleted()
    {
        _eggCounterText.DOColor(_eggCounterColor, _colorDuraction);

        _eggCounterRectTransform.DOScale(1.2f, _scaleDuration).SetEase(Ease.OutBack);
    }

}
