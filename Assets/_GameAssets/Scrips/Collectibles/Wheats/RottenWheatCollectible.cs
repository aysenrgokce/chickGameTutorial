using UnityEngine;

public class RottenWheatCollectible : MonoBehaviour, ICollectible
{
    [SerializeField] private WheatDesignSO _wheatDesignSO;
    [SerializeField] private PlayerController _playerController;

    public void Collect()
    {
        if (_playerController == null)
        {
            Debug.LogError("RottenWheatCollectible: PlayerController is NULL!");
            return;
        }

        if (_wheatDesignSO == null)
        {
            Debug.LogError("RottenWheatCollectible: WheatDesignSO is NULL!");
            return;
        }

        _playerController.SetMovementSpeed(_wheatDesignSO.IncreaseDecreaseMultipler, _wheatDesignSO.ResetBoostDuration);

        Destroy(this.gameObject);
    }
}
