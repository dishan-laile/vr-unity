using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject Ciname;
    public float Speed;
    public static Player instance;
    private Vector3 RecordY;
    public bool AimState;
    public bool isHaveClip;//判断手上是否有弹夹
    public float jumpSpeed;
    public bool isGround;//是否在地面上
    public float jumpTime;
    private float timer;
    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    private void Update()//检测人物输出
    {
        if (Input.GetMouseButton(1))//瞄准的逻辑
        {
            bool aim = OpenOrCloseAim(weapon.instance.AimState);
            if (aim)
            {
                weapon.instance.gunAnim = weapon.Gun.Aim;
            }
            if (!aim)
            {
                weapon.instance.gunAnim = weapon.Gun.AimOut;
            }
        }
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    RecordY.y = transform.position.y;
        //    JumpTarget = JumpHeight;
        //    if (JumpTarget > 0)
        //    {
        //        Debug.Log("1");
        //        Vector3 NewPos = transform.position;
        //        NewPos.y+= JumpSpeed * Time.deltaTime;
        //        transform.position = NewPos;
        //        if (transform.position.y >= RecordY.y+JumpTarget)
        //        {
        //            JumpTarget = 0;
        //        }
        //    }
        //    if (JumpTarget==0&&transform.position.y > RecordY.y+JumpTarget)
        //    {
        //        Debug.Log("tiao");
        //        transform.Translate(Vector3.down * JumpFallSpeed * Time.deltaTime);
        //        if (transform.position.y < RecordY.y)
        //        {
        //            Vector3 vector3= transform.position;
        //            vector3.y = RecordY.y;
        //            transform.position = vector3;
        //        }
        //    }
        //}//还需要完善
    }
public bool OpenOrCloseAim(bool state)//瞄准的逻辑
{
    if (state)
    {
        state = false;
        weapon.instance.AimState = state;
        Debug.Log(state);
        return state;
    }
    else
    {
        state = true;
        weapon.instance.AimState = state;
        Debug.Log(state);
        return state;
    }
}
    float y;
    float x;
    private void FixedUpdate()
    {
        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");
        if (!glider.instance.isGlide&&!isGround)
        {
          
            Vector3 vector3 = Ciname.transform.forward * y + Ciname.transform.right * x;
            transform.Translate(vector3 * Speed * Time.fixedDeltaTime, Space.World);
        }
        
        if (x>0 && isGround)
        {
            jumpstair();
            Debug.Log("开始跳跃");
            
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.transform.CompareTag("stair"))
        {
            isGround = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.CompareTag("stair"))
        {
            isGround = false;
        }
    }
    public void jumpstair()
    {
       
        gameObject.GetComponent<Rigidbody>().velocity = new Vector3(x, jumpSpeed, x);

    }
}

