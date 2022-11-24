using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Reflection;

public class SaveAndLoadData
{
    public static void Serialize(object data, string filePath)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = null;
        try
        {
            stream = new FileStream(filePath, FileMode.Create);
            formatter.Serialize(stream, data);
        }
        catch (Exception e)
        {
            Debug.Log("====> Exception in Serialilize: " + e);
        }
        finally
        {
            stream?.Close();
        }
    }
    public static object Deserialize(string filePath)
    {
        object data = null;
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = (FileStream)null;
        try
        {
            if (!File.Exists(filePath)) return data;
            stream = new FileStream(filePath, FileMode.Open);
            data = formatter.Deserialize(stream);
        }
        catch (Exception e)
        {
            Debug.Log("====> Exception in Deserialize: " + e);
        }
        finally
        {
            stream?.Close();
        }
        return data;
    }
}
