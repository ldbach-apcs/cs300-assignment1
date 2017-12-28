using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;
using System;

public class DatabaseReader : MonoBehaviour {

	// Use this for initialization
	void Start () {
        string conn = "URI=file:" + Application.dataPath + "/Scripts/Database/database.db"; //Path to database.
        IDbConnection dbconn;
        dbconn = (IDbConnection)new SqliteConnection(conn);
        dbconn.Open(); //Open connection to the database.
        
        

        dbconn.Close();
        dbconn = null;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void achivementReader(IDbConnection dbconn) {
        IDbCommand dbcmd = dbconn.CreateCommand();
        string sqlQuery = "SELECT * FROM Achievement";
        dbcmd.CommandText = sqlQuery;
        IDataReader reader = dbcmd.ExecuteReader();

        while (reader.Read()) {
            /*example: https://answers.unity.com/questions/743400/database-sqlite-setup-for-unity.html */
        }

        reader.Close();
        reader = null;
        dbcmd.Dispose();
        dbcmd = null;

        return;
    }

    void animationReader(IDbConnection dbconn)
    {
        IDbCommand dbcmd = dbconn.CreateCommand();
        string sqlQuery = "SELECT * FROM Animation";
        dbcmd.CommandText = sqlQuery;
        IDataReader reader = dbcmd.ExecuteReader();

        while (reader.Read())
        {

        }

        reader.Close();
        reader = null;
        dbcmd.Dispose();
        dbcmd = null;

        return;
    }

    void foodReader(IDbConnection dbconn)
    {
        IDbCommand dbcmd = dbconn.CreateCommand();
        string sqlQuery = "SELECT * FROM Food";
        dbcmd.CommandText = sqlQuery;
        IDataReader reader = dbcmd.ExecuteReader();

        while (reader.Read())
        {

        }

        reader.Close();
        reader = null;
        dbcmd.Dispose();
        dbcmd = null;

        return;
    }

    void missionReader(IDbConnection dbconn)
    {
        IDbCommand dbcmd = dbconn.CreateCommand();
        string sqlQuery = "SELECT * FROM  Mission";
        dbcmd.CommandText = sqlQuery;
        IDataReader reader = dbcmd.ExecuteReader();

        while (reader.Read())
        {

        }

        reader.Close();
        reader = null;
        dbcmd.Dispose();
        dbcmd = null;

        return;
    }

    void skinReader(IDbConnection dbconn)
    {
        IDbCommand dbcmd = dbconn.CreateCommand();
        string sqlQuery = "SELECT * FROM Skin";
        dbcmd.CommandText = sqlQuery;
        IDataReader reader = dbcmd.ExecuteReader();

        while (reader.Read())
        {

        }

        reader.Close();
        reader = null;
        dbcmd.Dispose();
        dbcmd = null;

        return;
    }
} 
