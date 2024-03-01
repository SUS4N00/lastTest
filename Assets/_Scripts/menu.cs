using UnityEngine;
using UnityEngine.SceneManagement;
public class menu : MonoBehaviour
{
    public void choi(){
        SceneManager.LoadScene(1);
    }
    public void thoatRaMenu(){
        SceneManager.LoadScene(0);
    }
    public void thoat(){
        Application.Quit();
    }
}
