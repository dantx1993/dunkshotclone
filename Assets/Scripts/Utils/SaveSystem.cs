using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Reflection;

/// <summary>
/// new(): new constraint áp đặt điều kiện 1 Generic class bắt buộc phải có 1 hàm khởi tạo k đối số
/// ISerializable: Interface cho phép một đối tượng kiểm soát tuần tự hóa và giải tuần tự hóa của chính nó.
/// </summary>
/// <returns></returns>
[Serializable]
public abstract class SaveSystem<T> : CollectionBase, ISerializable where T : SaveSystem<T>, new()
{
    public static T Instance
    {
        get
        {
            object obj;
            if (!CollectionBase.instances.TryGetValue(typeof(T), out obj))
            {
                obj = (object)new T();
                CollectionBase.instances.Add(typeof(T), obj);
                ((T)obj)._Init((T)obj);
            }
            return (T)obj;
        }
    }
    private string _FileName;

    protected abstract string FileName { get; }
    private string getFileName => string.Format($"{this.FileName}.data");
    private string FilePath => Path.Combine(Application.persistentDataPath, this.getFileName);
    protected SaveSystem()
    {
    }

    protected SaveSystem(SerializationInfo info, StreamingContext context)
    {
        foreach (FieldInfo serializationField in this.SerializationFields())
        {
            try
            {
                serializationField.SetValue((object)this, info.GetValue(serializationField.Name, serializationField.FieldType));
            }
            catch (SerializationException ex)
            {
                Debug.LogWarning((object)string.Format("Serialization Exception class type:'{0}' field: '{1}'", (object)this.GetType().Name, (object)serializationField.Name));
            }
        }
    }
    private void _Init(T instance)
    {
        Instance.Deserialize();
    }
    protected void Serialilize()
    {
        string filePath = this.FilePath;
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = null;
        try
        {
            stream = new FileStream(filePath, FileMode.Create);
            formatter.Serialize(stream, (object)this);
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


    protected void Deserialize()
    {
        string filePath = this.FilePath;
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = (FileStream)null;
        try
        {
            if (!File.Exists(filePath)) return;
            stream = new FileStream(filePath, FileMode.Open);
            Debug.Log("====> Stream is null: " + stream == null);
            T obj = formatter.Deserialize(stream) as T;
            CollectionBase.instances[typeof(T)] = (object)obj;
        }
        catch (Exception e)
        {
            Debug.Log("====> Exception in Deserialize: " + e);
        }
        finally
        {
            stream?.Close();
        }
    }

    public void Load()
    {
    }

    private FieldInfo[] SerializationFields()
    {
        return this.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy);
    }
    public void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        FieldInfo[] fieldInfoArray = this.SerializationFields();
        if ((uint)fieldInfoArray.Length <= 0U)
            return;
        foreach (FieldInfo fieldInfo in fieldInfoArray)
            info.AddValue(fieldInfo.Name, fieldInfo.GetValue((object)SaveSystem<T>.Instance));
    }
    public void Reset()
    {
        File.Delete(this.FilePath);
    }
}

public class CollectionBase
{
    protected static readonly Dictionary<Type, object> instances = new Dictionary<Type, object>();
    protected CollectionBase()
    {
    }
}



