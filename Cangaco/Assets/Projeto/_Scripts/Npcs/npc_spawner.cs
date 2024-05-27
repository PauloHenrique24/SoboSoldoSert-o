using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class npc_spawner : MonoBehaviour
{
    public GameObject npcPrefab; 
    public int numNPCs = 5; 
    public float spacing = 2f; 
    public Vector3 direction = Vector3.forward;
    void Start()
    {
        spawnnpcs();
    }

    // Update is called once per frame
    void spawnnpcs()
    {
        for (int i = 0; i < numNPCs; i++)
        {
            Vector3 spawnPosition = transform.position + (direction * spacing * i);

            GameObject gameObject1 = Instantiate(npcPrefab, spawnPosition, Quaternion.identity);
            GameObject npc = gameObject1;

            npc.GetComponent<npcsgroup>().moveDirection = direction;
        }
    }
}
