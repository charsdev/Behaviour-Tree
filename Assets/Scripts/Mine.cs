using UnityEngine;
using System.Collections.Generic;

public class Mine : MonoBehaviour
{
    public int GoldInMine = 1000;
    public List<WorkerBehaviour> WorkerIntoTheMine = new List<WorkerBehaviour>();
    [SerializeField] private NameGenerator _nameGenerator;

    private void Awake()
    {
       
    }

    private void Update()
    {
        if (GoldInMine > 0)
        {
            return;
        }

        Disable();
    }

    public void Disable()
    {
        MinesManager.Instance.deactivateMines.Add(gameObject);
        MinesManager.Instance.activateMines.Remove(gameObject);
        _nameGenerator.ObjectName.gameObject.SetActive(false);
        WorkerIntoTheMine.Clear();
        gameObject.SetActive(false);
    }

    public void OnEnable()
    {
        _nameGenerator.ObjectName?.gameObject.SetActive(true);
        GoldInMine = 100;
    }

    public void AddWorker(WorkerBehaviour go) => WorkerIntoTheMine.Add(go);
    public void RemoveWorker(WorkerBehaviour go) => WorkerIntoTheMine.Remove(go);
    public bool SubstractGold() => --GoldInMine <= 0;
}

