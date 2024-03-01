using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerDead : MonoBehaviour
{
    IEnumerator deadScene(){
        yield return new WaitForSeconds(1.44f);
        SceneManager.LoadScene(2);
    }
}
