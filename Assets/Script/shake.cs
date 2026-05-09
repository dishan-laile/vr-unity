using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using Unity.VisualScripting;
using UnityEngine;


public class shake : MonoBehaviour
{
    private bool final;
    private bool StartShake;
    public float record;
    private bool rightShake;
    private bool right;
    private bool left;
    private bool both;
    public GameObject ciname;
    private bool leftShake;
    public GameObject Pos;//原位置
    private float timer;
    private float leftTimer;
    public float circulateTime;//计算时间
    private float currentY;
    private float currentZ;
    public float lerpSpeed;//渐变速度
    public float recordSpeed;//记录下速度
    public float rightLerpY;
    public float rightLerpZ;
    public float leftLerpY;
    public float leftLerpZ;
    private bool isCorrent;//判断是否是回正的

    // Start is called before the first frame update
    void Start()
    {
        final = true;
        recordSpeed = lerpSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        float ver = Input.GetAxis("Vertical");
        float hon = Input.GetAxis("Horizontal");
        if (final == true)
        {
            StartCoroutine(check());
            final = false;
        }
        if (!StartShake)//如果结束了晃动 根据记录下的record值来判断是否晃动了和晃动的大小 
        {
            if (record != 0)
            {

                //ShakeAngle(record,false);//将值传进去 准备进行晃动 存下晃动的值且还没有结束
                record = 0;//把值进行清零
            }

        }
        //CorrectNum();
        shakeWeaPon();
    }
    public void shakeWeaPon()
    {

        if (rightShake)
        {
            if (!right)
            {
                currentY = transform.localEulerAngles.y;
                currentZ = transform.localEulerAngles.z;
                right = true;
                left= false;
                both = false;
                lerpSpeed = recordSpeed;
                isCorrent = true;
            }
            
            currentY = Mathf.Lerp(currentY, rightLerpY, lerpSpeed * Time.deltaTime);
            currentZ = Mathf.Lerp(currentZ, rightLerpZ, lerpSpeed * Time.deltaTime);
            Vector3 eluer=new Vector3 (transform.localEulerAngles.x,currentY, currentZ);
            transform.localEulerAngles = eluer;
        }
        if (leftShake)
        {
            if (!left)
            {
                currentY = transform.localEulerAngles.y;
                currentZ = transform.localEulerAngles.z;
                right = false;
                left = true;
                both = false;
                lerpSpeed = recordSpeed;
                isCorrent = true;
            }
            
            currentY = Mathf.Lerp(currentY, leftLerpY, lerpSpeed * Time.deltaTime);
            currentZ = Mathf.Lerp(currentZ, leftLerpZ, lerpSpeed * Time.deltaTime);
            Vector3 eluer = new Vector3(transform.localEulerAngles.x, currentY, currentZ);
            transform.localEulerAngles = eluer;
        }
        if (!rightShake && !leftShake)
        {
            if (!both)
            {
                currentY = transform.localEulerAngles.y;
                currentZ = transform.localEulerAngles.z;
                both = true;
                right = false;
                left = false;
                
            }
            if (isCorrent)
            {
                lerpSpeed += 0.01f;
            }
            currentY = Mathf.Lerp(currentY, 3, lerpSpeed * Time.deltaTime);
            currentZ = Mathf.Lerp(currentZ, 3, lerpSpeed * Time.deltaTime);
            Vector3 eluer = new Vector3(transform.localEulerAngles.x, currentY, currentZ);
            transform.localEulerAngles = eluer;
            
        }
    }
    //public void CorrectNum()//回正数值
    //{
    //    float re;
    //    List<float> floats = new List<float> { backleftSpeedY, BackLeftSpeedz, ReleftShakey, ReLefyShakeZ };
    //    for (int i = 0; i < floats.Count; i++)
    //    {
    //        re = floats[i];
    //        if (re < 0 && i <= 1)
    //        {
    //            floats[i] = floats[i + 2];
    //        }
    //    }
    //}
    //public void checkShake()//检测物体的晃动
    //{
    //    Vector3 vector3 = Pos.transform.TransformPoint(Pos.transform.localPosition);
    //    Vector3 temp = vector3;
    //}
    public IEnumerator check()//判断武器是否晃动 且记录值的
    {
        //Vector3 vector3 = Pos.transform.TransformPoint(Pos.transform.localPosition);
        //yield return new WaitForSeconds(0.5f);
        //Vector3 temp = Pos.transform.TransformPoint(Pos.transform.localPosition);
        Vector3 vector3 = Pos.transform.position;
        yield return new WaitForSeconds(circulateTime);
        Vector3 temp = Pos.transform.position;
        float angle = rightAngle(ciname.transform.localEulerAngles.y);
        //Debug.Log(angle + "摄像机的y");
        //Debug.Log(ciname.transform.localEulerAngles.y);
        if (angle < 180 && angle > 0)//在正轴
        { //右正 z减
          //if(temp.y - vector3.y > 0)
          //{
          //    ShakeWeapon(temp.z - vector3.z);//将晃动的值传进来
          //    ProcessShake(true);//过程的晃动 开始过程晃动
          //    rightShake = false;
          //    leftShake = false;
          //    UpShake=true;
          //    Debug.Log("在往上");
          //}
          //else
          //{
            if (temp.z - vector3.z < -0.01)
            {
                while (timer < circulateTime)
                {
                    timer += Time.deltaTime;
                    leftTimer = 0;
                    ShakeWeapon(temp.z - vector3.z);
                    rightShake = true;
                    leftShake = false;
                    Debug.Log("在往右");
                }
                
            }
            if (temp.z - vector3.z > -0.01 && temp.z - vector3.z < 0.01)//结束了晃动
            {
                timer = 0;
                leftTimer = 0;
                ceaseShake();
                leftShake=false;
                rightShake = false;
                //ProcessShake(false);//过程的晃动 结束
            }
            if (temp.z - vector3.z > 0.01)
            {
                while (leftTimer < circulateTime)
                {
                    leftTimer += Time.deltaTime;
                    leftShake = true;
                    rightShake = false;
                    Debug.Log("在往左");
                }
            }
            //}

        }

        if (angle > -180 && angle < 0)//在负轴
        {
            if (temp.y - vector3.y > 0)
            {
               
                ShakeWeapon(temp.z - vector3.z);//将晃动的值传进来
                //ProcessShake(true);//过程的晃动 开始过程晃动
                rightShake = false;
                leftShake = false;
                //UpShake = true;
                Debug.Log("在往上");
            }
            else
            {
                if (temp.z - vector3.z > 0.01)
                {
                    while (timer < circulateTime)
                    {
                        timer += Time.deltaTime;
                        leftTimer = 0;
                        rightShake = true;
                        leftShake = false;
                        ShakeWeapon(temp.z - vector3.z);//将晃动的值传进来
                                                        //ProcessShake(true);//过程的晃动 开始过程晃动
                        Debug.Log("在往右");

                    }
                }
                if (temp.z - vector3.z > -0.01 && temp.z - vector3.z < 0.01)//结束了晃动
                {
                    ceaseShake();
                    rightShake = false;
                    leftShake = false;
                    //ProcessShake(false);//过程的晃动 结束
                }
                if (temp.z - vector3.z < -0.01)
                {
                    while (leftTimer < circulateTime)
                    {
                        timer = 0;
                        leftTimer += Time.deltaTime;
                        leftShake = true;
                        rightShake = false;
                        ShakeWeapon(temp.z - vector3.z);
                        //ProcessShake(true);//开启晃动
                        Debug.Log("在往左");
                        
                    }
                    
                }
            }

        }
        final = true;
    }
    public void ShakeWeapon(float valua)//记录武器晃动值逻辑
    {
        record += valua;//记录下晃动总值
        StartShake = true;
    }
    public void ceaseShake()//结束晃动
    {
        StartShake = false;//结束晃动
    }
    public float rightAngle(float valua)//修正角度
    {
        float angle = valua - 180;
        if (angle < 0)
        {
            return valua;
        }
        if (angle > 0)
        {
            return -(valua - 180);
        }
        return angle;
    }
}
