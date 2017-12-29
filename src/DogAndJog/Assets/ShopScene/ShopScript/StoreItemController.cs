using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreItemController : MonoBehaviour {
    Item item;
    void Start()
    {
        gameObject.GetComponent<Button>().
            onClick.AddListener(delegate () { BuyController.buyController.ItemLoad(item); });
    }
	public void SetItem(Item item)
    {
        this.item = item;
        transform.GetChild(0).GetComponent<Text>().text = item.GetName();

        transform.GetChild(1).GetComponent<Image>().sprite = Resources.Load("foodMeat") as Sprite;
        transform.GetChild(2).GetComponent<Text>().text = item.GetPrice().ToString();
        transform.GetChild(3).GetComponent<Text>().text = item.GetDescription();
    }
	// Update is called once per frame
	public void OnClick () {
        
	}
}
