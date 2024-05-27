using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pointer : MonoBehaviour
{
    public GameObject pointer;

    public void EnablePointer() => pointer.SetActive(true);
    public void DesablePointer() => pointer.SetActive(false);

    void Update(){
        pointer.transform.position = Input.mousePosition;
    }
}
