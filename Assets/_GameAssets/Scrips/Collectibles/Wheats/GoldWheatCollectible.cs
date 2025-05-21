using UnityEngine;
public class GoldWheatCollectible : MonoBehaviour
{
    // PlayerController tipinde bir referans (oyuncuyu kontrol eden sınıf)
    [SerializeField] private PlayerController _playerController;

    // Oyuncunun hareket hızına eklenecek olan artış miktarı
    [SerializeField] private float _movementIncreaseSpeed;

    // Hız artışının ne kadar süreceğini belirleyen süre (saniye cinsinden)
    [SerializeField] private float _resetBoostDuration;

    // Bu metod, oyuncu bu nesneyi "topladığında" çağrılır
    public void Collect()
    {
        // Oyuncunun hareket hızını geçici olarak artır
        _playerController.SetMovementSpeed(_movementIncreaseSpeed, _resetBoostDuration);

        // Bu nesneyi sahneden yok et
        Destroy(gameObject);
    }
}
