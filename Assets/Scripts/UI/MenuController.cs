using UnityEngine;

public class MenuController : MonoBehaviour
{
    public GameObject mainMenu;
    void Start()
    {
        mainMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab)) {
            mainMenu.SetActive(!mainMenu.activeSelf);
        }
    }
}
