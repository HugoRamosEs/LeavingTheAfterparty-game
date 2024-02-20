using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health;
    public float speed;

    public Animator anim;
    public GameObject bloodEffect;

    void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("isRunning", true);
    }

    void Update()
    {
        if (health <= 0) {
            Destroy(gameObject);
        }

        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }

    //los metodos start y void son del video [el if health <= 0 hay que ponerlo si o si, para que cuando la vida llegue a 0, se destruya]

    // Este metodo se tiene que poner en el script de enemigos que hagamos, de esta manera 
    // el personaje podrá quitarle vida al enemigo
    // de la misma manera, debera tener el atributo de bloodEffect para ponerle la sangre
    public void TakeDamage(int damage)
    {
        Instantiate(bloodEffect, transform.position, Quaternion.identity);
        health -= damage;
        Debug.Log("Le he pegado!");
    }
}
