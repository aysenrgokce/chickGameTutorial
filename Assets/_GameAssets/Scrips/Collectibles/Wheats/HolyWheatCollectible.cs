using UnityEngine;

public class HolyWheatCollectible : MonoBehaviour, ICollectible
{
    [SerializeField] private WheatDesignSO _wheatDesignSO;
    [SerializeField] private PlayerController _playerController;

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

        Destroy(this.gameObject);
    }
}
