using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This script is used to control the player.
/// </summary>
[Serializable]
public class Stat
{
    public float maxVal;
    public float currentVal;

    /// <summary>
    /// This method is used to set the current and max value of the stat
    /// </summary>
    /// <param name="current"> current value</param>
    /// <param name="max"> max value</param>
    public Stat(float current, float max)
    {
        maxVal = max;
        currentVal = current;
    }
    /// <summary>
    /// This method is used to substract the amount of the stat
    /// </summary>
    /// <param name="amount"> the amount of the stat</param>
    internal void Substract(float amount)
    {
        currentVal -= amount * Time.deltaTime;
    }

    /// <summary>
    /// This method is used to add the amount of the stat
    /// </summary>
    /// <param name="amount"> the amount of stat</param>
    internal void Add(float amount)
    {
        currentVal += amount * Time.deltaTime;
        if (currentVal > maxVal)
        {
            currentVal = maxVal;
        }
    }

    /// <summary>
    /// This method is used to set the stat to the max value
    /// </summary>
    internal void SetToMax()
    {
        currentVal = maxVal;
    }
}
/// <summary>
/// This script is used to control the player.
/// </summary>
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

    /// <summary>
    /// This method is used to ensure the functionality of the player
    /// </summary>
    private void Start()
    {
        hpBar.Set(hp.currentVal, hp.maxVal);
        staminaBar.Set(stamina.currentVal, stamina.maxVal);
    }
    /// <summary>
    /// This method is used to update the hp bar
    /// </summary>
    public void UpdateHpBar()
    {
        hpBar.Set(hp.currentVal, hp.maxVal);
    }

    /// <summary>
    /// This method is used to update the stamina bar
    /// </summary>
    public void UpdateStaminaBar()
    {
        staminaBar.Set(stamina.currentVal, stamina.maxVal);
    }
    /// <summary>
    /// This method is used to take damage
    /// </summary>
    /// <param name="amount"> amount of damage</param>
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

    /// <summary>
    /// This method is method to setup the game over
    /// </summary>
    public void GameOver()
    {
        deathScreenPnl.Setup();

        hpBar.gameObject.SetActive(false);
        staminaBar.gameObject.SetActive(false);
        toolBarPanel.gameObject.SetActive(false);

        gameObject.SetActive(false);
        Time.timeScale = 0;
    }

    /// <summary>
    /// This method is used to freeze the game after a delay
    /// </summary>
    IEnumerator FreezeGameAfterDelay(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);

        Time.timeScale = 0;
        gameObject.SetActive(false);
    }

    /// <summary>
    /// This method is used to heal the player
    /// </summary>
    /// <param name="amount"> amount of heal</param>
    public void Heal(float amount)
    {
        hp.Add(amount);
        UpdateHpBar();
    }
    /// <summary>
    /// This method is used to fully heal the player
    /// </summary>
    public void FullHeal()
    {
        hp.SetToMax();
        UpdateHpBar();
    }

    /// <summary>
    /// This method is to make the player tired.
    /// </summary>
    /// <param name="amount"> amount of stamina stat</param>
    public void GetTired(float amount)
    {
        stamina.Substract(amount);
        if (stamina.currentVal <= 0)
        {
            isExhausted = true;
        }
        UpdateStaminaBar();
    }

    /// <summary>
    /// This method is used to let the player rest
    /// </summary>
    /// <param name="amount"> amount of stamina added</param>
    public void Rest(float amount)
    {
        stamina.Add(amount);
        UpdateStaminaBar();
    }

    /// <summary>
    /// This method is used to fully rest the player
    /// </summary>
    public void FullRest()
    {
        stamina.SetToMax();
        UpdateStaminaBar();
    }

    /// <summary>
    /// This method is used to make the player invulnerable
    /// </summary>
    /// <param name="duration"> duration of invulnerability</param>
    /// <returns></returns>
    public IEnumerator Invulnerability(float duration)
    {
        isInvulnerable = true; 
        yield return new WaitForSeconds(duration);
        isInvulnerable = false;
    }
}
