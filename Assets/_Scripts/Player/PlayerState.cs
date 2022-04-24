using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New PlayerState", menuName = "ScriptableObjects/States/PlayerState")]
public class PlayerState : ScriptableObject
{
    [SerializeField] private int _health;
    public int health
    {
        get { return _health; }
        set
        {
            if (_health == value) 
                return;
            _health = Mathf.Clamp(value, 0, maxHealth);
            OnHealthChanged.Invoke();
        }
    }
    public int maxHealth = 4;
    public bool isInvincible = false;
    public bool isDead = false;
    public bool isTraveling = false;
    public bool hasWeapon = false;
    public List<int> universeCrystals;
    
    public void TakeDamage(int damage)
    {
        if (!isInvincible && !isDead)
        {
            health -= damage;
            Debug.Log($"Took damage {damage} {health}");
            OnDamageTaken.Invoke();
            if (health <= 0)
            {
                Debug.Log("Died");
                isDead = true;
                OnDeath.Invoke();
            }
        }
    }

    public float damagedInvincibilityDuration;

    public UnityEvent OnHealthChanged;
    public UnityEvent OnDamageTaken;
    public UnityEvent OnDeath;


}
