public class ActionGoToMine : Leaf
{
    public override int Run()
    {
        WorkerBehaviour workerScript = Miner.GetComponent<WorkerBehaviour>();
        workerScript.target = Utilities.GetClosestGameObjectByTag("Mine", workerScript.transform);

        if (workerScript.target == null)
        {
            return (int)States.FALSE;
        }

        Mine mine = workerScript.target.GetComponent<Mine>();
        workerScript.PathToFollow.Clear();

        if (mine.WorkerIntoTheMine.Count == 1)
        {
            return (int)States.FALSE;
        }
        else
        {
            mine.AddWorker(workerScript);
        }

        if (workerScript.PathToFollow == null || workerScript.PathToFollow.Count == 0)
        {
            PathRequestManager.RequestPath(workerScript.gameObject, workerScript.target, workerScript.OnPathFound);
            return (int)States.TRUE;
        }

        return (int)States.RUNNING;
    }
}

