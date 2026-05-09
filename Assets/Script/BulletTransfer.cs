using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTransfer : MonoBehaviour
{
    public float TransferBullet;
    public static BulletTransfer instance;
    public bool GunBellet;//判断是否从枪械上获得的子弹
    public bool changeBullet;//判断是否从腰间获得的子弹
    public bool isHaveClip;//判断手上是否有弹夹
    private void Awake()
    {
        instance = this;
    }
    private void Update()
    {
       
    }
}
