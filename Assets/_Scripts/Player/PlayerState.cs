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
    
    public void TakeDamage(int damage)
    {
        if (!isInvincible && !isDead)
        {
            health -= damage;
            OnDamageTaken.Invoke();
            if (health <= 0)
            {
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
