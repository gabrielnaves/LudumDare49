using UnityEngine;

public class DespawnOnAnimatorStateEnter : StateMachineBehaviour
{
    public float delay = 0;
    public string despawnedTrigger = "Despawned";

    private float elapsedTime;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        elapsedTime = 0;
        if (delay <= 0)
            Despawn(animator);
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime > delay)
            Despawn(animator);
    }

    private void Despawn(Animator animator)
    {
        SimplePool.Despawn(animator.gameObject);
        animator.SetTrigger(despawnedTrigger);
    }
}
