using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamage
{
    public void Damage(float damage);
    public void Hit(GameObject bullet);
}
