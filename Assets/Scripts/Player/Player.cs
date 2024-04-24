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
    public bool isInvulnerable;
    [SerializeField] ItemToolBarPanel toolBarPanel;

    public DeathScreen deathScreenPnl;

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
        if (isInvulnerable)
        {
            return;
        }

        hp.Substract(amount);
        if (hp.currentVal <= 0.1)
        {
            isDead = true;
            GameOver();
        }
        UpdateHpBar();
    }

    public void GameOver()
    {
        deathScreenPnl.Setup();

        hpBar.gameObject.SetActive(false);
        staminaBar.gameObject.SetActive(false);
        toolBarPanel.gameObject.SetActive(false);

        gameObject.SetActive(false);
        Time.timeScale = 0;
    }

    IEnumerator FreezeGameAfterDelay(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);

        Time.timeScale = 0;
        gameObject.SetActive(false);
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

    public IEnumerator Invulnerability(float duration)
    {
        isInvulnerable = true; 
        yield return new WaitForSeconds(duration);
        isInvulnerable = false;
    }
}
