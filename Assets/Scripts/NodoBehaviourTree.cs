using UnityEngine;

public class NodoBehaviourTree
{
	public GameObject Miner;
    public WorkerBehaviour WorkerBehaviour;
    public virtual int Run() => 0;
}

