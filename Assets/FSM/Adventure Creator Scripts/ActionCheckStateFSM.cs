/*
 *
 *	Adventure Creator
 *	by Chris Burton, 2013-2022
 *	
 *	"ActionCheckTemplate.cs"
 * 
 *	This is a blank action template, which has two outputs.
 * 
 */

using UnityEngine;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace AC
{

	[System.Serializable]
	public class ActionCheckStateFSM : ActionCheck
	{

		// Declare properties here
		public override ActionCategory Category { get { return ActionCategory.Object; } }
		public override string Title { get { return "Check State of FSM"; } }
		public override string Description { get { return "Checks is the stated state is the current active one."; } }


		// Declare variables here
		public ActiveStateFullHashPath activeState;
		public string stateName;
		//public bool isCurrentState;

		public int constantID;

		public override bool CheckCondition ()
		{
			// Return 'true' if the condition is met, and 'false' if it is not met.
			return activeState.IsCurrentState(stateName);
		}

		
		#if UNITY_EDITOR

		public override void ShowGUI ()
		{
			// Action-specific Inspector GUI code here.  The "Condition is met" / "Condition is not met" GUI is rendered automatically afterwards.
			activeState = (ActiveStateFullHashPath)EditorGUILayout.ObjectField("Plot Thread FSM:", activeState, typeof(ActiveStateFullHashPath), true);
			stateName = EditorGUILayout.TextField("State to compare:", stateName);

			constantID = FieldToID(activeState, constantID);
			activeState = IDToField(activeState, constantID, true);
		}
		

		public override string SetLabel ()
		{
			// (Optional) Return a string used to describe the specific action's job.
			
			return string.Empty;
		}

		override public void AssignValues()
		{
			activeState = AssignFile(constantID, activeState);
		}

		#endif

	}

}