using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Reflection;

[Serializable]
public class StorageUserInfo
{
    public static StorageUserInfo _Instance;
    public static StorageUserInfo Instance
    {
        get
        {
            if (_Instance == null)
            {
                StorageUserInfo ouput = new StorageUserInfo();
                ouput._Init(ouput);
            }
            return _Instance;
        }
    }
    protected string FileName => Define.FN_UserData;
    private string getFileName => string.Format($"{FileName}.data");
    private string FilePath => Path.Combine(Application.persistentDataPath, this.getFileName);
    private void _Init(StorageUserInfo instance)
    {
        _Instance = SaveAndLoadData.Deserialize(FilePath) == null ? instance : (StorageUserInfo)SaveAndLoadData.Deserialize(FilePath);
    }
    [SerializeField]
    private int _HighScore;
    public int highScore
    {
        get
        {
            return _HighScore;
        }
        set
        {
            _HighScore = value;
            EventHub.Instance.UpdateEvent(EventName.HIGH_SCORE, _HighScore);
            SaveAndLoadData.Serialize(Instance, FilePath);
        }
    }
    [SerializeField]
    private string _BallName = "Classic Ball";
    public string ballName
    {
        get
        {
            return _BallName;
        }
        set
        {
            _BallName = value;
            SaveAndLoadData.Serialize(Instance, FilePath);
        }
    }
    [SerializeField]
    private string _ThemeName = "Classic";
    public string themeName
    {
        get
        {
            return _ThemeName;
        }
        set
        {
            _ThemeName = value;
            SaveAndLoadData.Serialize(Instance, FilePath);
        }
    }
    public void Reset()
    {
        File.Delete(FilePath);
    }
    public void Load() { }

    public StorageUserInfo()
    {
        _BallName = "Classic Ball";
        _ThemeName = "Classic";
    }
}

public static class Define
{
    public static string FN_UserData = "UserData";
}

