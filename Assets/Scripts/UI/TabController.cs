using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UI;

public class TabController : MonoBehaviour
{   
    public UnityEngine.UI.Image[] tabs;
    public GameObject[] pages;

    void Start()
    {
        ActivateTab(0);
    }

    public void ActivateTab(int index) {
        for (int i = 0; i < pages.Length; ++i) {
            if (i == index) {
                pages[i].SetActive(true);
                tabs[i].color = Color.white;
            } else {
                pages[i].SetActive(false);
                tabs[i].color = Color.gray;
            }
        }
    }
}
