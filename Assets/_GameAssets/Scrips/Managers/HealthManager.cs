using Unity.VisualScripting;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    
    public static HealthManager Instance { get; private set; }
     [Header("Referances")]
    [SerializeField] private PlayerHealthUI _playerHealthUI;
     [Header("Settings")]
    [SerializeField] private int _maxHealth = 3;
    private int _currentHealth;
    void Awake()
    {
        Instance = this; 
    }
    private void Start()
    {
        _currentHealth = _maxHealth;
    }

    public void Damage(int damageAmount)
    {
        if (_currentHealth > 0)
        {
            _currentHealth -= damageAmount;
            _playerHealthUI.AnimateDamage();
            if (_currentHealth <= 0)
            {
                GameManagers.Instance.PlayGameOver();
            }
        }

    }
    public void Heal(int healAmaunt)
    {
        if (_currentHealth < _maxHealth)
        {
            _currentHealth = Mathf.Min(_currentHealth + healAmaunt, _maxHealth);
        }
    }
}
