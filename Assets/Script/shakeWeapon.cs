using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;

public class shakeWeapon : MonoBehaviour
{
    public GameObject weapon;
    public GameObject Pos;//计算开始晃动的位置
    public GameObject ciname;
    public GameObject start;
    public GameObject Gun;
    public float shakeSpeed;
    public float record;//记录下晃动的大小
    private float ShakeValua;//这是将要晃动的valua值
    public bool StartShake;
    public bool final;
    public float processValua;//移动过程中的旋转值
    public float processValuaY;//移动的y的目标值
    private float RecordProcessGun;//记录下枪原有的z值
    private float RecordRecordGunY;
    private float lerpValua;//记录下渐变的过程中valua值
    public float LerpSpeed;
    public float EndShakeSpeed;//结束的时候的晃动速度
    private bool CorrectOnce;//右矫正位置
    private bool Correcttwice;//左矫正 
    float ProceesZ;//过程中的z值
    float ProcessY;
    float ProcessX;
    private bool rightShake;
    private bool leftShake;
    public  float leftShakeY;
    private float ReleftShakey;
    public float leftShakeZ;
    private float ReLefyShakeZ;
    public float BackLeftSpeedz;//往左晃回去渐变
    public float backleftSpeedY;
    public float lerpLeftSpeedZ;
    public float lerpLeftSpeedY;
    public float UpTargetValua;
    public float DownTargetValua;
    private bool UpShake;
    private bool thirdShake;
    private float RecordProcessX;
    public float circulateTime;
    private float timer;//这是右手的计时器
    private float timerleft;
    private bool once;
    private void Start()
    {
        RecordProcessGun = Gun.transform.localRotation.z;//获得武器原来的z值
        RecordRecordGunY = Gun.transform.localRotation.y;//获得武器原来的y值
        RecordProcessX = Gun.transform.localRotation.x;
        ReleftShakey = backleftSpeedY;//记录最大渐变值
        ReLefyShakeZ = BackLeftSpeedz;
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
            if (record !=0)
            {
                
                //ShakeAngle(record,false);//将值传进去 准备进行晃动 存下晃动的值且还没有结束
                record = 0;//把值进行清零
            }
            
        }
        CorrectNum();
        shakeWeaPon();
    }
    public void shakeWeaPon()
    {
        
        if (rightShake)
        {
            if (!once)
            {
                //re=transform.localPosition.y;
                //float z=transform.localPosition.z;
                once = true;
            }
             //=Mathf.Lerp()
        }
        if (leftShake)
        {

        }
    }
    public void CorrectNum()//回正数值
    {
        float re;
        List<float> floats=new List<float> {backleftSpeedY,BackLeftSpeedz,ReleftShakey,ReLefyShakeZ };
        for(int i = 0; i < floats.Count; i++)
        {
            re = floats[i];
            if (re < 0&&i<=1)
            {
                floats[i] = floats[i+2];
            }
        }
    }
    public void checkShake()//检测物体的晃动
    {
        Vector3 vector3 = Pos.transform.TransformPoint(Pos.transform.localPosition);
        Vector3 temp = vector3;
    }
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
        if (angle < 180&&angle >0)//在正轴
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
                
                    
                    ShakeWeapon(temp.z - vector3.z);//将晃动的值传进来
                    //ProcessShake(true);//过程的晃动 开始过程晃动
                    rightShake = true;
                    leftShake = false;
                    Debug.Log("在往右");
              
                    
                }
                if (temp.z - vector3.z > -0.01 && temp.z - vector3.z < 0.01)//结束了晃动
                {
                timer = 0;
                    ceaseShake();
                    //ProcessShake(false);//过程的晃动 结束
                }
                if (temp.z - vector3.z > 0.01)
                {
                
                    leftShake = true;
                    rightShake = false;
                    Debug.Log("在往左");
               
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
                UpShake = true;
                Debug.Log("在往上");
            }
            else
            {
                if (temp.z - vector3.z > 0.01)
                {
                   
                        rightShake = true;
                        leftShake = false;
                        ShakeWeapon(temp.z - vector3.z);//将晃动的值传进来
                        //ProcessShake(true);//过程的晃动 开始过程晃动
                        Debug.Log("在往右");
                    
                    
                }
                if (temp.z - vector3.z > -0.01 && temp.z - vector3.z < 0.01)//结束了晃动
                {
                    ceaseShake();
                    //ProcessShake(false);//过程的晃动 结束
                }
                if (temp.z - vector3.z < -0.01)
                {
                   
                        leftShake = true;
                        rightShake = false;
                        ShakeWeapon(temp.z - vector3.z);
                        //ProcessShake(true);//开启晃动
                        Debug.Log("在往左");
                  
                   
                }
            }
            
        }
        final = true;
    }
    public void ShakeWeapon(float valua)//记录武器晃动值逻辑
    {
        record += valua;//记录下晃动总值
        StartShake=true;
    }
    public void ceaseShake()//结束晃动
    {
        StartShake = false;//结束晃动
    }
    public float rightAngle(float valua)//修正角度
    {
        float angle=valua - 180;
        if (angle < 0)
        {
            return valua;
        }
        if (angle > 0)
        {
            return -(valua-180);
        }
        return angle;
    }
    
    
    //public void ProcessShake(bool end)//过程的偏移 
    //{
    //    if (end)
    //    {
    //        if (rightShake)//如果是右手的晃动
    //        {
    //            while (timer < circulateTime)
    //            {
    //                timerleft = 0;
    //                timer += Time.deltaTime;
    //                ProceesZ = MathLerp(ProceesZ, processValua);//向下渐变的目标值 这两个值都是负值
    //                ProcessY = MathLerpY(ProcessY, processValuaY);
    //                Vector3 Eluer = new Vector3(transform.localRotation.x, ProcessY, ProceesZ);
    //                Gun.transform.localRotation = Quaternion.Euler(Eluer);
    //                CorrectOnce = true;//矫正一次
    //            }
                
    //        }
    //        if (leftShake)
    //        {
    //            while (timerleft < circulateTime)
    //            {
    //                timerleft += Time.deltaTime;
    //                timer = 0;
    //                //leftShakeZ//leftShakeY 目标值
    //                ProceesZ = MathLerpLeft(ProceesZ, leftShakeZ);//向下渐变的目标值 这两个值都是负值
    //                ProcessY = MathLerpLeftY(ProcessY, leftShakeY + 360);//这是修改了bug
    //                Vector3 Eluer = new Vector3(transform.localRotation.x, ProcessY, ProceesZ);
    //                Gun.transform.localRotation = Quaternion.Euler(Eluer);
    //                Correcttwice = true;
    //            }
                
    //        }
    //        if (UpShake)//如果是往上的
    //        {
    //            Vector3 Eluer = new Vector3(transform.localRotation.x, ProcessY, ProceesZ);
    //            Gun.transform.localRotation = Quaternion.Euler(Eluer);
    //            CorrectOnce = true;
    //        }
    //        //这是往右的过程晃动

    //    }
    //    else //复原z值
    //    {
    //        //这是往右的 结束是武器摆动的逻辑
    //        //lerpValua = 0;//将值进行清零
    //        if (CorrectOnce&&rightShake)
    //        {
    //            Debug.Log("jji");
    //            ProceesZ = CorrectLerp(ProceesZ, RecordProcessGun);//往回矫正
    //            ProcessY = CorrectLerp(ProcessY, RecordRecordGunY);
    //            Vector3 Eluer = new Vector3(transform.localRotation.x, ProcessY, ProceesZ);
    //            Gun.transform.localRotation = Quaternion.Euler(Eluer);
    //        }
    //        if (Correcttwice && leftShake)
    //        {//leftShakeZ//leftShakeY 目标值
    //            Debug.Log("ji");
    //            ProceesZ = CorrectLerpLeftZ(ProceesZ, RecordProcessGun);//往回矫正
    //            ProcessY = CorrectLerpLeftY(ProcessY, RecordRecordGunY);
    //            Vector3 Eluer = new Vector3(transform.localRotation.x, ProcessY, ProceesZ);
    //            Gun.transform.localRotation = Quaternion.Euler(Eluer);
    //        }
    //        if (thirdShake && UpShake)
    //        {

    //        }
    //    }
    //}
    //public float CorrectLerpLeftY(float original, float valua)//这是左晃往回渐变的过程的逻辑 这是需要减y值的
    //{//负值减正值
    //    if (Mathf.Abs(original - valua) < 0.3f || original < valua)//这是个死代码 出问题找这
    //    {
    //        original = valua;//直接
    //        return original;
    //    }
    //    original = Mathf.SmoothDamp(original, valua, ref backleftSpeedY, 0.3f);
    //    Debug.Log(original);
    //    return original;
    //}
    //public float CorrectLerpLeftZ(float original, float valua)//这是左晃往回渐变的过程的逻辑 这是需要减z值的
    //{//负值减正值
    //    if (Mathf.Abs(original - valua) < 0.3f || original < valua)//这是个死代码 出问题找这
    //    {
    //        original = valua;//直接
    //        Debug.Log(original);
    //        return original;
    //    }
    //    original = Mathf.SmoothDamp(original, valua, ref BackLeftSpeedz, 0.5f);
    //    //Debug.Log(original);
    //    //Debug.Log(original);
    //    return original;
    //}
    
    //public float MathLerpLeftY(float original, float valua)//y往左的渐变方法
    //{
    //    Debug.Log(original+" "+valua);
    //    Debug.Log(Mathf.Abs(original + 360 + valua)+"相减");
    //    if (Mathf.Abs( valua-original) < 0.3f)
    //    {
    //        Debug.Log("1");
    //        original = valua;
    //        return original;
    //    }
    //    original = Mathf.SmoothDamp(original, valua, ref lerpLeftSpeedY, 0.8f);
    //    return original;
    //}
    //public float MathLerpLeft(float original, float valua)//z往左的渐变方法
    //{
        
    //    if (Mathf.Abs(valua - original) < 0.3f||original>valua)
    //    {
           
    //        original = valua;
    //        return original;
    //    }
    //    original = Mathf.SmoothDamp(original, valua,ref lerpLeftSpeedZ, 0.5f);
    //    return original;
    //}
    
    //public float MathLerpY(float original, float valua)//y往右的渐变方法
    //{

    //    if (Mathf.Abs(ProceesZ + (-360) - valua) < 0.3f)
    //    {
          
    //        ProceesZ = valua;
    //        CorrectOnce = true;
    //        rightShake = false;//右手震动完成
    //        return ProceesZ;
    //    }
    //    float speed = 15;
    //    ProceesZ = Mathf.SmoothDamp(original, valua, ref speed, 0.8f);
    //    return ProceesZ;
    //}
    
    
    //public float MathLerpYLeft(float original, float valua)//y往左的渐变方法
    //{
    //    if (Mathf.Abs(original + (-360) - valua) < 0.3f|| ProcessY + (-360) > valua)
    //    {
    //        Debug.Log("1");
    //        original = valua;
    //        CorrectOnce = true;
    //        rightShake = false;//右手震动完成
    //        return ProcessY;
    //    }
    //    float speed = 15;
    //    original = Mathf.SmoothDamp(original, valua, ref speed, 0.8f);
    //    return original;
    //}
   
    //public float MathLerp(float original, float valua)//z往右的渐变方法
    //{
    //    Debug.Log("wanyou");
    //    if (Mathf.Abs(valua - original) < 0.3f)
    //    {
    //        original = valua;
    //        return original;
    //    }
    //    original = Mathf.Lerp(original, valua, LerpSpeed);
    //    Debug.Log(original);
    //    return original;
    //}
    ////public float CorrectLerpLeftY(float original, float valua)//这是左晃往回渐变的过程的逻辑 这是需要减y值的
    ////{//负值减正值
    ////    if (Mathf.Abs(original - valua) < 0.3f || original < valua)//这是个死代码 出问题找这
    ////    {
    ////        original = valua;//直接
    ////        return original;
    ////    }
    ////    float speed = -5;
    ////    original = Mathf.SmoothDamp(original, valua, ref speed, 0.3f);
    ////    Debug.Log(original);
    ////    return original;
    ////}
   
    //public float LeftMathLerp(float original, float valua)//z往左的渐变方法
    //{//z要变大
    //    if (Mathf.Abs(valua - ProceesZ) < 0.3f)
    //    {
    //        ProceesZ = valua;
    //        return ProceesZ;
    //    }
    //    float speed=10;
    //    ProcessY = Mathf.SmoothDamp(ProcessY, valua, ref speed, 0.8f);
    //    return ProcessY;
    //}
    
    //public float CorrectLerp(float original, float valua)//这是右晃往回渐变的过程的逻辑 这是需要加z值的
    //{//负值减正值
    //    if (Mathf.Abs(valua - lerpValua) <0.15f||lerpValua>RecordProcessGun)//这是个死代码 出问题找这
    //    {
    //        lerpValua = valua;//直接
    //        return lerpValua;
    //    }
    //    //lerpValua += Time.deltaTime * backLerpSpeed;
    //    //lerpValua = Mathf.Lerp(lerpValua, valua, 0.3f);
    //    float speed=13;
    //    lerpValua = Mathf.SmoothDamp(lerpValua, valua, ref speed, 0.4f);
    //    return lerpValua;
    //}
    
    ////public void ShakeAngle(float valua,bool avtive)//需要晃动的值 和是否结束了的bool 值 
    ////{
    ////    ShakeValua += valua;//将晃动的值记录下来 
    ////    Debug.Log(ShakeValua);
    ////    if (ShakeValua != 0 && avtive)//不够平滑
    ////    {
    ////        Gun.transform.Rotate(Vector3.up, ShakeValua * Time.deltaTime * EndShakeSpeed);
    ////        Debug.Log(ShakeValua * Time.deltaTime * EndShakeSpeed);
    ////        Debug.Log("开始结束的逻辑");
    ////        ShakeValua = 0;//将值进行清理
    ////    }
    ////}
}
