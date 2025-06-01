using UnityEngine;

public class EggCollectible : MonoBehaviour, ICollectible
{
    public void Collect()
    {
        GameManagers.Instance.OnEggCollected();
        Destroy(this.gameObject);
        
    }
}
