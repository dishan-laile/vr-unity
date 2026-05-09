using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class adsorption : MonoBehaviour
{
    public float absorptionSize;//吸附大小
    public bool isGroud;
    public static adsorption instance;
    public Vector3 idleTransform;
    private float gap;
    public GameObject wallParent;
    public bool isFloor;//判断是否是地板
    public bool isWall;
    public bool isStair;
    public bool isDoor;
    public GameObject floorParent;
    public List<GameObject> right;
    public List<GameObject> left;
    public List<GameObject> foward;
    public List<GameObject> back;//这是floor的四个方向的数组
    private Transform temp;//之前的floor的位置
   
    private void Awake()
    {
        instance = this;
        
        
    }
    void Start()
    {
        idleTransform = rightRay.instance.rayPosition;
        gap = idleTransform.y - gameObject.transform.localScale.y / 2;
        wallParent = GameObject.Find("wallParent");
        floorParent = GameObject.Find("floorParent");
       
       
    }

    // Update is called once per frame
    void Update()
    {
        idleTransform = rightRay.instance.rayPosition;
        transform.localPosition = gridAlign();
        Horizontalrotation();
        closeUse();
        destroyUse();   
    }
    public Vector3 gridAlign()//网格对齐
    {
        if (isGroud)
        {
            float x = Mathf.Round(idleTransform.x / absorptionSize) * absorptionSize;
            float z = Mathf.Round(idleTransform.z / absorptionSize) * absorptionSize;
            float originalY = Mathf.Round(gameObject.transform.position.y / absorptionSize) * absorptionSize;
            float endY = (Mathf.Round((idleTransform.y) / absorptionSize) * absorptionSize);
            if (endY > originalY)
            {
                return new Vector3(x, endY, z);
            }
            else
            {
                return new Vector3(x, originalY, z);
            }
        }
        else
        {
            float x = Mathf.Round(idleTransform.x / absorptionSize) * absorptionSize;
            float y = Mathf.Round((idleTransform.y) / absorptionSize) * absorptionSize;
            float z = Mathf.Round(idleTransform.z / absorptionSize) * absorptionSize;
            return new Vector3(x, y, z);
        }
        
    }
    public void destroyUse()//关闭使用
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            Destroy(gameObject);
        }
    }
    public void Horizontalrotation()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Vector3 rotate = new Vector3(transform.localRotation.x, correctAngle(transform.localEulerAngles.y), transform.localRotation.z);
            gameObject.transform.localEulerAngles=rotate;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Dirt"))
        {
            isGroud = true;
        }
        
    }
    //private void OnTriggerStay(Collider other)
    //{
    //    //if (Input.GetKeyDown(KeyCode.Y))
    //    //{
    //    //    Debug.Log(other.tag);
    //    //    if (!other.transform.CompareTag("Dirt"))
    //    //    {
    //    //        Destroy(other.gameObject.GetComponent<BoxCollider>());
    //    //    }
    //    //}
    //}
    private void OnTriggerExit(Collider other)
    {
        if (other.transform.CompareTag("Dirt"))
        {
            isGroud = false; 
        }
    }
    public float correctAngle(float angle)
    {
        float valua= angle + 90;
        if (valua >= 360)
        {
            return valua - 360;
        }
        return valua;
    }
    public void closeUse()//关闭使用
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            if (isWall)//如果不是地板
            {
                gameObject.tag = "architected";
                gameObject.GetComponent<BoxCollider>().enabled = true;
                gameObject.GetComponent<BoxCollider>().isTrigger = false;
                gameObject.GetComponent<adsorption>().enabled = false;
                Destroy(gameObject.transform.GetComponent<Rigidbody>());
                rightRay.instance.creatWall = true;
                seperateChild();
            }
            if(isFloor)
            {
                Debug.Log("111");
                Debug.Log(temp);
                if (architectManage.instance.tempGame != null)
                {
                    temp = architectManage.instance.tempGame.transform;
                }
                if (temp != null)
                {
                    judgePosition();
                }
                gameObject.tag = "architected";
                gameObject.GetComponent<BoxCollider>().enabled = true;
                gameObject.GetComponent<adsorption>().enabled = false;
                Destroy(gameObject.transform.GetComponent<Rigidbody>());
                seperateFloor();
                rightRay.instance.creatWall = true;
                if (!architectManage.instance.once)
                {
                    architectManage.instance.onceGame = gameObject.transform;
                    architectManage.instance.once = true;
                }//存储第一个位置
                architectManage.instance.tempGame=gameObject.transform;
            }
            if (isStair)//这是楼梯的逻辑 
            {
                gameObject.tag = "architected";
                gameObject.GetComponent<BoxCollider>().enabled = true;
                gameObject.GetComponent<adsorption>().enabled = false;
                Destroy(gameObject.transform.GetComponent<Rigidbody>());
                rightRay.instance.creatWall = true;
            }
            if (isDoor)
            {
                gameObject.tag = "architected";
                gameObject.GetComponent<BoxCollider>().enabled = true;
                gameObject.GetComponent<adsorption>().enabled = false;
                Destroy(gameObject.transform.GetComponent<Rigidbody>());
                rightRay.instance.creatWall = true;
                for(int i = 0; i < transform.GetChild(1).childCount; i++)
                {
                    transform.GetChild(1).GetChild(i).gameObject.GetComponent<BoxCollider>().enabled=true;//可以把门的碰撞体打开了
                }
            }

        }
    }
    Vector3 vector3;
    public void judgePosition()//判断位置的方法
    {
        gameObject.transform.SetParent(temp);

        //if (!architectManage.instance.once)
        //{
            vector3= gameObject.transform.localPosition;
        //    architectManage.instance.once = true;
        //}
        
        Debug.Log(vector3);
        Debug.Log(vector3.x - vector3.z);
        //if (vector3.x - vector3.z > 0)
        //{
        //    Debug.Log(vector3.x);
            if (vector3.x > 1)
            {
                for (int i = 0; i < right.Count; i++)
                {
                    Destroy(temp.GetComponent<adsorption>().right[i].gameObject);
                    Destroy(left[i].gameObject);
                }
                Debug.Log("右");
            }
            if (vector3.x < -1)
            {
                for (int i = 0; i < left.Count; i++)
                {
                    Destroy(temp.GetComponent<adsorption>().left[i].gameObject);
                    Destroy(right[i].gameObject);
                }
                Debug.Log("左");
            }
        //}
        //else
        //{
            Debug.Log(vector3.x);
            if (vector3.z > 1)
            {
                for (int i = 0; i < right.Count; i++)
                {
                    Destroy(temp.GetComponent<adsorption>().foward[i].gameObject);
                    Destroy(back[i].gameObject);
                }
                Debug.Log("上");
            }
            if (vector3.z < -1)
            {
                for (int i = 0; i < back.Count; i++)
                {
                    Destroy(temp.GetComponent<adsorption>().back[i].gameObject);//temp是上一个物体的值
                    Destroy(foward[i].gameObject);
                }
                Debug.Log("下");
            }
        //judgeOnce();
    }
    Vector3 onceVector;
    //public void judgeOnce()//进行优化 相对于第一个位置
    //{
    //    gameObject.transform.SetParent(architectManage.instance.onceGame.transform);
    //    onceVector= gameObject.transform.localPosition;
    //    if (onceVector.x > 1)
    //    {
    //        if(architectManage.instance.onceGame.GetComponent<adsorption>().right != null)
    //        {
    //            for (int i = 0; i < architectManage.instance.onceGame.GetComponent<adsorption>().right.Count; i++)
    //            {
    //                Destroy(architectManage.instance.onceGame.GetComponent<adsorption>().right[i]);
    //            }
    //        }
    //        if (left != null)
    //        {
    //            for (int i = 0; i < left.Count; i++)
    //            {
    //                Destroy(left[i]);
    //            }
    //        }
    //        Debug.Log("you");
    //    }
    //    if (onceVector.x <-1)
    //    {
    //        if (architectManage.instance.onceGame.GetComponent<adsorption>().left != null)
    //        {
    //            for (int i = 0; i < architectManage.instance.onceGame.GetComponent<adsorption>().left.Count; i++)
    //            {
    //                Destroy(architectManage.instance.onceGame.GetComponent<adsorption>().left[i]);
    //            }
    //        }
    //        if (right != null)
    //        {
    //            for (int i = 0; i < right.Count; i++)
    //            {
    //                Destroy(right[i]);
    //            }
    //        }
    //        Debug.Log("you");
    //    }
    //    if (onceVector.z > 1)
    //    {
    //        if (architectManage.instance.onceGame.GetComponent<adsorption>().foward != null)
    //        {
    //            for (int i = 0; i < architectManage.instance.onceGame.GetComponent<adsorption>().foward.Count; i++)
    //            {
    //                Destroy(architectManage.instance.onceGame.GetComponent<adsorption>().foward[i]);
    //            }
    //        }
    //        if (back != null)
    //        {
    //            for (int i = 0; i < back.Count; i++)
    //            {
    //                Destroy(back[i]);
    //            }
    //        }
    //        Debug.Log("shang");
    //    }
    //    if (onceVector.z < -1)
    //    {
    //        if (architectManage.instance.onceGame.GetComponent<adsorption>().back != null)
    //        {
    //            for (int i = 0; i < architectManage.instance.onceGame.GetComponent<adsorption>().back.Count; i++)
    //            {
    //                Destroy(architectManage.instance.onceGame.GetComponent<adsorption>().back[i]);
    //            }
    //        }
    //        if (foward != null)
    //        {
    //            for (int i = 0; i < foward.Count; i++)
    //            {
    //                Destroy(foward[i]);
    //            }
    //        }
    //        Debug.Log("you");
    //    }
    //}
    Transform wallChidle;
    Transform floorChidle;
    public void seperateFloor()
    {
        wallChidle = transform.GetChild(0);//这是wall的集合
        floorChidle = transform.GetChild(1);//这是floor的集合
        wallChidle.transform.SetParent(wallParent.transform);
        floorChidle.transform.SetParent(floorParent.transform);
    }
    public void seperateChild()//移除父级
    {
        for(int i = transform.childCount-1; 0<i ; i--)//0号索引不能动他 他是拼装的材质
        {
            if (i == 2)
            {
                Transform childe = transform.GetChild(i);
                childe.SetParent(floorParent.transform);
            }
            else
            {
                Transform child = transform.GetChild(i);
                child.SetParent(wallParent.transform);
            }
                
           
        }
    }
}
