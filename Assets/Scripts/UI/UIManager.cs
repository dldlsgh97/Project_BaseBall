using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private List<UIBase> uiList;

    public void Show<T>(params object[] param) where T : UIBase
    {
        UIBase ui = null;
        foreach(UIBase obj in uiList)
        {
            if(obj.name == typeof(T).ToString())
            {
                ui = obj;
                break;
            }
        }

        if (ui != null)
        {
            ui.SetActive(true);
            ui.opened.Invoke(param);
        }

    }
    public void Hide<T>(params object[] param) where T : UIBase
    {
        UIBase ui = null;
        foreach (UIBase obj in uiList)
        {
            if (obj.name == typeof(T).ToString())
            {
                ui = obj;
                break;
            }
        }
        if (ui != null)
        {
            ui.SetActive(false);
            ui.closed.Invoke(param);
        }
    }
}
