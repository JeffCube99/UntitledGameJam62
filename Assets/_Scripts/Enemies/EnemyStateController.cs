using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyStateController : MonoBehaviour
{
    public int maxHealth;
    private List<Collider2D> colliders;
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

    public bool isDead;

    private void Start()
    {
        colliders = new List<Collider2D>(GetComponents<Collider2D>());
        Respawn();
    }

    public void Respawn()
    {
        health = maxHealth;
        foreach (Collider2D collider in colliders)
            collider.enabled = true;
        OnRespawn.Invoke();
    }

    public void TakeDamage(int damage)
    {
        if (!isDead)
        {
            health -= damage;
            OnDamageTaken.Invoke();
            if (health <= 0)
            {
                isDead = true;
                foreach (Collider2D collider in colliders)
                    collider.enabled = false;
                OnDeath.Invoke();
            }
        }
    }

    public UnityEvent OnDamageTaken;
    public UnityEvent OnHealthChanged;
    public UnityEvent OnDeath;
    public UnityEvent OnRespawn;
}
