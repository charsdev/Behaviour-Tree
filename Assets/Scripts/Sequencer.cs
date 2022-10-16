public class Sequencer : NodeWithChildren
{
	public override int Run()
	{
		for (int i = childrenPos; i < children.Count; i++)
		{
			int childState = children[i].Run();

            switch (childState)
            {
                case (int)States.FALSE:
                    childrenPos = 0;
                    return (int)States.FALSE;
                case (int)States.RUNNING:
                    return (int)States.RUNNING;
                case (int)States.TRUE:
                    childrenPos++;
                    break;
            }
        }

		childrenPos = 0;
		return (int)States.TRUE;
	}
}
