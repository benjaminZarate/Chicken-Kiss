using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryCondition : MonoBehaviour
{
    [SerializeField] GameObject vicText;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) {
            vicText.SetActive(true);
            StartCoroutine(Victory());
        }
    }

    IEnumerator Victory() {
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Z));
        SceneManager.LoadScene("Menu");
    }

}
