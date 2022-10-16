public class ActionGoToDeposit : Leaf
{
    public override int Run()
    {
        WorkerBehaviour workerScript = Miner.GetComponent<WorkerBehaviour>();
        workerScript.target = Utilities.GetClosestGameObjectByTag("Warehouse", workerScript.transform);

        if (workerScript.target == null)
        {
            return (int)States.FALSE;
        }

        if (workerScript.PathToFollow != null || workerScript.PathToFollow.Count > 0)
        {
            PathRequestManager.RequestPath(workerScript.gameObject, workerScript.target, workerScript.OnPathFound);
            return (int)States.TRUE;
        }

        return (int)States.RUNNING;
    }
}
