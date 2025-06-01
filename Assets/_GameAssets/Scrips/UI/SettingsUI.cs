using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class SettingsUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject _settingsPopupObject;
    [SerializeField] private GameObject _blackBackgroundObject;
    [Header("Buttons")]
    [SerializeField] private Button _settingsButton;
    [SerializeField] private Button _musicButton;
    [SerializeField] private Button _soundButton;
    [SerializeField] private Button _resumeButton;
    [SerializeField] private Button _mainMenuButton;
    [Header("Settings")]
    [SerializeField] private float _animationDuration;



    private Image _blacBackgroundImage;
    private void Awake()
    {
        _blacBackgroundImage = _blackBackgroundObject.GetComponent<Image>();
        _settingsPopupObject.transform.localScale = Vector3.zero;

        _settingsButton.onClick.AddListener(OnSettingsButtonCliked);
        _resumeButton.onClick.AddListener(OnResumeButtonClicked);
    }

    private void OnSettingsButtonCliked()
    {
        GameManagers.Instance.ChangeGameState(GameState.Pause);
        _blackBackgroundObject.SetActive(true);
        _settingsPopupObject.SetActive(true);

        _blacBackgroundImage.DOFade(0.8f, _animationDuration).SetEase(Ease.Linear);
        _settingsPopupObject.transform.DOScale(1.5f, _animationDuration).SetEase(Ease.OutBack);

    }
    private void OnResumeButtonClicked()
    {
    _blacBackgroundImage.DOFade(0f, _animationDuration).SetEase(Ease.Linear);
    
    _settingsPopupObject.transform.DOScale(0f, _animationDuration)
        .SetEase(Ease.OutExpo)
        .OnComplete(() =>
        {
            GameManagers.Instance.ChangeGameState(GameState.Resume);
            _blackBackgroundObject.SetActive(false);
            _settingsPopupObject.SetActive(false);
        });
    }

}
