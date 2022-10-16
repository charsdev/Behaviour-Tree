using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerBehaviour : MonoBehaviour
{
    #region private variables
    private int _maxGold = 100;
    #endregion

    #region public variables
    public Sequencer mainSeq;
    public List<GameObject> PathToFollow;
    public int GoldInBag;
    public int speed;
    public int targetIndex;
    public GameObject target;
    #endregion

    private void Start()
    {
        CreateBehaviourTree();
    }

    private void Update()
    {
        mainSeq.Run();
    }

    private void CreateBehaviourTree()
    {
        mainSeq = new Sequencer();

        Sequencer mineSeq = new Sequencer();
        mainSeq.children.Add(mineSeq);

        QuestionMineAvailable availableQuestion = new QuestionMineAvailable();
        mineSeq.children.Add(availableQuestion);

        ActionGoToMine gotoMineAction = new ActionGoToMine();
        mineSeq.children.Add(gotoMineAction);

        ActionMine mineAction = new ActionMine();
        mineSeq.children.Add(mineAction);

        Sequencer depositSeq = new Sequencer();
        mainSeq.children.Add(depositSeq);

        ActionGoToDeposit gotoDepositAction = new ActionGoToDeposit();
        depositSeq.children.Add(gotoDepositAction);

        ActionDeposit depositAction = new ActionDeposit();
        depositSeq.children.Add(depositAction);

        mainSeq.Miner = gameObject;
        mineSeq.Miner = gameObject;
        availableQuestion.Miner = gameObject;
        gotoMineAction.Miner = gameObject;
        mineAction.Miner = gameObject;
        depositSeq.Miner = gameObject;
        gotoDepositAction.Miner = gameObject;
        depositAction.Miner = gameObject;
    }

    public void OnPathFound(List<GameObject> newPath, bool pathSuccessful)
    {
        if (pathSuccessful)
        {
            PathToFollow = newPath;
            targetIndex = 0;
            StopCoroutine(FollowPath());
            StartCoroutine(FollowPath());
        }
    }

    public IEnumerator FollowPath()
    {
        Vector3 currentWaypoint = PathToFollow[0].transform.position;
        while (targetIndex < PathToFollow.Count)
        {
            if (transform.position == currentWaypoint)
            {
                targetIndex++;
                if (targetIndex >= PathToFollow.Count)
                {
                    yield break;
                }
                currentWaypoint = PathToFollow[targetIndex].transform.position;
            }

            transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, speed * Time.deltaTime);
            yield return null;
        }
    }

    public bool AddGold() => ++GoldInBag >= _maxGold;
    public bool SubstractGold() => --GoldInBag <= 0;
    public bool HasGold() => GoldInBag > 0;
}
