using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;
using System;

public class DatabaseReader {

    private static readonly DatabaseReader instance = new DatabaseReader();
	private DatabaseReader() {
        connectionString = "URI=file:" + Application.dataPath + DATABASE_PATH;
        dbConnection = (IDbConnection) new SqliteConnection(connectionString);
     }

	public static DatabaseReader Instance()
	{
		return instance;
	}

    ~DatabaseReader() {
        SaveData();
        dbConnection.Dispose();
        dbConnection = null;
    }

    public List<Achievement> ReadAchievements() {
        dbConnection.Open();
        IDbCommand dbcmd = dbConnection.CreateCommand();
        string sqlQuery = "SELECT * FROM Achievement";
        dbcmd.CommandText = sqlQuery;
        IDataReader reader = dbcmd.ExecuteReader();

        List<Achievement> achievements = new List<Achievement>();

        while (reader.Read()) {
            /*example: https://answers.unity.com/questions/743400/database-sqlite-setup-for-unity.html */
            var newAchievement = Achievement.Parse(reader);
        }
        
        reader.Close();
        reader = null;
        dbcmd.Dispose();
        dbcmd = null;
        dbConnection.Close();

        return achievements;
    }

    void ReadAnimation()
    {
        IDbCommand dbcmd = dbConnection.CreateCommand();
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

    void ReadFood()
    {
        IDbCommand dbcmd = dbConnection.CreateCommand();
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

    public IQuest ReadQuest()
    {
        // if (dbConnection.State == ConnectionState.Closed)
            dbConnection.Open();
        
        IDbCommand dbcmd = dbConnection.CreateCommand();
        string sqlQuery = "SELECT * FROM  Mission";
        dbcmd.CommandText = sqlQuery;
        IDataReader reader = dbcmd.ExecuteReader();

        IQuest currentQuest = null;

        if (reader.Read()) {
            currentQuest = QuestFactory.Instance().ParseQuest(reader);
        }

        reader.Close();
        reader = null;
        dbcmd.Dispose();
        dbcmd = null;

//        if (dbConnection.State == ConnectionState.Open)
            dbConnection.Close();

        if (currentQuest == null)
            currentQuest = QuestFactory.Instance().GetQuest();
        return currentQuest;
    }
    
    public void SaveQuest(IQuest quest) {
        
        int type = -1;
        if (quest is DistanceQuest)
            type = 0;
        else if (quest is FacebookQuest)
            type = 1;

        String name = quest.name.Replace("'", "''");
        String description = quest.description.Replace("'", "''");
        int rewardExp = quest.rewardExp;
        int rewardMoney = quest.rewardMoney;
        double prevValue = quest.prevValue;
        double requireValue = quest.requireValue;

        // if (dbConnection.State == ConnectionState.Closed)
            dbConnection.Open();

        string sqlQuery = 
            " UPDATE Mission SET" +
            " name = '" + name + "'," +
            " description = '" + description +  "'," +
            " type = " + type.ToString() + "," +
            " rewardExp = " + rewardExp.ToString() + "," + 
            " rewardMoney = " + rewardMoney.ToString() + "," +
            " prevValue = " + prevValue.ToString() + "," +
            " requireValue = " + requireValue.ToString();

        Debug.Log(sqlQuery);

        IDbCommand dbCmd = dbConnection.CreateCommand();
        dbCmd.CommandText = sqlQuery;
        dbCmd.ExecuteNonQuery();

        dbCmd.Dispose();
        dbCmd = null;

        Debug.Log(type.ToString());
        
//        if (dbConnection.State == ConnectionState.Open)
            dbConnection.Close();
    }

    void ReadSkin()
    {
        IDbCommand dbcmd = dbConnection.CreateCommand();
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

    private void SaveData() {

    }

    private string DATABASE_PATH = "/database.db";
    private string connectionString;
    private IDbConnection dbConnection;

    private double totalDistance;
    public double getTotalDistance() { 
        return totalDistance;
    }

    private int totalShare;
    public int getTotalShare() {
        return totalShare;
    }
} 
