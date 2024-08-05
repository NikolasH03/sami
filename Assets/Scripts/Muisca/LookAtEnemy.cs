using UnityEngine;

public class LookAtEnemy : MonoBehaviour
{
    public Transform player;
    private Transform currentTarget;

    void Update()
    {
        if (currentTarget != null)
        {
            Vector3 direction = (currentTarget.position - player.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            player.rotation = Quaternion.Slerp(player.rotation, lookRotation, Time.deltaTime * 5f);
        }
    }

    public void SetTarget(Transform target)
    {
        currentTarget = target;
    }

    public void ClearTarget()
    {
        currentTarget = null;
    }
}

