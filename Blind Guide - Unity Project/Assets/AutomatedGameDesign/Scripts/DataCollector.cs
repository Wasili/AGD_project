using UnityEngine;
using System.Collections;
using System;
using UnityEngine.Experimental.Networking;

public class DataCollector {
    private static DataCollector _instance;
    private int gameID;
    private int gameSession;
    private DataMetricGame _currGame;
    private DataMetricLevel _currLevel; 
    
    //private string _constr = "URI=file:" + Application.dataPath + "/AutomatedGameDesign/Scripts/agd_db.db";
    //private IDbConnection dbConnection;
    
    DataCollector()
    {
        //dbConnection = (IDbConnection)new SqliteConnection(_constr);
        //dbConnection.Open();
    }

    public string getJSSONString()
    {
        return JsonUtility.ToJson(_currGame);
    }

    public IEnumerator Upload(string data)
    {
        byte[] myData = System.Text.Encoding.UTF8.GetBytes(data);
        UnityWebRequest www = UnityWebRequest.Post("http://agd.vdmastnet.nl/api.php", data);
        yield return www.Send();

        if (www.isError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("Upload complete!");
        }
    }

    public static DataCollector getInstance()
    {
        if (_instance == null)
        {
            _instance = new DataCollector();

            _instance.gameSession = (int)UnityEngine.Random.Range(0, 9223372036854775807);
        }
        return _instance;
    }

    public void createGame()
    {
        if(_currGame == null) {
            _currGame = new DataMetricGame();
            _currGame.starttime = System.DateTime.Now;
            _currGame.session = gameSession;
            Debug.Log("Created game...");
        }
    }

    public void startLevel(DataMetricLevel.Levels level)
    {
        createGame();
        if (_currLevel == null)
        {
            Debug.Log("Creating level...");
            _currLevel = new DataMetricLevel();
            _currLevel.startTime = DateTime.Now;
        }
    }

    public void endLevel(bool playerDied, string howPlayerDied = null)
    {
        if(_currLevel != null)
        {
            _currLevel.endTime = Time.time;
            _currLevel.playerDied = Convert.ToInt32(playerDied);
            if (howPlayerDied != null)
            {
                _currLevel.howPlayerDied = howPlayerDied;
            }
            _currLevel.howPlayerDied = "";

            _currGame.addLevel(_currLevel);
            _currLevel = null;
        }
    }

    public int playerDeadsInLevel(DataMetricLevel.Levels levelKind)
    {
        return _currGame.playerDeadsInLevel(levelKind); 
    }

    public void createObstacle(DataMetricObstacle obstacle)
    {
        if (_currLevel != null)
        {
            _currLevel.addObstacle(obstacle);
        }
    }

    public void createAttack(DataMetricAttack attack)
    {
        if (_currLevel != null)
        {
            _currLevel.addAttack(attack);
        }
    }
    
    public void createRates(DataMetricRating rate)
    {
        DataMetricLevel lastLevel = getLastLevel();
        lastLevel.addRating(rate);
    }

    public DataMetricLevel getLastLevel()
    {
        if(_currGame == null)
        {
            return null;
        }
        else
        {
            return _currGame.getLastLevel();
        }
    }

    //public void finishedGame(Time finished, bool playerDied)
    //{
    //    _currGame.endTime = finished;
    //}

    //public void saveMetric(DataMetric metric)
    //{

    //    Debug.Log(metric.queryForSave);
    //    IDbCommand dbCommand = dbConnection.CreateCommand();
    //    dbCommand.CommandText = metric.queryForSave;
    //    IDataReader dataReader = dbCommand.ExecuteReader();

    //    dataReader.Close();
    //    dbCommand.Dispose();

    //    if (metric is DataMetricGame)
    //    {
    //        IDbCommand cmdLastID = dbConnection.CreateCommand();
    //        cmdLastID.CommandText = "SELECT last_insert_rowid()";

    //        gameID = (int)cmdLastID.ExecuteScalar();
    //    }

    //}

    //public DataMetric getMetricData(string name)
    //{
    //    IDbCommand dbCommand = dbConnection.CreateCommand();
    //    dbCommand.CommandText = "SELECT * FROM Attack WHERE name=" + name;
    //    IDataReader dataReader = dbCommand.ExecuteReader();
    //    while (dataReader.Read())
    //    {
    //        string attackTime = dataReader.GetString(0);
    //        string type = dataReader.GetString(1);
    //    }

    //    dataReader.Close();
    //    dbCommand.Dispose();

    //    return null;
    //}

    /*public static int GetLocalMetricValue(string _name) {
        string metric = PlayerPrefs.GetString(_name);
        return metricValues[(int)metricPoint];
    }*/

    /*public static void SaveAllMetricsLocally()
    {
        //iterate through all metric points, except the last one
        for (int i = 0; i < metricValues.Length; i++)
        {
            //read the metric point at the current iteration
            Metric metric = (Metric)i;
            //save the metric value of the current iteration locally
            PlayerPrefs.SetInt(metric.ToString(), metricValues[i]);
        }
    }*/

    /*public static void LoadLocalMetricData() {
        for (int i = 0; i < metricValues.Length; i++)
        {
            //read the metric point at the current iteration
            Metric metric = (Metric)i;
            //load the metric value of the current iteration from local save data
            metricValues[i] = PlayerPrefs.GetInt(metric.ToString());
        }
    }*/

    //public static void ClearLocalMetricData() {
    //    //remove all entires from database
    //}

    //public static void LoadOnineMetricData() {
    //    //get all metric data using an http request and save it in local variables
    //}

    //public static void SaveAllMetricsOnline() {
    //    //send an http request containing all metric data to the server
    //}
}
