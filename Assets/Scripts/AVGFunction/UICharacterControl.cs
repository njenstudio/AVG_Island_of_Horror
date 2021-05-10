using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UICharacterControl : MonoBehaviour
{
    public bool isShowOnStart = false;
    public Vector3 defultScale = Vector3.one;
    public Vector2 defultPos;
    // Start is called before the first frame update
    void Start()
    {
        RectTransform trans = GetComponent<RectTransform>();
        gameObject.SetActive(isShowOnStart);
        trans.localScale = defultScale;
        trans.anchoredPosition = defultPos;

    }
}
