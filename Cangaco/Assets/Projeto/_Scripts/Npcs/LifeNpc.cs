using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LifeNpc : MonoBehaviour
{
    public TextMeshProUGUI txt_name;
    public Image img_life;

    [HideInInspector]
    public Transform parent;

    public bool isMov;

    void Start() => StartCoroutine(StartAnim());

    IEnumerator StartAnim(){
        yield return new WaitForSeconds(1f);
        isMov = true;
    }

    void Update()
    {
        if(isMov){
            var pos = new Vector3(parent.position.x,parent.position.y + .2f);
            Vector3 movePosition = Vector3.MoveTowards(transform.position, pos, Time.deltaTime * 5);
            
            transform.position = movePosition;


            if(Vector3.Distance(transform.position,pos) < .01f){
                parent.GetComponent<SimpleNpc_Controller>().lifeCanva = gameObject;

                transform.SetParent(parent);
                isMov = false;
            }
        }
    }
}
