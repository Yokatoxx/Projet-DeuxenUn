using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuEnd : MonoBehaviour
{

    public NombreDeVague nbWave;

    public void TextWave()
    {
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }


}
