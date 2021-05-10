// This code is part of the Fungus library (https://github.com/snozbot/fungus)
// It is released for free under the MIT open source license (https://github.com/snozbot/fungus/blob/master/LICENSE)

﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Fungus
{
    /// <summary>
    /// Supported modes for clicking through a Say Dialog.
    /// </summary>
    public enum ClickMode
    {
        /// <summary> Clicking disabled. </summary>
        Disabled,
        /// <summary> Click anywhere on screen to advance. </summary>
        ClickAnywhere,
        /// <summary> Click anywhere on Say Dialog to advance. </summary>
        ClickOnDialog,
        /// <summary> Click on continue button to advance. </summary>
        ClickOnButton,
        /// <summary> No need Click for continue to advance. </summary>
        AutoPlay,
        /// <summary> No need Click for continue to advance and no wait. </summary>
        Skip,
    }

    /// <summary>
    /// Input handler for say dialogs.
    /// </summary>
    public class DialogInput : MonoBehaviour
    {
        [Tooltip("Click to advance story")]
        [SerializeField] protected ClickMode clickMode;

        [Tooltip("Delay between consecutive clicks. Useful to prevent accidentally clicking through story.")]
        [SerializeField] protected float nextClickDelay = 0f;

        [Tooltip("Allow holding Cancel to fast forward text")]
        [SerializeField] protected bool cancelEnabled = true;

        [Tooltip("Ignore input if a Menu dialog is currently active")]
        [SerializeField] protected bool ignoreMenuClicks = true;
        public float waitAutoPerWordTime = 0.3f;
        protected bool dialogClickedFlag;

        protected bool nextLineInputFlag;

        protected float ignoreClickTimer;

        protected StandaloneInputModule currentStandaloneInputModule;

        protected Writer writer;

        protected virtual void Awake()
        {
            writer = GetComponent<Writer>();

            CheckEventSystem();
        }

        // There must be an Event System in the scene for Say and Menu input to work.
        // This method will automatically instantiate one if none exists.
        protected virtual void CheckEventSystem()
        {
            EventSystem eventSystem = GameObject.FindObjectOfType<EventSystem>();
            if (eventSystem == null)
            {
                // Auto spawn an Event System from the prefab
                GameObject prefab = Resources.Load<GameObject>("Prefabs/EventSystem");
                if (prefab != null)
                {
                    GameObject go = Instantiate(prefab) as GameObject;
                    go.name = "EventSystem";
                }
            }
        }
        float autoPlaytimer;
        protected virtual void Update()
        {
            if (EventSystem.current == null)
            {
                return;
            }

            if (currentStandaloneInputModule == null)
            {
                currentStandaloneInputModule = EventSystem.current.GetComponent<StandaloneInputModule>();
            }

            if (writer != null && writer.IsWriting)
            {
                if (Input.GetButtonDown(currentStandaloneInputModule.submitButton) ||
                    (cancelEnabled && Input.GetButton(currentStandaloneInputModule.cancelButton)))
                {
                    SetNextLineFlag();
                }
            }

            switch (clickMode)
            {
            case ClickMode.Disabled:
                break;
            case ClickMode.ClickAnywhere:
                if (Input.GetMouseButtonDown(0))
                {
                    SetNextLineFlag();
                }
                break;
            case ClickMode.ClickOnDialog:
                if (dialogClickedFlag)
                {
                    SetNextLineFlag();
                    dialogClickedFlag = false;
                }
                break;
            case ClickMode.AutoPlay:
                  
                    if (writer.IsWaitingForInput)
                    {
                        autoPlaytimer += Time.deltaTime;
                    }
                    float readWait = (writer.targetTextObject.GetComponent<Text>().text.Length * waitAutoPerWordTime);
                    float waitTime = SettingManager.AutoPlaySpeed + readWait;
                    if (autoPlaytimer>= waitTime)
                    {
                        autoPlaytimer = 0;
                        SetNextLineFlag();
                    }
                    Debug.Log("isW: = " + writer.IsWaitingForInput + " wait:" + readWait + " Length:" + writer.targetTextObject.GetComponent<Text>().text.Length);
                    break;
                case ClickMode.Skip:
                    Debug.Log("isW: = " + writer.IsWaitingForInput + " Timer:" + autoPlaytimer + " Speed:" + SettingManager.AutoPlaySpeed);
                    if (writer.IsWaitingForInput)
                    {
                        SetNextLineFlag();
                    }
                    break;
            }

            if (ignoreClickTimer > 0f)
            {
                ignoreClickTimer = Mathf.Max (ignoreClickTimer - Time.deltaTime, 0f);
            }

            if (ignoreMenuClicks)
            {
                // Ignore input events if a Menu is being displayed
                if (MenuDialog.ActiveMenuDialog != null && 
                    MenuDialog.ActiveMenuDialog.IsActive() &&
                    MenuDialog.ActiveMenuDialog.DisplayedOptionsCount > 0)
                {
                    dialogClickedFlag = false;
                    nextLineInputFlag = false;
                }
            }

            // Tell any listeners to move to the next line
            if (nextLineInputFlag)
            {
                var inputListeners = gameObject.GetComponentsInChildren<IDialogInputListener>();
                for (int i = 0; i < inputListeners.Length; i++)
                {
                    var inputListener = inputListeners[i];
                    inputListener.OnNextLineEvent();
                }
                nextLineInputFlag = false;
            }
        }

        #region Public members

        /// <summary>
        /// Trigger next line input event from script.
        /// </summary>
        [ContextMenu("SetNextLineFlag")]
        public virtual void SetNextLineFlag()
        {
            nextLineInputFlag = true;
            if (clickMode != ClickMode.Skip)
                writer.writingSpeed = SettingManager.TextSpeed;
            else
                writer.writingSpeed = 600;
        }

        /// <summary>
        /// Set the dialog clicked flag (usually from an Event Trigger component in the dialog UI).
        /// </summary>
        public virtual void SetDialogClickedFlag()
        {
            // Ignore repeat clicks for a short time to prevent accidentally clicking through the character dialogue
            if (ignoreClickTimer > 0f)
            {
                return;
            }
            ignoreClickTimer = nextClickDelay;
            Debug.Log("GetClickedFlag");
            // Only applies in Click On Dialog mode
            if (clickMode == ClickMode.ClickOnDialog)
            {
                dialogClickedFlag = true;
            }
            if(clickMode == ClickMode.Skip||clickMode == ClickMode.AutoPlay)
            {
                clickMode = ClickMode.ClickOnDialog;
            }
        }

        /// <summary>
        /// Sets the button clicked flag.
        /// </summary>
        public virtual void SetButtonClickedFlag()
        {
            // Only applies if clicking is not disabled
            if (clickMode != ClickMode.Disabled)
            {
                SetNextLineFlag();
            }
        }

        #endregion
    }
}
