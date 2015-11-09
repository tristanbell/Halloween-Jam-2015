using UnityEngine;
using System.Collections;

public class SingleAttackBehaviour : StateMachineBehaviour 
{
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		if (stateInfo.normalizedTime > 0.7f) 
		{
			animator.SetBool ("Is Attacking", false);
		}
	}
}
