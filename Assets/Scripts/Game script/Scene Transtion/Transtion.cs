using System.Collections;using UnityEngine;
using UnityEngine.SceneManagement;


public class Transation : MonoBehaviour
{

    public bool scenechange;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(loadscene());
            StartCoroutine(destroye());


        }


    }





    IEnumerator loadscene()
    {
        yield return new WaitForSeconds(0.8f);
        SceneManager.LoadScene("Level_2.1");

    }
    IEnumerator destroye()
    {
        yield return new WaitForSeconds(0.9f);
        Destroy(this.gameObject);

    }


}