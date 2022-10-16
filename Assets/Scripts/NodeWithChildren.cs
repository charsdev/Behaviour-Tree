using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NodeWithChildren : NodoBehaviourTree 
{
	public List<NodoBehaviourTree> children = new List<NodoBehaviourTree>();
	public int childrenPos;
}
