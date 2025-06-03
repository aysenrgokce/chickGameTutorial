using MaskTransitions;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinPopup : MonoBehaviour
{
    [Header("Referances")]
    [SerializeField] private TimerUI _timerUI;
    [SerializeField] private TMP_Text _timerText;
    [SerializeField] private Button _oneMoreButton;
    [SerializeField] private Button _mainMenuButton;

    private void OnEnable()
    {
        _timerText.text = _timerUI.GetFinalTime();

        _oneMoreButton.onClick.AddListener(OnOneMoreButtonCliked);
          _mainMenuButton.onClick.AddListener(() =>
        {
            TransitionManager.Instance.LoadLevel(Consts.ScaneNames.MENU_SCENE);
        });
    }

    private void OnOneMoreButtonCliked()
    {
         TransitionManager.Instance.LoadLevel(Consts.ScaneNames.GAME_SCENE);
    }
}

