using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using Unity.VisualScripting;
using UnityEngine;
//using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class glidePos : MonoBehaviour
{
    public List<bool> pos = new List<bool> { false, false };
    //1左 2右
    public Transform GlidePos;
    public GameObject leftGuidePos;
    public Transform RecordLeftPos;
    public GameObject RighGuidetPos;
    public Transform RecordRightPos;
    public GameObject player;
    public float RotateSpeed;//旋转速度
    public float MaxRotateSpeed;
    public float radeSpeedRotate;
    private float RecordFadeSpeed;
    public float MaxradeSpeedRotate;
    public float AddSpeed;//增加的旋转速度
    public float DecreacSpeed;
    public float CloseSpeed;//旋转靠拢速度
    public Vector3 velco;
    public Rigidbody rightRd;
    public Rigidbody LeftRd;
    public float vertialSpeed;
    public float horizontal;
    public float downSpeed;
    public qingkuan Qin;
    private Rigidbody playerRd;
    public float fowardForce;
    public float idleForce;
    public float minIdleForce;//最小的idle力
    public float ListForce;
    public GameObject triangleGlider;//三角飞机
    private int leftNum;//记录按下的次数
    private int rightNum;
    private float coutdown;
    public static glidePos instance;
    private void Awake()
    {
        instance = this;
    }
    public qingkuan qin
    {
        set
        {
            Qin = value;
            switch (Qin)
            {
                case qingkuan.right:
                    rightRd.isKinematic = false;
                    LeftRd.isKinematic = true;
                    playerRd.isKinematic = true;
                    playerRd.constraints = RigidbodyConstraints.FreezePositionY;
                    RighGuidetPos.transform.SetParent(GlidePos);
                    player.transform.SetParent(RighGuidetPos.transform);
                    leftGuidePos.transform.SetParent(RighGuidetPos.transform);//放置父级下进行旋转
                    //leftGuidePos.transform.position = new Vector3(leftGuidePos.transform.position.x, leftGuidePos.transform.position.y+re, leftGuidePos.transform.position.z);///y轴要和玩家保持
                    leftGuidePos.transform.position = leftGuidePos.transform.position;//将另一个进行复位
                    DecreacSpeed = RotateSpeed;//对上一个速度进行归位
                    AddSpeed = AddRotateSpeed(AddSpeed);
                    fadeChange();
                    idleforce(rightRd);
                    rotate(RighGuidetPos, AddSpeed);
                    break;
                case qingkuan.left:
                    rightRd.isKinematic = true;
                    LeftRd.isKinematic = false;
                    playerRd.isKinematic = true;
                    playerRd.constraints = RigidbodyConstraints.FreezePositionY;
                    leftGuidePos.transform.SetParent(GlidePos);
                    player.transform.SetParent(leftGuidePos.transform);
                    RighGuidetPos.transform.SetParent(leftGuidePos.transform);//放置父级下进行旋转
                    //leftGuidePos.transform.position = new Vector3(leftGuidePos.transform.position.x, leftGuidePos.transform.position.y+re, leftGuidePos.transform.position.z);///y轴要和玩家保持
                    RighGuidetPos.transform.position = RecordRightPos.transform.position;//将另一个进行复位
                    AddSpeed = RotateSpeed;//对上一个速度进行归位
                    fadeChange();//根据按键的时长来改变渐变的速度
                    DecreacSpeed = DecreaceRoateSpeed(DecreacSpeed);
                    idleforce(LeftRd);
                    rotate(leftGuidePos, -DecreacSpeed);
                    break;
                case qingkuan.idle:
                    rightRd.isKinematic = true;
                    LeftRd.isKinematic = true;
                    playerRd.isKinematic = false;
                    playerRd.constraints = RigidbodyConstraints.None;
                    player.transform.SetParent(GlidePos);
                    DecreacSpeed = RotateSpeed;
                    AddSpeed = RotateSpeed;//对上一个速度进行归位
                    RighGuidetPos.transform.SetParent(player.transform);
                    leftGuidePos.transform.SetParent(player.transform);
                    break;
                case qingkuan.forward:
                    rightRd.isKinematic = true;
                    LeftRd.isKinematic = true;
                    playerRd.isKinematic = false;
                    break;
            }
        }
    }
    void Start()
    {
        LeftRd = leftGuidePos.GetComponent<Rigidbody>();
        rightRd = RighGuidetPos.GetComponent<Rigidbody>();
        playerRd = player.GetComponent<Rigidbody>();
        RecordLeftPos = leftGuidePos.transform;
        RecordRightPos = RighGuidetPos.transform;
        LeftRd.useGravity = false;
        rightRd.useGravity = false;
        leftGuidePos.transform.SetParent(player.transform);
        RighGuidetPos.transform.SetParent(player.transform);
        AddSpeed = DecreacSpeed = RotateSpeed;
        RecordFadeSpeed = radeSpeedRotate;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (glider.instance.isGlide)//如果开始滑翔
        {
            //Vector3 vector3 = player.transform.TransformPoint(player.transform.localPosition);
            //re = vector3.y;

            float y = Input.GetAxis("Horizontal");
            float x = Input.GetAxis("Vertical");
            Debug.Log(y);
            if (y == 0)
            {
                Down(playerRd);
                qin = qingkuan.idle;
            }
            if (y < 0)
            {

                qin = qingkuan.left;
                Down(LeftRd);
                if (leftNum == 0)
                {
                    radeSpeedRotate = RecordFadeSpeed;
                }
                leftNum++;
                rightNum = 0;
            }
            if (y > 0)
            {
                qin = qingkuan.right;
                Down(rightRd);
                if (rightNum == 0)
                {
                    radeSpeedRotate = RecordFadeSpeed;
                }
                rightNum++;
                leftNum = 0;
            }
            if (x > 0)
            {
                Addforce(x);
            }
            if (x == 0)
            {
                idleforce(playerRd);
            }
        }
        if (SceneManage.Instance.SetStart&&triangleGlider!=null)///三角架子开始追随玩家
        {
            triangleGlider.transform.SetParent(player.transform);
            SceneManage.Instance.SetStart = false;
        }

        //左负 右正 上正 下负数

    }
    public void idleforce(Rigidbody rigidbody)
    {
        rigidbody.AddForce(player.transform.forward * idleForce, ForceMode.Force);
        rigidbody.AddForce(Vector3.up * ListForce, ForceMode.Force);
        if (idleForce > minIdleForce)
        {
            idleForce -=Time.deltaTime;
        }
        else
        {
            idleForce = minIdleForce;
        }
    }
    
    public void fadeChange()//修改渐变的速度
    {
        coutdown+= Time.deltaTime;
        if (coutdown > 0.7f)
        {
            radeSpeedRotate = MaxradeSpeedRotate;//则record最大了
        }
    }
    public float AddRotateSpeed(float speed)//增加旋转的速度
    {
        speed += Time.deltaTime * radeSpeedRotate;
        if (speed >= MaxRotateSpeed)
        {
            speed = MaxRotateSpeed;
            return speed;
        }
        return speed;
    }
    public float DecreaceRoateSpeed(float speed)
    {
            speed += Time.deltaTime * radeSpeedRotate;
            if (speed <= -MaxRotateSpeed)
            {
                Debug.Log(-MaxRotateSpeed);
                speed = MaxRotateSpeed;
                return speed;
            }
            Debug.Log(speed);
            return speed;
    }

    public void Addforce(float x)//施加向前的冲力
    {
        playerRd.AddForce(player.transform.forward * fowardForce * x,ForceMode.Force);
        playerRd.AddForce(Vector3.up * ListForce, ForceMode.Force);
    }
    public void rotate(GameObject pos,float speed)//传进来哪个物品需要旋转
    {
        Vector3 Euler;
        if (speed > 0)
        {
            Debug.Log("a");
            Euler = new Vector3(0, pos.transform.rotation.eulerAngles.y + AddSpeed * Time.deltaTime, 0);
            pos.transform.rotation = Quaternion.Euler(Euler);
        }
        if(speed<0)
        {
            Debug.Log("b");
            Euler = new Vector3(0, pos.transform.rotation.eulerAngles.y - DecreacSpeed * Time.deltaTime, 0);
            pos.transform.rotation = Quaternion.Euler(Euler);
        }
        
    }
    public void Down(Rigidbody rd)//开始下降
    {
        rd.useGravity = true;
        velco = rd.velocity;//这是下降的
        vertialSpeed  = velco.y;
        horizontal = new Vector3(velco.x, 0, velco.z).magnitude;
        velco.y = downSpeed;
        rd.velocity = velco;
    }
    public enum qingkuan//将情况进行枚举
    {
        right,
        left,
        idle,
        forward,
    }
}
