using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    public bool isright;//是否是右边的门
    
    public static OpenDoor instance;
    public void Awake()
    {
        instance = this;
    }

    private void OnCollisionEnter(Collision collision)
    {
        
        Debug.Log(collision.transform.name);
        if (collision.transform.CompareTag("rightHand") || collision.transform.CompareTag("hand")||collision.transform.CompareTag("Player"))
        {
            //for (int i = 0; i < transform.parent.childCount; i++)
            //{
            //    if (gameObject == transform.parent.GetChild(0).gameObject)
            //    {
            //        index = i;
            //    }
            //}
            gameObject.GetComponent<BoxCollider>().enabled = false;
            Debug.Log("peng");
            if (isright == true)
            {
                if (transform.parent.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("DoorLeftStart"))
                {
                    transform.parent.GetComponent<Animator>().SetBool("endleft", true);
                }
                else
                {
                    transform.parent.SetParent(null);
                    transform.parent.GetComponent<Animator>().SetBool("startright", true);
                }
                    
            }
            else
            {
                if (transform.parent.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("doorRightStart"))
                {
                    transform.parent.GetComponent<Animator>().SetBool("endright", true);
                }
                else
                {
                    transform.parent.SetParent(null);
                    transform.parent.GetComponent<Animator>().SetBool("startleft", true);
                }
                   
            }
        }
    }

}
