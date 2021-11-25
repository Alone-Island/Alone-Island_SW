using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

// J : https://chameleonstudio.tistory.com/56 ����
public class DataController : MonoBehaviour
{
    
    static GameObject _container;
    static GameObject Container
    {
        get 
        {
            return _container;
        }
    }

    // J : �̱������� ����
    // J : DataContorller�� �ν��Ͻ�ȭ->�ٸ� ���Ͽ��� ��ũ��Ʈ�� ã�� �ʰ� �ٷ� ���� ����
    // J : static field, ��ü ������ ������� Ŭ�������� ����� ���� �޸𸮿� �Ҵ�ǰ� ���α׷� ���� ������ ����
    static DataController _instance;
    public static DataController Instance
    {
        get
        {
            if (!_instance)
            {
                _container = new GameObject();
                _container.name = "DataController";
                _instance = _container.AddComponent(typeof(DataController)) as DataController;
                DontDestroyOnLoad(_container);  // J : scene�� �̵��ص� game object ����
            }
            return _instance;
        }
    }

    public string SettingDataFileName = "Setting.json";

    public SettingData _settingData;
    public SettingData settingData
    {
        get 
        {
            if (_settingData == null) 
            {
                LoadSettingData();
                SaveSettingData();
            }
            return _settingData;
        }
    }

    public void LoadSettingData()
    {
        string filePath = Application.persistentDataPath + SettingDataFileName;
        Debug.Log(filePath);
        if (File.Exists(filePath))
        {
            Debug.Log("�ҷ����� ����!");
            string FromJsonData = File.ReadAllText(filePath);
            _settingData = JsonUtility.FromJson<SettingData>(FromJsonData);
        }
        else 
        {
            Debug.Log("���ο� ���� ����");
            _settingData = new SettingData();
        }
    }

    public void SaveSettingData()
    {
        string ToJsonData = JsonUtility.ToJson(settingData);
        string filePath = Application.persistentDataPath + SettingDataFileName;
        File.WriteAllText(filePath, ToJsonData);
        Debug.Log("���� �Ϸ�");
    }

    private void OnApplicationQuit()    // J : �� ���� �� ������ ����
    {
        SaveSettingData();
    }
}
