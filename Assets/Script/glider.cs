using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class glider : MonoBehaviour
{
    public Vector3 velco;
    public  float vertialSpeed;
    public float horizontal;
    public float downSpeed;
    private Rigidbody rd;
    public bool isGlide;
    public List<bool> glideItem= new List<bool> {false,false};
    public List<GameObject> trail;//拖尾
    public static glider instance;
    private float time;
    public float existTime;
    private bool cheakFly;
    private void Awake()
    {
        instance = this;
    }
    //一是人物滑翔
    //二是检测底部碰撞体是否要开启滑翔
    void Start()
    {
        rd = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    isGlide = true;
        //}
        //if (Input.GetKeyUp(KeyCode.Space))
        //{
        //    isGlide = false;
        //}
    }
    private void FixedUpdate()
    {
        velco = rd.velocity;
        vertialSpeed = velco.y;
        horizontal = new Vector3(velco.x, 0, velco.z).magnitude;
        if (isGlide)
        {
            velco.y = downSpeed;
            rd.velocity = velco;
        }
        if (cheakFly)
        {
            time += Time.deltaTime;
            if (time > existTime)
            {
                
                Debug.Log("kaiqi");
                isGlide = true;
                trail[0].SetActive(true);
                trail[1].SetActive(true);
                cheakFly = false;
            }
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.CompareTag("plane"))
        {

            cheakFly = true;
            
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Dirt"))
        {
            isGlide = false;
            rd.useGravity = true;//这里可能会出问题
            rd.mass = 0;
            Invoke("gravity", 1);
            Invoke("Destorytriangle", 1);
        }
        if (collision.transform.CompareTag("plane"))
        {
            cheakFly = false;
            time = 0;
            isGlide = false;
            trail[0].SetActive(false);
            trail[1].SetActive(false);
        }
        
    }
    public void gravity()
    {
        rd.useGravity = true;
        rd.mass = 1;
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.transform.CompareTag("plane"))
        {
            cheakFly = false;
            time = 0;
            isGlide = false;
            trail[0].SetActive(false);
            trail[1].SetActive(false);
        }
    }
    public void Destorytriangle()
    {
        Destroy(glidePos.instance.triangleGlider);
    }
}
