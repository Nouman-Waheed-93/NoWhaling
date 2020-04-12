using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour {

    public IntEvent onDamage = new IntEvent();
    public UnityEvent onDie = new UnityEvent();
    public IntEvent onHeal = new IntEvent();
    public UnityEvent onHealed = new UnityEvent();

    [SerializeField]
    private int maxHealth;
    private int currHealth;
    public bool isDead { get;  private set; }
    public bool isHealthy { get; private set; }

    private void Start()
    {
        ResetHealth();
    }

    public void SetMaxHealth(int maxHealth)
    {
        this.maxHealth = maxHealth;
    }

    public float GetHlthinPerOne()
    {
        return (float)currHealth /maxHealth;
    }

    public void Damage(int damage)
    {
        isHealthy = false;

        BleedOffHealth(damage);
        onDamage.Invoke(damage);
    }

    public void BleedOffHealth(int amt)
    {
        if (isDead)
            return;
        currHealth -= amt;
        if (currHealth <= 0)
            Die();
    }

    public void Die() {
        isDead = true;
        onDie.Invoke();
    }

    public void Heal(int healAmt)
    {
        currHealth += healAmt;
        onHeal.Invoke(healAmt);
        if(currHealth >= maxHealth)
        {
            currHealth = maxHealth;
            Healed();
        }
    }

    public void Healed()
    {
        isHealthy = true;
        onHealed.Invoke();
    }

    void ResetHealth()
    {
        isHealthy = true;
        isDead = false;
        currHealth = maxHealth;
    }
    
}
