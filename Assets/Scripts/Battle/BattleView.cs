using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BattleView : MonoBehaviour
{
    public Text stableText;
    public Text knownText;
    public GameObject BlackDisruptIcon;
    public GameObject EventIcon;
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
