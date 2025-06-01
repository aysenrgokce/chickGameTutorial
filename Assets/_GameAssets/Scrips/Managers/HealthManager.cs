using Unity.VisualScripting;
using UnityEngine;

public class HealthManager : MonoBehaviour
{

    [SerializeField] private int _maxHealth = 3;
    private int _currentHealth;
    private void Start()
    {
        _currentHealth = _maxHealth;
    }

    public void Damage(int damageAmount)
    {
        if (_currentHealth > 0)
        {
            _currentHealth -= damageAmount;
            //uI lara tanımla

            if (_currentHealth <= 0)
            {
                //uI 
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
