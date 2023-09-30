using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[RequireComponent(typeof(Animator))]
public class ActiveStateFullHashPath : MonoBehaviour
{
    #region Attrubutes

    [SerializeField] private Animator animator;
    [SerializeField] private AnimatorClipInfo[] clipInfo;
    [Tooltip("USE: if(activeStateHashName == Animator.StringToHash(\"Base Layer.\" + yourString) ")]
    public int activeStateHashName; 

    #endregion

    void OnValidate()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Application.isEditor)
        {
            //do stuff
        }

        SetActiveStateFullHashPath();
    }

    /// <summary>
    /// Clears all pending triggers, and then trigers the desired one.
    /// </summary>
    /// <param name="triggerName"></param>
    public void Trigger (string triggerName)
    {
        ResetAllTriggers(); //clean the stuck triggers
        animator.SetTrigger(triggerName);
    }

    /// <summary>
    /// For some reason, the trigers that are not imedialtely activated, 
    /// wait forever to be used, defeating the purpose of using them in this Plot Thread implementation
    /// This resets all the trigers, to fix the issue.
    /// </summary>
    private void ResetAllTriggers()
    {
        foreach (var trigger in animator.parameters)
        {
            if (trigger.type == AnimatorControllerParameterType.Trigger)
            {
                animator.ResetTrigger(trigger.name);
            }
        }
    }
    private void SetActiveStateFullHashPath()
    {
        // get the HashFullPath (Name)
        // TODO: check if ful path is like: "Base Layer.state Name"
        int layerIndex = 0;  // 0 is Base Layer
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(layerIndex);
        activeStateHashName = stateInfo.fullPathHash;

        return;  
    }

    public bool IsCurrentState (string stateName)
    {
        if (activeStateHashName == Animator.StringToHash("Base Layer." + stateName))
        {
            return true;
        }
        return false;
    }
}
