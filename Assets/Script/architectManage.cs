using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class architectManage : MonoBehaviour
{
   
    public static architectManage instance;
    public Transform tempGame;//存储上一物体的变量
    public Transform onceGame;
    public bool once;
    public void Awake()
    {
        instance=this;
    }
    // Start is called before the first frame update
    
    // Update is called once per frame
    public void closeOrOpenChilde(GameObject target,bool active)
    {
        for(int i = 0; i < target.transform.childCount; i++)
        {
            target.transform.GetChild(i).gameObject.SetActive(active);
        }
    }
    
}
