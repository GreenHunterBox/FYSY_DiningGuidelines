using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class UI_Controller : MonoSingleton<UI_Controller>
{
    [SerializeField] GameObject[] pages;
    //[Header("！！！！！！01Page！！！！！！")]
    //[Header("！！！！！！01Page！！！！！！")]

    private void Start()
    {
        InitBtnOnClickEvent();
        ShowPage(0);
    }

    void InitBtnOnClickEvent()
    {

    }

    void ShowPage(int i){for (int j = 0; j < pages.Length; j++) pages[j].SetActive(j == i);}
    T LoadData<T>(string fileName) where T : new()
    {
        string savePath = Path.Combine(Application.streamingAssetsPath, fileName);
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            T data = JsonUtility.FromJson<T>(json);
            Debug.Log($"Data loaded from {savePath}");
            return data;
        }
        else
        {
            Debug.LogWarning($"Save file not found in {savePath}. Returning default data.");
            return new T(); // 卦指潮範峙
        }
    }
}
