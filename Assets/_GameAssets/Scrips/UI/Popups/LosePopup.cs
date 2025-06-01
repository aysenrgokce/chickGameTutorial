using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class LosePopup : MonoBehaviour
{
    [Header("Referances")]
    [SerializeField] private TimerUI _timerUI;
    [SerializeField] private TMP_Text _timerText;
    [SerializeField] private Button _tryAgainButton;
    [SerializeField] private Button _mainMenuButton;
    
    private void OnEnable()
    {
        _timerText.text = _timerUI.GetFinalTime();

        _tryAgainButton.onClick.AddListener(OnTryAgainButtonCliked);
    }

    private void OnTryAgainButtonCliked()
    {
        SceneManager.LoadScene(Consts.ScaneNames.GAME_SCENE);
    }
}
