using UnityEngine;
using System.Collections;

public class ActionDeposit : Leaf
{
	public override int Run()
	{
		WorkerBehaviour minerScript = Miner.GetComponent<WorkerBehaviour>();
		GameObject target = Utilities.GetClosestGameObjectByTag("Warehouse", minerScript.transform);

		if (target == null)
		{
			return (int)States.FALSE;
		}

        if (minerScript.targetIndex >= minerScript.PathToFollow.Count
			&& minerScript.HasGold())
        {
            target.GetComponent<House>().AddGold();

            if (minerScript.SubstractGold()
				&& minerScript.GoldInBag == 0)
            {
                return (int)States.TRUE;
            }
        }

        return (int)States.RUNNING;
	}
}
