using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Inventario/item", fileName = "Item Inv")]
public class ItemInv : ScriptableObject
{
    public string name_;
    public Sprite image_;
    public Sprite InvImage_;

    [Space]
    public bool can_group;
    public int max_Itens;

    [Space]
    public GameObject prefab;

    [Space]
    public typeItem type;

    [Header("Gun")]
    public GameObject bulletPref;

    [Space]
    public RuntimeAnimatorController anim;
    
    [Space]
    public Vector3 posTargetBullet;

    [Space]
    public typeMunition TypeMunition;

    [Space]
    public float fireRate;
    public float speedBullet;
    public float timerRecharge;

    [Space]
    public int qtd_Bullet;
    
    [Space]
    public float timer_Life;

    [Space]
    public int maxMunition;

    [Header("Gun White")]
    public float range;

    [Header("Damage")]
    public float damage;
}
