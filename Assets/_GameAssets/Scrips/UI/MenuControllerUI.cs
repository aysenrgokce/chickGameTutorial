using MaskTransitions;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuControllerUI : MonoBehaviour
{
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _quitButton;
    private void Awake()
    {
        Debug.Log("aaaaaaaaaaaaşögfdih");
        _playButton.onClick.AddListener(() =>
        {
            TransitionManager.Instance.LoadLevel(Consts.ScaneNames.GAME_SCENE);
        });
        _quitButton.onClick.AddListener(() =>
        {
          
            Application.Quit();
        });
    }
    
}
