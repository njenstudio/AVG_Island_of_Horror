using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class OnAnimationEnd : MonoBehaviour {

	public UnityEvent OnAniEnd;

    Animation ani;
    Animator anitor;
    void Start()
    {
        ani = gameObject.GetComponent<Animation>();
        anitor = gameObject.GetComponent<Animator>();
    }

    void Update () {
        if(ani!=null)
        {
            if (!ani.isPlaying)
            {
                OnAniEnd.Invoke();
                enabled = false;
            }
        }
        else if(anitor!=null)
        {
            if (anitor.GetCurrentAnimatorStateInfo(0).IsTag("Played"))
            {
                OnAniEnd.Invoke();
                enabled = false;
            }
        }
	}

    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
