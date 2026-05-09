using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eventManage : MonoBehaviour
{
    public GameObject wall;
    public GameObject floor;
    public static eventManage instance;
    public GameObject stair;
    public GameObject door;
    private bool once;
    private void Awake()
    {
        instance = this;
    }
    public void floorEvent()
    {
        rightRay.instance.creatWall=true;
        rightRay.instance.achitectItem=floor;
        uiManage.instance.architectBGTarget = 0; 
        uiManage.instance.wall.SetActive(false);
        uiManage.instance.floor.SetActive(false);
        rightRay.instance.index = 1;//分配下标进行判断
        uiManage.instance.stair.SetActive(false);
        uiManage.instance.door.SetActive(false);
        if (!once)//判断是否是第一次使用 进行存储下标
        {
            rightRay.instance.temp = 1;
            once = true;
        }
        Debug.Log("1111");
    }
    public void wallEvent()
    {
        rightRay.instance.creatWall = true;
        rightRay.instance.achitectItem = wall;
        uiManage.instance.architectBGTarget = 0;
        uiManage.instance.wall.SetActive(false);
        uiManage.instance.floor.SetActive(false);
        rightRay.instance.index = 2;//分配下标进行判断
        uiManage.instance.stair.SetActive(false);
        uiManage.instance.door.SetActive(false);
        if (!once)//判断是否是第一次使用 进行存储下标
        {
            rightRay.instance.temp = 2;
            once = true;
        }
    }
    public void stairEvent()
    {
        rightRay.instance.creatWall = true;
        rightRay.instance.achitectItem = stair;
        uiManage.instance.architectBGTarget = 0;
        rightRay.instance.index = 3;//分配下标进行判断
        uiManage.instance.wall.SetActive(false);
        uiManage.instance.floor.SetActive(false);
        uiManage.instance.stair.SetActive(false);
        uiManage.instance.door.SetActive(false);
    }
    public void doorEvent()
    {
        rightRay.instance.creatWall = true;
        rightRay.instance.achitectItem = door;
        uiManage.instance.architectBGTarget = 0;
        uiManage.instance.wall.SetActive(false);
        uiManage.instance.floor.SetActive(false);
        uiManage.instance.stair.SetActive(false);
        uiManage.instance.door.SetActive(false);
    }
}
