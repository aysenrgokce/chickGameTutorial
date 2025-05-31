using UnityEngine;

// Unity editöründe "WheatDesignSO" adlı bir ScriptableObject dosyası oluşturulmasını sağlar
[CreateAssetMenu(fileName = "WheatDesignSO", menuName = "ScriptableObjects/WheatDesignSO")]
public class WheatDesignSO : ScriptableObject
{
    // Oyuncunun hareket ya da zıplama hızındaki artış/azalış oranı (çarpan olarak kullanılır)
    [SerializeField] private float _increaseDecreaseMultipler;

    // Bu güçlendirme ya da yavaşlatmanın ne kadar süreceğini belirten süre (saniye cinsinden)
    [SerializeField] private float _resetBoostDuration;

    // Diğer sınıflardan bu değere sadece okuma (get) izni verilir
    public float IncreaseDecreaseMultipler => _increaseDecreaseMultipler;

    // Diğer sınıflardan bu değere sadece okuma (get) izni verilir
    public float ResetBoostDuration => _resetBoostDuration;
}

