using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollViewCtrl : MonoBehaviour
{
    public GameObject ScrollView;
    public GameObject Load;

    public Button Hide;
    public Button Show;
    // Start is called before the first frame update
    void Start()
    {
        Hide.onClick.AddListener(() => { ScrollView.SetActive(false); Load.SetActive(false); Hide.gameObject.SetActive(false); Show.gameObject.SetActive(true); });
        Show.onClick.AddListener(() => { ScrollView.SetActive(true); Load.SetActive(true); Hide.gameObject.SetActive(true); Show.gameObject.SetActive(false); });
    }

    // Update is called once per frame
    void Update()
    {

    }
}
