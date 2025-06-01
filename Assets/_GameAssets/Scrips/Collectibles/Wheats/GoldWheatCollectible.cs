using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class GoldWheatCollectible : MonoBehaviour , ICollectible
{

    [SerializeField] private WheatDesignSO _wheatDesignSO;
    // PlayerController tipinde bir referans (oyuncuyu kontrol eden sınıf)
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private PlayerStateIU _playerStateUI;

    private RectTransform _playerBoosterTransform;
    private Image _playerBoosterImage;

    private void Awake()
    {
        _playerBoosterTransform = _playerStateUI.GetBoosterSpeedTransform;
        _playerBoosterImage = _playerBoosterTransform.GetComponent<Image>();
    }



    // Bu metod, oyuncu bu nesneyi "topladığında" çağrılır
    public void Collect()
    {
        // Oyuncunun hareket hızını geçici olarak artır
        if (_playerController == null)
        {
            Debug.LogError("PlayerController is NULL!");
            return;
        }

        if (_wheatDesignSO == null)
        {
            Debug.LogError("WheatDesignSO is NULL!");
            return;
        }

        _playerController.SetMovementSpeed(_wheatDesignSO.IncreaseDecreaseMultipler, _wheatDesignSO.ResetBoostDuration);

        _playerStateUI.PlayBoosterAnimations(_playerBoosterTransform,_playerBoosterImage,
        _playerStateUI.GetGoldBoosterWheatImage,_wheatDesignSO.ActiveSprite,_wheatDesignSO.PassiveSprite,
        _wheatDesignSO.ActiveWheatSprite,_wheatDesignSO.PassiveWheatSprite,_wheatDesignSO.ResetBoostDuration);

        Destroy(this.gameObject);
    }
}
