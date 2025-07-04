using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class WinLoseUI : MonoBehaviour
{
    [Header("Referances")]
    [SerializeField] private GameObject _blackBackGroundObject;
    [SerializeField] private GameObject _winPopup;
    [SerializeField] private GameObject _losePopup;

    [Header("Settings")]
    [SerializeField] private float _animationDuration = 0.3f;

    private Image _blackBackGroundImage;
    private RectTransform _winPopupTransform;
    private RectTransform _losePopupTransform;

    private void Awake()
    {
        _blackBackGroundImage = _blackBackGroundObject.GetComponent<Image>();
        _winPopupTransform = _winPopup.GetComponent<RectTransform>();
        _losePopupTransform = _losePopup.GetComponent<RectTransform>();
    }

    public void OnGameWin()
    {
        _blackBackGroundObject.SetActive(true);
        _winPopup.SetActive(true);

        _blackBackGroundImage.DOFade(0.8f, _animationDuration).SetEase(Ease.Linear);
        _winPopupTransform.DOScale(1.5f, _animationDuration).SetEase(Ease.OutBack);
    }
    public void OnGameLose()
    {
        _blackBackGroundObject.SetActive(true);
        _losePopup.SetActive(true);

        _blackBackGroundImage.DOFade(0.8f, _animationDuration).SetEase(Ease.Linear);
        _losePopupTransform.DOScale(1.5f, _animationDuration).SetEase(Ease.OutBack);
    }
    

}
