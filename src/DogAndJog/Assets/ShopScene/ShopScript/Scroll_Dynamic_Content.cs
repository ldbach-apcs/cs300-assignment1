using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;
using UnityEngine.UI;

public class Scroll_Dynamic_Content : MonoBehaviour {
    string conn;
    string type;
    public GameObject prefab;


    public GameObject[] skin;

    public int itemHeight;
    IDbConnection dbconn;
    IDbCommand dbcmd;
    IDataReader reader;
    Vector3 position = Vector3.up * 780;
    void Start()
    {
        LoadList("Food");
        Rect rect = GetComponent<RectTransform>().rect;
    }

    GameObject NewUnit(int n)
    {
        Vector3 newPos = Vector3.down * itemHeight * n;
        GameObject go = Instantiate(skin[n], transform);
        // GameObject go = Instantiate(prefab, transform);
        go.transform.SetParent(transform);
        go.GetComponent<RectTransform>().anchoredPosition = newPos + new Vector3(10,-10,0);
        //  go.GetComponent<RectTransform>().transform.position = transform.position + Vector3.forward * itemHeight * n;// + new Vector3(0, -transform.childCount * 140, 0);
        // string name = reader.GetString(0);
        // string description = reader.GetString(1);
        // int storeCost = reader.GetInt32(2);
        // Item item = new MySkin(names[n], descriptions[n], storeCosts[n], owned[n]);
        // if (type.Equals("Food")) item = new MyFood(name, description, storeCost, 0, 0);
        // if (type.Equals("Skin")) item = new MySkin(name, description, storeCost, false);
        //go.GetComponent<StoreItemController>().SetItem(item);
        return go;
    }

    void OpenConnect()
    {
        conn = "URI=file:" + Application.dataPath + "/database.db"; //Path to database.

        dbconn = (IDbConnection)new SqliteConnection(conn);
        dbconn.Open();
        dbcmd = dbconn.CreateCommand();
    }
    void CloseConnect()
    {
        reader.Close();
        reader = null;
        dbcmd.Dispose();
        dbcmd = null;
        dbconn.Close();
        dbconn = null;
    }

    public void LoadList(string type)
    {
        Transform tf;
        for (int i = transform.childCount - 1; i>=0; i--)
        {
            tf = transform.GetChild(i);
            tf.parent = null;
            Destroy(tf.gameObject);
        }
        this.type = type;
        
        /*
        OpenConnect();
        string sqlQuery = "SELECT name, description, storeCost FROM " + type;
        dbcmd.CommandText = sqlQuery;
        reader = dbcmd.ExecuteReader();
        int n = 0;
        while (reader.Read())
        {
            NewUnit(n);
            n++;
            //string name = reader.GetString(0);
            //string description = reader.GetString(1);
            //int storeCost = reader.GetInt32(2);
			//Debug.Log( "name ="+name+"  description ="+  description);
        }
        CloseConnect();
        */

        if (type == "Food") {
            List<Food> list = DatabaseReader.Instance().ReadFood();
            int indx = 0;
            foreach (Food f in list) {
                NewFood(indx, f);
                indx++;
            }
        
        } else {
            NewUnit(0);
            NewUnit(1);
        }

        GetComponent<RectTransform>().sizeDelta = new Vector2(0, transform.childCount * itemHeight);
    }

     GameObject NewFood(int n, Food f)
    {
        string name = f.Name();
        string description = f.Description();
        int cost = f.Cost();
        int power = f.Power();

        Vector3 newPos = Vector3.down * itemHeight * n;

        Item item = new MyFood(name, description, cost, 0, power);

        GameObject go = Instantiate(prefab, transform);

        go.transform.SetParent(transform);
        go.GetComponent<RectTransform>().anchoredPosition = newPos + new Vector3(10,-10,0);
        
        // Inflate different view 
        go.transform.GetChild(2).GetComponent<Text>().text = name;
        // go.transform.GetChild(1).transform.GetComponent<Image>().sprite = food_milk;
        go.transform.GetChild(0).GetComponent<Text>().text = "$" + cost;//cost.ToString();
        go.transform.GetChild(3).GetComponent<Text>().text = description;

        var img = f.Img();
        if (img != null && img.Length != 0) {

            // Debug.Log(img.Length.ToString());
            // Load image
            //var tex = new Texture2D(100, 100);
            var tex = new Texture2D(1, 1, TextureFormat.ARGB32, false);
            tex.LoadImage(img);

            // Sprite sprite = Resources.Load("Resources/food_milk") as Sprite;
            Sprite sprite = Sprite.Create (tex, new Rect(0, 0, tex.width, tex.height), new Vector2(.5f,.5f));
            if (sprite == null) {
                Debug.Log("Load failed");
            }
            // Debug.Log(sprite.ToString());
            var imgGo = go.transform.GetChild(1).GetComponent<Image>();
            imgGo.sprite = sprite;
            //Debug.Log(imgGO);
            
    
             item.SetImg(sprite);
        }

        go.GetComponent<StoreItemController>().SetItem(item);
        return go;
    }
}
