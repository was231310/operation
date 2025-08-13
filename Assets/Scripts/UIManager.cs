using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject BtnPrefab;
    public GameObject ScrollView;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="index">1,2,3,4,5.....</param>
    /// <param name="name"></param>
    public void CreateUIBtn(GameObject obj, int index, string name)
    {
        BtnCtrl btn_c = Instantiate<GameObject>(BtnPrefab).GetComponent<BtnCtrl>();
        btn_c.gameObject.name = name;
        btn_c.gameObject.transform.parent = ScrollView.transform;
        //TODO
        Vector3 temp_pos = Vector3.zero;
        Quaternion temp_rot = Quaternion.identity;

        btn_c.GetComponent<RectTransform>().anchoredPosition = new Vector3(10, -5 - (index - 1) * 35, 0);
        btn_c.SetControlObj(obj);
        btn_c.SetControlObjName(name);
    }




}
