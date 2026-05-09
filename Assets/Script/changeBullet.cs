using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class changeBullet : MonoBehaviour
{
    public float CurrentBeltClipAmount;//当前腰间弹夹的子弹数量
    public float Countdown;
    public bool BeletHaveClip;
    public Text BeltText;
    public GameObject Temp;
    public Text desText;
    //5是弹夹的更换物体

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (BeletHaveClip)
        {
            if (CurrentBeltClipAmount < 10)//weapon.instance.MaxBullet
            {
                Countdown += Time.deltaTime;
                if (Countdown >= 1f)
                {
                    Countdown = 0;
                    CurrentBeltClipAmount += 1;
                }
            }
        }
        BeltText.text = "腰间子弹" + CurrentBeltClipAmount;
    }

   
    private void OnTriggerStay(Collider other)
    {
            if (other.CompareTag("hand"))//要加是否有弹夹的判断
            {
                ray.instance.GameShader(gameObject, 0);//碰撞手 弹夹高光
            if (ray.instance.isChangeShader)
            {
                Temp=other.gameObject;
            }
                if (Input.GetKeyDown(KeyCode.G)&&BulletTransfer.instance.isHaveClip)//如果按下g且手里有弹夹
                {
                    changeHandAndBelt();
                //ray.instance.GameItem(other.gameObject, 0);//物体状态激活 照射到的状态隐藏
                //BulletTransfer.instance.TransferBullet = CurrentBeltClipAmount; //腰间子弹进行更换
                desText.text = "交换成功";
                Invoke("clearText", 2);
                }
                else if (Input.GetKeyDown(KeyCode.G))//如果手上没有的弹夹且按下g键的话
                {
                    ray.instance.GameItem(gameObject, 0);//手上物体状态激活 腰间的子弹的状态隐藏
                    BeletHaveClip = false;//腰间没有弹夹了
                    OpenOrCloseActive(BulletTransfer.instance.isHaveClip);//则隐藏该物体
                    BulletTransfer.instance.isHaveClip = true;//手上有了弹夹
                    changeHandAndBelt();//转移转加子弹
            }
            }
    }
    public void clearText()
    {
        desText.text = "    ";
    }
    private void OnTriggerExit(Collider other)
    {
        if (ray.instance.isChangeShader)//如果改变了渲染
        {
            if (other.tag == Temp.tag&&Temp!=null&&other.tag!=null)//且标签一样
            {
                ray.instance.ResetShader(gameObject,0);
            }
        }
    }
    public void changeHandAndBelt()//交换手上的弹夹和腰间的子弹的逻辑 在手上有枪上转移而来的子弹的情况下
    {
        float temp = 0;
        temp = BulletTransfer.instance.TransferBullet;//记录下转移子弹的变量
        BulletTransfer.instance.TransferBullet = CurrentBeltClipAmount; //腰间子弹传到转移站
        CurrentBeltClipAmount = temp;//手里子弹的变量传入到了腰间里面的了
    }
    public void OpenOrCloseActive(bool active)//手上有弹夹的话 交换的时候这物体不隐藏 没有则隐藏
    {
        gameObject.transform.GetChild(0).gameObject.SetActive(active);//则把弹夹打开或隐藏
    }
}
