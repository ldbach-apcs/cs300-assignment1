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
        GameObject go = Instantiate(prefab, transform);
        go.GetComponent<RectTransform>().position = Vector3.down * itemHeight * n;// + new Vector3(0, -transform.childCount * 140, 0);
        string name = reader.GetString(0);
        string description = reader.GetString(1);
        int storeCost = reader.GetInt32(2);
        Item item = null;
        if (type.Equals("Food")) item = new MyFood(name, description, storeCost);
        if (type.Equals("Skin")) item = new MySkin(name, description, storeCost);
        go.GetComponent<StoreItemController>().SetItem(item);
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
        OpenConnect();
        string sqlQuery = "SELECT name, description, storeCost FROM " + type;
        dbcmd.CommandText = sqlQuery;
        reader = dbcmd.ExecuteReader();
        int n = 0;
        while (reader.Read())
        {
            n++;
            NewUnit(n);
            /*string name = reader.GetString(0);
            string description = reader.GetString(1);
            int storeCost = reader.GetInt32(2);
			Debug.Log( "name ="+name+"  description ="+  description);*/
        }
        CloseConnect();
        GetComponent<RectTransform>().sizeDelta = new Vector2(0, transform.childCount * itemHeight);
    }
}
