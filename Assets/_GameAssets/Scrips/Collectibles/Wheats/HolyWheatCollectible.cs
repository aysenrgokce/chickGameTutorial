using UnityEngine;
using UnityEngine.UI;

public class HolyWheatCollectible : MonoBehaviour, ICollectible
{
    [SerializeField] private WheatDesignSO _wheatDesignSO;
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private PlayerStateIU _playerStateUI;

    private RectTransform _playerBoosterTransform;
    private Image _playerBoosterImage;

    private void Awake()
    {
        _playerBoosterTransform = _playerStateUI.GetBoosterJumpTransform;
        _playerBoosterImage = _playerBoosterTransform.GetComponent<Image>();
    }
    public void Collect()
    {
        if (_playerController == null)
        {
            Debug.LogError("HolyWheatCollectible: PlayerController is NULL!");
            return;
        }

        if (_wheatDesignSO == null)
        {
            Debug.LogError("HolyWheatCollectible: WheatDesignSO is NULL!");
            return;
        }

        _playerController.SetJumpForce(_wheatDesignSO.IncreaseDecreaseMultipler, _wheatDesignSO.ResetBoostDuration);

        
        _playerStateUI.PlayBoosterAnimations(_playerBoosterTransform,_playerBoosterImage,
        _playerStateUI.GetHolyBoosterWheatImage,_wheatDesignSO.ActiveSprite,_wheatDesignSO.PassiveSprite,
        _wheatDesignSO.ActiveWheatSprite,_wheatDesignSO.PassiveWheatSprite,_wheatDesignSO.ResetBoostDuration);
        Destroy(this.gameObject);
    }
}
