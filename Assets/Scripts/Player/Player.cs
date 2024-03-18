using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class Stat
{
    public float maxVal;
    public float currentVal;

    public Stat(float current, float max)
    {
        maxVal = max;
        currentVal = current;
    }
    internal void Substract(float amount)
    {
        currentVal -= amount * Time.deltaTime;
    }

    internal void Add(float amount)
    {
        currentVal += amount * Time.deltaTime;
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
    public void UpdateHpBar()
    {
        hpBar.Set(hp.currentVal, hp.maxVal);
    }

    public void UpdateStaminaBar()
    {
        staminaBar.Set(stamina.currentVal, stamina.maxVal);
    }
    public void TakeDamage(float amount)
    {
        hp.Substract(amount);
        if (hp.currentVal <= 0)
        {
            isDead = true;
        }
        UpdateHpBar();
    }
    public void Heal(float amount)
    {
        hp.Add(amount);
        UpdateHpBar();
    }
    public void FullHeal()
    {
        hp.SetToMax();
        UpdateHpBar();
    }

    public void GetTired(float amount)
    {
        stamina.Substract(amount);
        if (stamina.currentVal <= 0)
        {
            isExhausted = true;
        }
        UpdateStaminaBar();
    }
    public void Rest(float amount)
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
