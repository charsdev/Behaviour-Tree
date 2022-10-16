public class ActionMine : Leaf
{
	public override int Run()
	{
		WorkerBehaviour minerScript = Miner.GetComponent<WorkerBehaviour>();
		minerScript.target = Utilities.GetClosestGameObjectByTag("Mine", minerScript.transform);

		if (minerScript.target == null)
		{
			return (int)States.FALSE;
		}

		if (minerScript.targetIndex == minerScript.PathToFollow.Count)
		{
			bool bagFull = minerScript.AddGold();
			bool mineEmpty = minerScript.target.GetComponent<Mine>().SubstractGold();

			if (bagFull || mineEmpty)
			{
				return (int)States.TRUE;
			}
        }

        return (int)States.RUNNING;
	}
}
