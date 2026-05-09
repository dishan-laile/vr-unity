using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class rightRay : MonoBehaviour
{
    public Ray ray;
    private Vector3 dir;
    public Transform original;
    public Transform endPos;
    public bool isCollide;
    public  Vector3 rayPosition;
    public GameObject achitectItem;
    private GameObject instaniateItem;
    public GameObject idleCreat;
    public GameObject rightBox;
    public GameObject leftBox;
    public bool creatWall;
    public int index;//获得该建筑物体的下标
    public int temp;//存储下来之前的下标
    private bool changeItem;//更换了物体
    public GameObject wallParent;
    public GameObject floorParent;
    public GameObject tempGa;//存储上一物体的变量
    private bool islerp;//渐变了按钮
    private bool endLerp;
    private GameObject lerpGa;
    public static rightRay instance;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        ray = new Ray(GameObject.Find("[Right Controller] Ray Origin").transform.position,dir);
    }

    // Update is called once per frame
    void Update()
    {
            launchRay();
        if (islerp && !endLerp)
        {
            if (lerpGa != null)
            {
                if (lerpGa.transform.GetComponent<Slider>().value > 0)
                {
                    lerpGa.transform.GetComponent<Slider>().value -= Time.deltaTime * 5;
                }
                else
                {
                    endLerp = true;
                }
                
            }
        }
    }
    public void launchRay()//射线距离为十  要建立一个新的tag
    { 
       
        ray.origin = original.position;
        ray.direction = endPos.position - original.position;
        RaycastHit hit;
        float length = 10f;
        if (Physics.Raycast(ray, out hit, length))
        {
            if (hit.transform.CompareTag("lerp"))//ui渐变的
            {
                if (Input.GetKey(KeyCode.Mouse0))
                {
                    hit.transform.GetComponent<Slider>().value += Time.deltaTime * 5;
                    lerpGa = hit.transform.gameObject;
                    islerp = true;
                    endLerp = false;
                }
            }
            if (hit.transform.CompareTag("architected"))//已经建造完成了的
            {
                rayPosition= hit.transform.position;//后面可以在这上面进行加y轴布置家具
                Debug.Log(hit.transform.gameObject.name);
            }
            else//
            {
                if (!hit.transform.CompareTag("architecture") && !hit.transform.CompareTag("Player"))
                {
                    isCollide = true;
                    rayPosition = hit.point;
                   
                }
                
               
            }
            if (hit.transform.CompareTag("Player"))
            {
                
                rayPosition = hit.transform.position;
            }
            Debug.Log(hit.transform.tag);
        }
        else
        {
           
            isCollide = false;
            rayPosition = idleCreat.transform.position;
        }

        SetParent(rayPosition);
        cheackIndexitem();
    }
    public void cheackIndexitem()//用来检测下标是否是同一个物体
    {
        if (temp == 1)
        {
            architectManage.instance.closeOrOpenChilde(floorParent, true);
            architectManage.instance.closeOrOpenChilde(wallParent, false);
        }
        if (temp == 2)
        {
            architectManage.instance.closeOrOpenChilde(wallParent, true);
            architectManage.instance.closeOrOpenChilde(floorParent, false);
        }
        if (temp == 3)
        {

        }
        if (temp != index)//说明更换了物体
        {
            temp= index;
            changeItem = true;
            if (tempGa == eventManage.instance.stair)
            {
                Debug.Log("bucui");
            }
            //if (tempGa != null)
            //{
            //    Destroy(tempGa);
            //}
            
        }
    }

    public void SetParent(Vector3 transform)
    {
        if (creatWall)
        {
            instaniateItem = Instantiate(achitectItem);
            instaniateItem.transform.position = transform;
            instaniateItem.GetComponent<BoxCollider>().enabled = true;
            rightBox.GetComponent<BoxCollider>().enabled = false;
            leftBox.SetActive(false);
            creatWall = false;
            if (achitectItem == eventManage.instance.floor)
            {
                achitectItem.GetComponent<adsorption>().isFloor = true;
            }
            if (temp == index)
            {
                
                tempGa = instaniateItem;//存储每一个物体
            }
        }
    }
}
