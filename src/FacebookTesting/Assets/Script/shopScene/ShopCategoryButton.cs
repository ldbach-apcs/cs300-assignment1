using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;
using UnityEngine.UI;

public class ShopCategoryButton : MonoBehaviour {
    public static ShopCategoryButton currentActive;
    public GameObject content;
    public bool startActive;
    public string type;
    Image image;
    Vector3 position = Vector3.up * 780;
    void Start () {
        if (startActive) currentActive = this;
        image = GetComponent<Image>();
    }

    public void OnClick()
    {
        currentActive.UnSet();
        currentActive = this;
        content.GetComponent<Scroll_Dynamic_Content>().LoadList(type);
        image.color = new Color(0.349f, 1, 0.2941f);
    }
	
    void UnSet()
    {
        image.color = Color.white * (0.93333f);
    }
	// Update is called once per frame
	void Update () {
		
	}
}
