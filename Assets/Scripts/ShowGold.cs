using UnityEngine;
using UnityEngine.UI;

public class ShowGold : MonoBehaviour
{
    private Text ObjectText;
    public GameObject Reference;
    public Canvas Canvas;

    private void Start()
    {
        GameObject GO = Instantiate(Reference);
        ObjectText = GO.GetComponent<Text>();
        ObjectText.transform.SetParent(Canvas.transform, false);
        GO.name = "ShowGold";
    }

    private void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

        if (hit.collider != null && hit.collider.tag != "Node")
        {
            ObjectText.gameObject.SetActive(true);

            if (hit.collider.tag == "Mine")
            {
                ObjectText.text = hit.collider.GetComponent<Mine>().GoldInMine.ToString();
            }
            else if (hit.collider.tag == "Player")
            {
                ObjectText.text = hit.collider.GetComponent<WorkerBehaviour>().GoldInBag.ToString();
            }
            else if (hit.collider.name == "Warehouse")
            {
                ObjectText.text = hit.collider.GetComponent<House>().GoldInHouse.ToString();
            }

            Vector3 newPos = hit.transform.position;
            newPos.z = -1;
            ObjectText.transform.position = Camera.main.WorldToScreenPoint(newPos);
        }
        else
        {
            ObjectText.gameObject.SetActive(false);
        }
    }
}

