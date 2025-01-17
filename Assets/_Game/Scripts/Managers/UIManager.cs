using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] Dictionary<System.Type, UICanvas> canvasActives = new();
    Dictionary<System.Type, UICanvas> canvasPrefabs = new();

    [SerializeField] Transform parent;
    public static UIManager instance;

    private void Awake()
    {
        instance = this;
        UICanvas[] prefabs = Resources.LoadAll<UICanvas>("UI/");
        for (int i = 0; i < prefabs.Length; i++)
        {
            canvasPrefabs.Add(prefabs[i].GetType(), prefabs[i]);
        }
    }
    public T OpenUI<T>() where T : UICanvas
    {
        T canvas = GetUI<T>();
        canvas.Setup();
        canvas.Open();
        return canvas;
    }

    public void CloseUI<T>(float time) where T : UICanvas
    {
        if (IsLoadedUI<T>())
        {
            canvasActives[typeof(T)].Close(time);
        }
    }

    public void CloseUIForced<T>() where T : UICanvas
    {
        canvasActives[typeof(T)].Close(0);
    }

    public bool IsLoadedUI<T>() where T : UICanvas
    {
        return canvasActives.ContainsKey(typeof(T)) && canvasActives[typeof(T)] != null;
    }

    public bool IsOpenUI<T>() where T : UICanvas
    {
        return IsLoadedUI<T>() && canvasActives[typeof(T)].gameObject.activeSelf;
    }

    public T GetUI<T>() where T : UICanvas
    {
        if (!IsLoadedUI<T>())
        {
            T prefab = GetUIPrefab<T>();
            T canvas = Instantiate(prefab, parent);
            canvasActives[typeof(T)] = canvas;
        }

        return canvasActives[typeof(T)] as T;
    }

    private T GetUIPrefab<T>() where T : UICanvas
    {
        return canvasPrefabs[typeof(T)] as T;

    }

    public void CloseAllUI()
    {
        foreach (var canvas in canvasActives)
        {
            if (canvas.Value && canvas.Value.gameObject.activeSelf)
            {
                canvas.Value.Close(0);
            }
        }
    }
}
