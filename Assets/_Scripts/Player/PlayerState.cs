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
    
    public void TakeDamage(int damage)
    {
        if (!isInvincible)
        {
            health -= damage;
            OnDamageTaken.Invoke();
        }
    }

    public UnityEvent OnHealthChanged;
    public UnityEvent OnDamageTaken;


}
