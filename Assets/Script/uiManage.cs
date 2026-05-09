using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class uiManage : MonoBehaviour
{
    public CanvasGroup architectBG;//建筑的canvas组
    public float speed;
    public float architectBGTarget;
    public GameObject floor;
    public GameObject wall;
    public GameObject stair;
    public GameObject door;
    public static uiManage instance;
    public void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (architectBG.alpha != architectBGTarget)
        {
            architectBG.alpha=Mathf.Lerp(architectBG.alpha, architectBGTarget, speed * Time.deltaTime);
          
        }
        if (Mathf.Abs(architectBG.alpha - architectBGTarget) < 0.1f)
        {
            architectBG.alpha = architectBGTarget;
            
        }
        activeMune();
    }
    public void activeMune()//唤醒建筑菜单
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            architectBGTarget = 1;
            wall.SetActive(true);
            floor.SetActive(true);
            stair.SetActive(true);
            door.SetActive(true);
        }
    }
}
