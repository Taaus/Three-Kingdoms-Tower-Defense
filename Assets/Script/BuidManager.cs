using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class BuidManager : MonoBehaviour
{
    public GameObject towerPrefab;//定义塔的预制体（Prefab）应用

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))//判断鼠标左键是否按下
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);//从主相机发出射线ray到鼠标点击的点
            if (Physics.Raycast(ray, out RaycastHit hit))//定义一个类型为RaycasiHit的变量)) //判断是否击中了物体，并把击中物体的数据写入hit
            {
                if (hit.transform.name.Contains("BuildPoint")) //判断击中物体中的transform名字中是都包含BulidPoint
                {
                    if (BaseManager.Instance != null)
                    {
                        if (BaseManager.Instance.SpendGold(100))
                        {
                            Instantiate(towerPrefab, hit.transform.position + new Vector3(0, -0.5f, 0), Quaternion.identity);//实例化塔，生成位置y轴便宜-0.5
                            Destroy(hit.transform.gameObject);//销毁被击中的建塔点，防止重复建塔
                        }
                        else
                        {
                            Debug.Log("金币不足");
                        }
                    }
                }
            }
        }
        
    }

}
