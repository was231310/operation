using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BtnCtrl : MonoBehaviour
{
    public Sprite Open;
    public Sprite Close;

    bool isShow = true;

    Button btn;
    Image image;

    GameObject ControlObj;
    // Start is called before the first frame update
    void Start()
    {
        btn = GetComponent<Button>();
        image = GetComponent<Image>();

        btn.onClick.AddListener(()=> {
            if (isShow == true)
            {
                if (null != ControlObj)
                    ControlObj.SetActive(false);
                isShow = false;
                image.sprite = Close;
            }
            else
            {
                if (null != ControlObj)
                    ControlObj.SetActive(true);
                isShow = true;
                image.sprite = Open;
            }
        });
    }

    public void SetControlObj(GameObject obj)
    {
        this.ControlObj = obj;
    }

    public void SetControlObjName(string name)
    {
        this.GetComponentInChildren<Text>().text = name;
    }



    // Update is called once per frame
    void Update()
    {
        
    }


}
