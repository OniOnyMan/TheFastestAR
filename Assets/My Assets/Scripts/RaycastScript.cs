using UnityEngine;

public class RaycastScript : MonoBehaviour
{
    public bool IsBang = true;

    public GameObject PlayerOne;
    public GameObject PlayerTwo;

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && IsBang)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                var hittedGameObject = hit.transform.gameObject;
                Debug.Log(hittedGameObject.name + " hitted");
                if (hittedGameObject == PlayerOne)
                {
                    if (PlayerTwo != null)
                        PlayerTwo.GetComponent<PlayerController>().Hitted();

                }
                else if (hittedGameObject == PlayerTwo)
                {
                    if (PlayerOne != null)
                        PlayerOne.GetComponent<PlayerController>().Hitted();
                }
            }
        }
    }
}
