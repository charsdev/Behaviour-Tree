using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class MinesManager : MonoBehaviour
{
	public List<GameObject> deactivateMines = new List<GameObject>();
	public List<GameObject> activateMines = new List<GameObject>();
	public static MinesManager Instance;

	private void Awake()
	{
		Instance = this;
		activateMines = GameObject.FindGameObjectsWithTag("Mine").ToList();
	}

    public void Update()
    {
		if (activateMines.Count == 0 && deactivateMines.Count > 1)
        {
			activateMines.AddRange(deactivateMines);
			deactivateMines.Clear();
			foreach (var ac in activateMines)
			{
				ac.SetActive(true);
			}
		}
	}
}


