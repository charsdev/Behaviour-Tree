using UnityEngine;

public class QuestionMineAvailable : Leaf
{
    private const string mine = "Mine";
    public override int Run() => (GameObject.FindGameObjectsWithTag(mine).Length > 0) ? (int)States.TRUE : (int)States.FALSE;
}

