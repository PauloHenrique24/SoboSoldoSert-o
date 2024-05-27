using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    [Header("Gun Obj")]
    public GameObject objGun;

    public static GunInHand itemHand;

    [Header("Gun Root")]
    public GameObject gunRoot;
    public Animator animHandgun;

    [Header("Effect Hit")]
    public GameObject hit_Txt;

    void Start(){
        itemHand = new GunInHand();
    }

    void Update(){
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = Camera.main.transform.position.z;
        Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        Vector2 direction = worldMousePosition - gunRoot.transform.position; 

        float angle;

        if(FindFirstObjectByType<PlayerController>().isFacing)
            angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        else 
            angle = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;

        angle = Mathf.Clamp(angle,-60,60);

        var ang = Quaternion.AngleAxis(angle, Vector3.forward);

        gunRoot.transform.rotation = ang;

    }

    public void GunEnable(){objGun.SetActive(true); itemHand.InHand = true;}
    public void GunDesable(){objGun.SetActive(false); itemHand.InHand = false;}

    public void GunActive(Sprite spr, dragItem item){
        objGun.GetComponent<SpriteRenderer>().sprite = spr;
        itemHand.itemInHand = item;

        objGun.GetComponent<Animator>().runtimeAnimatorController = item.item.anim;

        FindFirstObjectByType<PlayerController>().targetBullet.localPosition = item.item.posTargetBullet;

        animHandgun.SetTrigger("Start");
        GunEnable();
    }

    public void Recharge() => objGun.GetComponent<Animator>().SetTrigger("R");
    
    public void Atk() => objGun.GetComponent<Animator>().SetTrigger("Atk");

    public void ShootAnim() => objGun.GetComponent<Animator>().SetBool("S",true);
    public void ShootAnimStop() => objGun.GetComponent<Animator>().SetBool("S",false);
}

[System.Serializable]
public class GunInHand{
    public bool InHand;
    public dragItem itemInHand;
}
