using UnityEngine;

public class PauseSystem : MonoBehaviour
{
    [SerializeField] private GameObject pauseUI = null;

    private bool _ispause = true;
    private void Update()
    {
        if (Input.GetButtonDown("Fire3"))
        {
            if(_ispause)
            {
                Time.timeScale = 0;
                pauseUI.SetActive(true);
                _ispause = false;
            }
            else
            {
                Time.timeScale = 1;
                pauseUI.SetActive(false);
                _ispause = true;
            }
        }
    }
}
