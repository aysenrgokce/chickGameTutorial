using UnityEngine;
public class GoldWheatCollectible : MonoBehaviour , ICollectible
{

    [SerializeField] private WheatDesignSO _wheatDesignSO;
    // PlayerController tipinde bir referans (oyuncuyu kontrol eden sınıf)
    [SerializeField] private PlayerController _playerController;

 

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

        Destroy(this.gameObject);
    }
}
