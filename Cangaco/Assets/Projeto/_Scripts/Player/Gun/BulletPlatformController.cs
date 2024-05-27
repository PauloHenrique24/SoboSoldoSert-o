using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPlatformController : MonoBehaviour
{   
    [HideInInspector] public Vector3 direct;
    public float speed;

    void Start(){
        Destroy(gameObject,4f);
    }
    
    void Update()
    {
        transform.position += direct * speed * Time.deltaTime;
    }
}
