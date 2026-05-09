using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBellet : MonoBehaviour
{
    private GameObject Temp;
    public GameObject handBellet;
    // Start is called before the first frame update
    private void OnTriggerStay(Collider other)//这是枪上面的弹夹
    {
        if (other.CompareTag("hand"))
        {   
              ray.instance.GameShader(gameObject, 0);//碰撞手 弹夹高光
            if (ray.instance.isChangeShader)
            {
                Temp=other.gameObject;
            }
               if (Input.GetKeyDown(KeyCode.G)&&!BulletTransfer.instance.isHaveClip)//要手上没有弹夹才行
               {
                Debug.Log("走");
                ray.instance.GameItem(gameObject, 0);//手上弹夹显示 这枪上关闭
                BulletTransfer.instance.TransferBullet=weapon.instance.bulletAmount;//枪里面的子弹传到手上的弹夹
                BulletTransfer.instance.isHaveClip = true;//手上有了弹夹
                weapon.instance.bulletAmount=0;//弹夹被拿下了 所以发不了子弹了
               }
               else if (Input.GetKeyDown(KeyCode.G))//如果有弹夹 且按下g键
               {
                weapon.instance.bulletAmount = BulletTransfer.instance.TransferBullet;//传值到枪里面的子弹
                OpenOrCloseActive(BulletTransfer.instance.isHaveClip);//手上有弹夹的话 则该物体显示
                handBellet.SetActive(false);
                //BulletTransfer.instance.isHaveClip=false;//手上没有了弹夹
               }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (ray.instance.isChangeShader)//如果改变了渲染
        {
            if (other!=null&&Temp!=null)//且标签一样
            {
                if(other.tag == Temp.tag)
                {
                    ray.instance.ResetShader(gameObject, 0);
                }
              
            }
        }
    }
    public void OpenOrCloseActive(bool active)//手上有弹夹的话 交换的时候这物体不隐藏 没有则隐藏
    {
            gameObject.transform.GetChild(0).gameObject.SetActive(active);//则把弹夹打开或隐藏
    }
    //问题是手上有没有弹夹
    }
