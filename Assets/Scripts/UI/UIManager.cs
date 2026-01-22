using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField]
    private Dictionary<Type, UIBase> uiDict = new();

    [SerializeField] //초기 비활성화 되어있는 UI등록용 리스트
    private List<UIBase> uiList;
    private void Awake()
    {
        Instance = this;

        //UI 수동등록 처리
        foreach(var ui in uiList)
        {
            ReisterUI(ui);
        }
    }
    public void ReisterUI(UIBase ui) //UI 등록
    {
        var type = ui.GetType();
        if (!uiDict.ContainsKey(type))
        {
            uiDict.Add(type, ui);
            //Debug.Log($"{type}.type {ui}.ui 등록된 UI갯수{uiDict.Count}");
        }
        else
        {
            Debug.Log($"{type} UI 가 등록되어 있습니다");
        }
    }

    public T Get<T>() where T : UIBase //UI 꺼내기
    {
        var type = typeof(T);
        if (uiDict.TryGetValue(type, out var ui))
        {
            return (T)ui;
        }

        Debug.Log($"[UIManager] {type} UI가 등록되지 않았습니다");
        return null;
    }
    public void Show<T>(params object[] param) where T : UIBase //UI 열기
    {
        var ui = Get<T>();
        if(ui == null)
        {
            Debug.LogError($"[UIManager] {typeof(T)} UI가 null입니다.");
            return;
        }
        ui.SetActive(true);
        ui.opened?.Invoke(param);

    }
    public void Hide<T>(params object[] param) where T : UIBase //UI 닫기
    {
        var ui = Get<T>();
        if (ui == null)
        {
            return;
        }
        ui.SetActive(false);
        ui.closed?.Invoke(param);
    }
}
