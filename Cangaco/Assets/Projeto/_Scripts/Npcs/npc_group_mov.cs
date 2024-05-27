using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class npcsgroup : MonoBehaviour
{
    public float moveSpeed = 3f;
    internal Vector3 moveDirection;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    }
}
