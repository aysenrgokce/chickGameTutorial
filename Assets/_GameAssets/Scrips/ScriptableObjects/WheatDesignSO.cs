using UnityEngine;

// Unity editöründe "WheatDesignSO" adlı bir ScriptableObject dosyası oluşturulmasını sağlar
[CreateAssetMenu(fileName = "WheatDesignSO", menuName = "ScriptableObjects/WheatDesignSO")]
public class WheatDesignSO : ScriptableObject
{
    // Oyuncunun hareket ya da zıplama hızındaki artış/azalış oranı (çarpan olarak kullanılır)
    [SerializeField] private float _increaseDecreaseMultipler;

    // Bu güçlendirme ya da yavaşlatmanın ne kadar süreceğini belirten süre (saniye cinsinden)
    [SerializeField] private float _resetBoostDuration;
    [SerializeField] private Sprite _activeSprite;
    [SerializeField] private Sprite _passiveSprite;
    [SerializeField] private Sprite _activeWheatSprite;
    [SerializeField] private Sprite _passiveWheatSprite;

    // Diğer sınıflardan bu değere sadece okuma (get) izni verilir
    public float IncreaseDecreaseMultipler => _increaseDecreaseMultipler;

    // Diğer sınıflardan bu değere sadece okuma (get) izni verilir
    public float ResetBoostDuration => _resetBoostDuration;

    public Sprite ActiveSprite => _activeSprite;
    public Sprite PassiveSprite => _passiveSprite;
    public Sprite ActiveWheatSprite => _activeWheatSprite;
    public Sprite PassiveWheatSprite => _passiveWheatSprite;

}

