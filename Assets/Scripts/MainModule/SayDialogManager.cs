using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SayDialogManager : MonoBehaviour
{
    public enum PrefabName
    {
        PrologueFlowchart = 0,
    }

    public static GameObject GetSayDialogPrefab(PrefabName name)
    {
        string prefabUrl = "Prefab/Flowchart_SayDialog/";
        GameObject prefab = Resources.Load(prefabUrl+name.ToString()) as GameObject;
        return prefab;
    }

    public static GameObject GetSayDialogPrefab(string name)
    {
        string prefabUrl = "Prefab/Flowchart_SayDialog/";
        GameObject prefab = Resources.Load(prefabUrl + name) as GameObject;
        return prefab;
    }

    public static GameObject InstantiateSayDialog(PrefabName name,Transform parent)
    {
        GameObject ins = Instantiate(GetSayDialogPrefab(name),Vector3.zero,Quaternion.identity,parent);
        ins.name = ins.name.Replace("(Clone)", "");
        return ins;
    }

    public static GameObject InstantiateSayDialog(string name, Transform parent)
    {
        GameObject ins = Instantiate(GetSayDialogPrefab(name), Vector3.zero, Quaternion.identity, parent);
        ins.name = ins.name.Replace("(Clone)", "");
        return ins;
    }

}
