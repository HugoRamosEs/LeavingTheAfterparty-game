using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class Stat
{
    public int maxVal;
    public int currentVal;

    public Stat(int current, int max)
    {
        maxVal = max;
        currentVal = current;
    }
    internal void Substract(int amount)
    {
        currentVal -= amount;
    }

    internal void Add(int amount)
    {
        currentVal += amount;
        if (currentVal > maxVal)
        {
            currentVal = maxVal;
        }
    }

    internal void SetToMax()
    {
        currentVal = maxVal;
    }
}
public class Player : MonoBehaviour
{
    public Stat hp;
    [SerializeField] PlayerStatus hpBar;
    public Stat stamina;
    [SerializeField] PlayerStatus staminaBar;
    public bool isDead;
    public bool isExhausted;
    private void Start()
    {
        hpBar.Set(hp.currentVal, hp.maxVal);
        staminaBar.Set(stamina.currentVal, stamina.maxVal);
    }
    private void UpdateHpBar()
    {
        hpBar.Set(hp.currentVal, hp.maxVal);
    }

    private void UpdateStaminaBar()
    {
        staminaBar.Set(stamina.currentVal, stamina.maxVal);
    }
    public void TakeDamage(int amount)
    {
        hp.Substract(amount);
        if (hp.currentVal <= 0)
        {
            isDead = true;
        }
        UpdateHpBar();
    }
    public void Heal(int amount)
    {
        hp.Add(amount);
        UpdateHpBar();
    }
    public void FullHeal()
    {
        hp.SetToMax();
        UpdateHpBar();
    }

    public void GetTired(int amount)
    {
        stamina.Substract(amount);
        if (stamina.currentVal <= 0)
        {
            isExhausted = true;
        }
        UpdateStaminaBar();
    }
    public void Rest(int amount)
    {
        stamina.Add(amount);
        UpdateStaminaBar();
    }
    public void FullRest()
    {
        stamina.SetToMax();
        UpdateStaminaBar();
    }
}
