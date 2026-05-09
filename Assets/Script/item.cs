using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class item : MonoBehaviour
{
    public static item Instance;
    public List<bool>itemEvent=new List<bool> { false, false, false, false,false};
    public int HandID;
    //1检测手柄滑动进行特效攻击 以上皆是1的参数
    //2为摧毁特效
    public List<Transform> CreatFristTrans;
    public GameObject FristEnemy;
    //3为检测拾取的枪械上的弹夹
    public Camera MainCamera;
    public float TurnSpeed;
    //4为转动腰带随着相机
    //五是滑翔机的碰撞
    private void Awake()
    {
        Instance=this;
    }
    void Start()
    {
        if (itemEvent[1] == true)//摧毁特效
        {
            Invoke("demageSwordParticle", 1.3f);
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (itemEvent[3] == true)//腰带随着人物的移动
        {
            float y=MainCamera.transform.rotation.eulerAngles.y;
            transform.rotation = Quaternion.Euler(0,y,0);
        }
    }

    public void demageSwordParticle()
    {
        Destroy(gameObject);
    }
    //public void  CreatFristEnemy() //为开始生成第一波怪物
    //{
    //    GameObject a=Instantiate(FristEnemy);
    //    a.transform.position = CreatFristTrans[Random.Range(0, CreatFristTrans.Count)].position;
    //}
    public IEnumerator CreatFristEnemy()
    {
        yield return new WaitForSeconds(5);
        GameObject a = Instantiate(FristEnemy);
        a.transform.position = CreatFristTrans[Random.Range(0, CreatFristTrans.Count)].position;
    }
    
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("hand") && itemEvent[4]==true)
        {
            ray.instance.GameShader(gameObject, -1);
            if (Input.GetKeyDown(KeyCode.G))
            {
                other.transform.parent.GetComponent<Animator>().SetFloat("Grip", 0.11f);//得到父物体
                SceneManage.Instance.RecordGlide++;
            }
        }
        if (other.CompareTag("rightHand") && itemEvent[4] == true)
        {
            ray.instance.GameShader(gameObject, -1);
            if (Input.GetKeyDown(KeyCode.G))
            {
                other.transform.parent.GetComponent<Animator>().SetFloat("Grip", 0.11f);//得到父物体
                SceneManage.Instance.RecordGlide++;
            }
        }
    }
   
}
