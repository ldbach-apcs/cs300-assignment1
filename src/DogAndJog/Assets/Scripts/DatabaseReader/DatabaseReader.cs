using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;
using System;

public class DatabaseReader {

    private static readonly DatabaseReader instance = new DatabaseReader();
	private DatabaseReader() {
        // Debug.Log("DB init now");
        connectionString = "URI=file:" + Application.dataPath + DATABASE_PATH;
        dbConnection = (IDbConnection) new SqliteConnection(connectionString);
        LoadSimpleData();
     }

	public static DatabaseReader Instance()
	{
		return instance;
	}

    ~DatabaseReader() {
       //  SaveSimpleData();
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

    public void BuySkin() {
        hasSkin = 1;
        SaveSimpleData();
    }

    public void BuyFood(string name, int ammt) {
        SaveSimpleData();
        dbConnection.Open();
        string sqlQuery = 
            " UPDATE Food SET" +
            " quantity = quantity + " + ammt.ToString() +
            " WHERE name = '" + name + "'";

        IDbCommand dbCmd = dbConnection.CreateCommand();
        dbCmd.CommandText = sqlQuery;
        dbCmd.ExecuteNonQuery();

        dbCmd.Dispose();
        dbCmd = null;

        dbConnection.Close();
    }

	public void EatFood(string name) {
		SaveSimpleData();
		dbConnection.Open();
		string sqlQuery = 
			" UPDATE Food SET" +
			" quantity = quantity - 1 " +
			" WHERE name = '" + name + "'";

		IDbCommand dbCmd = dbConnection.CreateCommand();
		dbCmd.CommandText = sqlQuery;
		dbCmd.ExecuteNonQuery();

		dbCmd.Dispose();
		dbCmd = null;

		dbConnection.Close();
	}

    public List<Food> ReadFood()
    {
        dbConnection.Open();
        IDbCommand dbcmd = dbConnection.CreateCommand();
        string sqlQuery = "SELECT * FROM Food";
        dbcmd.CommandText = sqlQuery;
        IDataReader reader = dbcmd.ExecuteReader();

        List<Food> foodList = new List<Food>();

        while (reader.Read())
        {
            foodList.Add(Food.Parse(reader));            
        }

        reader.Close();
        reader = null;
        dbcmd.Dispose();
        dbcmd = null;
        dbConnection.Close();

        Debug.Log ("Food amount: " + foodList.ToArray().Length.ToString());
        return foodList;
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

    public void SaveSimpleData() {
        PlayerPrefs.SetInt(KEY_HUNGER, hunger);
        PlayerPrefs.SetInt(KEY_EXP, exp);
        PlayerPrefs.SetInt(KEY_MONEY, money);
        PlayerPrefs.SetFloat(KEY_DISTANCE, (float) totalDistance);
        PlayerPrefs.SetInt(KEY_SHARE, totalShare);
        PlayerPrefs.SetInt(KEY_SKIN, currentSkin);
        PlayerPrefs.SetInt(KEY_HAS_SKIN, hasSkin);
        PlayerPrefs.SetInt("first_start", firstStart);

        Debug.Log(firstStart);
        PlayerPrefs.Save();
    }

    private void LoadSimpleData() {
        hunger = PlayerPrefs.GetInt(KEY_HUNGER, 0);
        exp = PlayerPrefs.GetInt(KEY_EXP, 0);
        money = PlayerPrefs.GetInt(KEY_MONEY, 0);
        totalDistance = PlayerPrefs.GetFloat(KEY_DISTANCE, 0);
        totalShare = PlayerPrefs.GetInt(KEY_SHARE, 0);
        currentSkin = PlayerPrefs.GetInt(KEY_SKIN, 0);
        hasSkin = PlayerPrefs.GetInt(KEY_HAS_SKIN, 0);

        // Debug.Log(firstStart);
       if (PlayerPrefs.GetInt("xxfirst_start", 0) == 0) {
            firstStart = 1;
            money = 2000;
        }
    }

    private int firstStart;

    private string DATABASE_PATH = "/db_w_sprite.db";
    //private string DATABASE_PATH = "/database.db";
    private string connectionString;
    private IDbConnection dbConnection;


    private string KEY_HUNGER = "current_hunger";
    public int hunger {get; set;}

    private string KEY_EXP = "total_exp";
    public int exp {get; set;}

    private string KEY_MONEY = "current_money";
    public int money {get; set;}

    private string KEY_DISTANCE = "total_distance";
    public double totalDistance {get; set;}

    private string KEY_SHARE = "total_share";
    public int totalShare {get; set;}

    private string KEY_SKIN = "current_skin";
    public int currentSkin {get; set;}

    private string KEY_HAS_SKIN = "has_skin";
    public int hasSkin { get; set;}
} 
