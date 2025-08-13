using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FingerController : MonoBehaviour
{
    public Transform center;
    public Transform organ;
    // Start is called before the first frame update

    public bool CanCtrl = false;

    public GameObject Slider_qlx;
    public GameObject Slider_zw;
    public GameObject Slider_nd;

    void Update()
    {
        if (!CanCtrl) return;

        if(Input.GetMouseButton(1))
        {
            Rotate();
        }
        
        if (Input.GetMouseButton(2))
        {
            Move();
        }

        ScallView();

        RayHit();
    }

    private void RayHit()
    {
        // 检测鼠标左键点击
        if (Input.GetMouseButtonDown(0))
        {
            // 创建一个射线从相机发出，穿过鼠标点击位置
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // 执行射线检测
            if (Physics.Raycast(ray, out hit))
            {
                // 检测到物体，输出碰撞的信息
                Debug.Log("Hit: " + hit.collider.name);
                if (hit.collider.tag.Equals("qlx"))
                {
                    Slider_qlx.SetActive(true);
                    Slider_nd.SetActive(false);
                    Slider_zw.SetActive(false);
                }
                if (hit.collider.tag.Equals("nd"))
                {
                    Slider_qlx.SetActive(false);
                    Slider_nd.SetActive(true);
                    Slider_zw.SetActive(false);
                }
                if (hit.collider.tag.Equals("zw"))
                {
                    Slider_qlx.SetActive(false);
                    Slider_nd.SetActive(false);
                    Slider_zw.SetActive(true);
                }
            }
            else
            {
                Debug.Log("No hit detected.");
            }
        }
    }

    public float rotateSpeed = 5f;

    private void Rotate()
    {
        float mouseX = Input.GetAxis("Mouse X") * rotateSpeed;
        float mouseY = Input.GetAxis("Mouse Y") * rotateSpeed;

        organ.Rotate(Vector3.up, -mouseX, Space.World);
        organ.Rotate(Vector3.forward, mouseY, Space.Self);
    }

    public float minZoom = 2f, maxZoom = 10f;
    public float zoomSpeed = 2f;
    private float currentZoom = 5f;
    private void ScallView()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0)
        {
            currentZoom = Mathf.Clamp(currentZoom - scroll * zoomSpeed, minZoom, maxZoom);
            transform.position = new Vector3(transform.position.x,transform.position.y, (transform.forward * -currentZoom).z);
        }
    }


    public float MoveSpeed;
    private void Move()
    {
        float mouseX = Input.GetAxis("Mouse X") * rotateSpeed;
        float mouseY = Input.GetAxis("Mouse Y") * rotateSpeed;

        transform.position += new Vector3(-mouseX * MoveSpeed, -mouseY * MoveSpeed, 0);
    }

}
