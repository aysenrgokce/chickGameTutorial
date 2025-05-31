using UnityEngine;

public class PlayerInteractionController : MonoBehaviour
{

    private PlayerController _playerController;

    private void Awake()
    {
        _playerController = GetComponent<PlayerController>();
    }
    //OnTriggerEnter(Collider other);Bir nesne, Trigger olarak ayarlanmış bir Collider'ın içine girdiğinde bu metod çağrılır.
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<ICollectible>(out var collectible))
        {
            collectible.Collect();
        }
    }
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.TryGetComponent<IBoostable>(out var boostable))
        {
            boostable.Boost(_playerController);
        } 
    }
}


