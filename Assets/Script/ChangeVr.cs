using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeVr : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject changGam;
    public GameObject startFather;
    public GameObject endFather;
    public int Id;//µ±Ç°×´Ì¬µÄid
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Id++;
        }
        if (Id == 1)
        {
            changGam.transform.SetParent(endFather.transform);
        }
        if (Id==0||Id == 2)
        {
            changGam.transform.SetParent(startFather.transform);
        }
        if (Id == 3)
        {
            Id = 0;
        }
    }
}
