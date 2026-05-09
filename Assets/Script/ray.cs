using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ray : MonoBehaviour
{
    public Ray leftRay;
    private Vector3 dir;
    public Transform lineStartPos;
    public Transform endPos;
    public GameObject EmptyBullet;//空弹夹
    public GameObject fillBullet;//满弹夹
    public GameObject leftHand;//左手
    public static ray instance;
    public  bool isChangeShader;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        leftRay = new Ray(GameObject.Find("[Left Controller] Ray Origin").transform.position, dir);
        dir = endPos.position - lineStartPos.position;
    }

    // Update is called once per frame
    void Update()
    { 
            CheakRay();
    }
    public void CheakRay()
    {
        leftRay.direction = endPos.position - lineStartPos.position;
        leftRay.origin = lineStartPos.position;
        RaycastHit hit;
        if (Physics.Raycast(leftRay,out hit))
        {
            
            //Debug.Log("射线射到的点" + hit.point);
            if (hit.transform.CompareTag("pick"))//寻找子弹
            {
                Debug.Log("wa");
                changeShader(hit,0);
                if (Input.GetKeyDown(KeyCode.G))//这部分后面要改
                {
                    CreantItem(hit,0);
                }
            }
        }
    }
    public void CreantItem(RaycastHit hit,int ID)//生成物体在手边 子物体的参数  为零则是有子物体的
    {
        if (ID >= 0)
        {
            Transform zhao = hit.transform.GetChild(ID);
            zhao.gameObject.SetActive(false);
            EmptyBullet.SetActive(true);
            leftHand.GetComponent<Animator>().SetFloat("Grip", 0.11f);
            if (zhao.GetComponent<item>().itemEvent[4] == true&&zhao.GetComponent<item>()!=null)
            {
                //则手上有了弹夹
            }
        }
        else
        {
            Transform zhao = hit.transform;
            zhao.gameObject.SetActive(false);
            Debug.Log(zhao.gameObject.activeSelf);
            EmptyBullet.SetActive(true);
            leftHand.GetComponent<Animator>().SetFloat("Grip", 0.11f);
        }
    }
    public void changeShader(RaycastHit hit, int ID)//子物体的参数
    {
        if (ID >= 0)
        {
            Transform zhao = hit.transform.GetChild(ID);
            zhao.transform.GetComponent<MeshRenderer>().materials[1].SetFloat("_Scale", 1.03f);
            isChangeShader = true;//改变了shader
            Debug.Log("gaibian");
        }
        else
        {
            Transform zhao = hit.transform;
            zhao.transform.GetComponent<MeshRenderer>().materials[1].SetFloat("_Scale", 1.03f);
            isChangeShader = true;//改变了shader
            Debug.Log("gaibian");
        }
    }
    
    public void GameShader(GameObject obj,int ID)
    {
        if (ID >= 0)
        {
            
            Transform valua = obj.transform.GetChild(0);//寻找子物体子弹
            valua.transform.GetComponent<MeshRenderer>().materials[1].SetFloat("_Scale", 1.03f);
            isChangeShader = true;//改变了shader
            
        }
        else
        {
            Transform valua = obj.transform;//就是此物体
            valua.transform.GetComponent<MeshRenderer>().materials[1].SetFloat("_Scale", 1.03f);
            isChangeShader = true;//改变了shader
        }
        
    }
    public void ResetShader(GameObject obj, int ID)
    {
        if (ID >= 0)
        {

            Transform valua = obj.transform.GetChild(0);//寻找子物体子弹
            valua.transform.GetComponent<MeshRenderer>().materials[1].SetFloat("_Scale", 1.0f);
            isChangeShader = true;//改变了shader

        }
        else
        {
            Transform valua = obj.transform;//就是此物体
            valua.transform.GetComponent<MeshRenderer>().materials[1].SetFloat("_Scale", 1.0f);
            isChangeShader = true;//改变了shader
        }

    }
    public void GameItem(GameObject obj, int ID)//生成物体在手边
    {
        if (ID >= 0) 
        {
            Transform zhao = obj.transform.GetChild(ID);
            zhao.gameObject.SetActive(false);
            Debug.Log(zhao.gameObject.activeSelf);
            EmptyBullet.SetActive(true);
            leftHand.GetComponent<Animator>().SetFloat("Grip", 0.11f);
            //if (zhao.GetComponent<item>().itemEvent[4] == true && zhao.GetComponent<item>() != null)
            //{
            //    //则手上有了弹夹
            //}
        }
        else
        {
            Transform zhao = obj.transform;
            zhao.gameObject.SetActive(false);
            Debug.Log(zhao.gameObject.activeSelf);
            EmptyBullet.SetActive(true);
            leftHand.GetComponent<Animator>().SetFloat("Grip", 0.11f);
        }
        
    }
    public void ResetShader()
    {

    }
}
