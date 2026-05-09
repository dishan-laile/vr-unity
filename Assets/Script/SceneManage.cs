using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManage : MonoBehaviour
{
    public static SceneManage Instance;
    public GameObject scene;
    public bool isShake;
    public float ShakeSpeed=4;
    public float ShakeTarget=2.5f;
    public GameObject player;
    public int NumberShake;
    public GameObject Montain;
    private bool moveWay=true;
    public int RecordGlide;//记录开始滑翔
    public bool SetStart;
    public bool SetEnd;
    public GameObject Gun;
    public GameObject Sword;
    public GameObject RightHand;
    public bool OnceActive;//激活一次 防止锁死手柄
    public int RecordNum;//记录执行的次数
    public List<GameObject> WeaponList;
    private int WeaponIndex;
    private GameObject temp;
    private void Awake()
    {
        Instance = this;
        WeaponIndex = 0;
        WeaponList=new List<GameObject> {Gun,Sword };
    }
    private void Update()
    {
        if (isShake)
        {
            Vector3 vector3 = new Vector3(0, Mathf.Lerp(scene.transform.position.y, ShakeTarget,Time.deltaTime*ShakeSpeed), 0);
            scene.transform.position = vector3;
            player.transform.position = new Vector3(player.transform.position.x,vector3.y,player.transform.position.z);
            if (Mathf.Abs(vector3.y - ShakeTarget) < 0.1f)
            {
                scene.transform.position = new Vector3(0, ShakeTarget, 0);
                //StartCoroutine(item.Instance.CreatFristEnemy());//可以开始生成第一波怪物了
                    isShake = false;
            }
        }
        if (RecordGlide>=1&&SetEnd==false)
        {
            SetStart = true;
            SetEnd = true;
        }
        if (Gun.activeSelf)
        {
            RightHand.transform.localPosition = new Vector3(0.045070f, -0.1988f, 0.326f);
            RightHand.transform.localRotation = Quaternion.Euler(3.161f, -6.178f, -111.155f);
            OnceActive = true;
        }
        if (Sword.activeSelf)
        {
            RightHand.transform.localPosition = new Vector3(0.0503f, -0.142f, 0.1198f);
            RightHand.transform.localRotation = Quaternion.Euler(-17.8f, 2.18f, -112.155f);
        }
        if (!Gun.activeSelf && !Sword.activeSelf&&OnceActive)
        {
            RightHand.transform.localPosition = new Vector3(0.018f, -0.027f, 0.013f);
            RightHand.transform.localRotation = Quaternion.Euler(-3.7f, 8.9f, -452.932f);
            Debug.Log("设置成功");
            OnceActive = false;
        }
        changeWeapom();
    }
    public void MoveWay()//移动山方式封装
    {
        while (moveWay)
        {
            Debug.Log("dioa");
            Vector3 vector3 = new Vector3(Montain.transform.position.x, Mathf.Lerp(Montain.transform.position.y, -27, Time.deltaTime *1f), Montain.transform.position.z);
            Montain.transform.position = vector3;
            if (Mathf.Abs(vector3.y+27) < 0.1f)
            {
                Montain.transform.position = new Vector3(Montain.transform.position.x, -27, Montain.transform.position.z);
                moveWay = false;
                Debug.Log("wan");
            }
        }
    }
    public void changeWeapom()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            Debug.Log(WeaponIndex);
            if (WeaponIndex == 3)
            {
                WeaponIndex = 0;
            }
            if (temp != null)
            {
                temp.SetActive(false);
            }
            if (WeaponIndex < 2)
            {
                WeaponList[WeaponIndex].SetActive(true);
                temp = WeaponList[WeaponIndex];
               
            }
            WeaponIndex++;

        }
    }
}
