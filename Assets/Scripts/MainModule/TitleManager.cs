using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleManager : MonoBehaviour
{
    public Animator LogoTitleAnimator;
    public Animator BGAnimator;
    public Animator MenuAnimator;
    bool isOpen = false;
    // Start is called before the first frame update
    void Start()
    {
        LogoTitleAnimator.SetTrigger("In");
    }
    public void GetAnyClick()
    {
        if (!isOpen)
        {
            LogoTitleAnimator.SetTrigger("Out");
            BGAnimator.SetTrigger("Out");
            MenuAnimator.SetTrigger("In");
            isOpen = true;
        }
        else if (isOpen)
        {
            LogoTitleAnimator.SetTrigger("In");
            BGAnimator.SetTrigger("In");
            MenuAnimator.SetTrigger("Out");
            isOpen = false;
        }
    }
    // Update is called once per frame
    void Update()
    {
        /*
        if(Input.GetMouseButtonDown(0) && !isOpen)
        {
            LogoTitleAnimator.SetTrigger("Out");
            BGAnimator.SetTrigger("Out");
            MenuAnimator.SetTrigger("In");
            isOpen = true;
        }
        else if (Input.GetMouseButtonDown(0) && isOpen)
        {
            LogoTitleAnimator.SetTrigger("In");
            BGAnimator.SetTrigger("In");
            MenuAnimator.SetTrigger("Out");
            isOpen = false;
        }
        */
    }
}
