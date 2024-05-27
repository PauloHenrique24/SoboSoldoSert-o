using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class npc_ataque : MonoBehaviour, IDamage
{
    public float movespeed = 3f;
    public int maxHealth = 100;
    public float currentHealth; // vida atual 
    public GameObject projectilePrefab;
    public Transform firepoint;
    public float shootingRange = 5f;
    public float fireRate = 1f;

    private Transform player;
    private float nextFiretime;

    // Start is called before the first frame update
    private void Start()
    {
        currentHealth = maxHealth;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        float distanceToplayer = Vector3.Distance(transform.position, player.position);
        if(distanceToplayer < 4f){

            transform.position = Vector3.MoveTowards(transform.position,player.position,movespeed * Time.deltaTime);

            if (distanceToplayer <= shootingRange)
            {
                if (Time.time >= nextFiretime)
                {
                    Shoot();
                    nextFiretime = Time.time + 1f / fireRate;
                }
            }
        }
    }
     
    void Shoot()
    {
        Instantiate(projectilePrefab, firepoint.position, Quaternion.identity);
    }

    public void Damage(float damage) => TakeDamage(damage);

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0) Die();
    }


    void Die() => Destroy(gameObject);

    public void Hit(GameObject bullet){}
}
