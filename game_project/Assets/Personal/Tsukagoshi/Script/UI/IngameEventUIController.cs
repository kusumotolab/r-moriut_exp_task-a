using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngameEventUIController : MonoBehaviour
{
    [SerializeField]
    GameObject WarningUI;

    [SerializeField]
    EventHandler eventHandler;

    // Start is called before the first frame update
    void Start()
    {
        WarningUI.gameObject.SetActive(false);
        eventHandler.EventOccurrence += OnEventOccured;
    }

    private void OnEventOccured(int i)
    {
        IEnumerator routine()
        {
            WarningUI.gameObject.SetActive(true);
            yield return new WaitForSeconds(3f);
            WarningUI.gameObject.SetActive(false);
        }

        StartCoroutine(routine());
    }
}
