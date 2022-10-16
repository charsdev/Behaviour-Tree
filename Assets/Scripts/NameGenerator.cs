using UnityEngine;
using UnityEngine.UI;

public class NameGenerator : MonoBehaviour
{
	public Canvas Canvas;
	public Text ObjectName;
	public GameObject Reference;
	private Vector2 _screenposition;

	private void Start()
	{
		GameObject GO = Instantiate(Reference);
		GO.name = "UI - " + gameObject.name;
		ObjectName = GO.GetComponent<Text>();
		ObjectName.text = gameObject.name;
		ObjectName.transform.SetParent(Canvas.transform, false);
		_screenposition = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y + 0.2f);
		ObjectName.transform.position = Camera.main.WorldToScreenPoint(_screenposition);
	}
}
