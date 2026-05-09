using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimManage : MonoBehaviour
{
   
   
    public void changeBullet()
    {
        weapon.instance.bulletAmount = weapon.instance.MaxBullet;
    }
    public void doorIdle()
    {
        GetComponent<Animator>().SetBool("startright", false);
        GetComponent<Animator>().SetBool("endright", false);
        GetComponent<Animator>().SetBool("startleft", false);
        GetComponent<Animator>().SetBool("endright", false);
    }
    public void doorendright()
    {
        GetComponent<Animator>().SetBool("startleft", false);
    }
    public void doorendleft()
    {
        
        GetComponent<Animator>().SetBool("startright", false);
    }
    
    public void openDoorBox()
    {
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            gameObject.transform.GetChild(i).GetComponent<BoxCollider>().enabled = true;//进行查找是哪个子物体发生了碰撞
        }
        
    }
}
