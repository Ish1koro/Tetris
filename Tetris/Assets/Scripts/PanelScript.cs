using UnityEngine;
/// <summary>
/// 設定画面のScript
/// </summary>
public class PanelScript : MonoBehaviour
{
    [SerializeField] private GameObject playpanel = null;
    [SerializeField] private GameObject keyboardpanel = null;
    [SerializeField] private GameObject controllerpanel = null;
    [SerializeField] private GameObject videopanel = null;
    [SerializeField] private GameObject audiopanel = null;
    private GameObject falsepanel = null;
    private void Update()
    {
        falsepanel = GameObject.FindWithTag("Panel");
    }
    public void PlaySelect()
    {
        playpanel.SetActive(true);
    }
    public void KeyBoardSelect()
    {
        keyboardpanel.SetActive(true);
    }
    public void ControlerSelect()
    {
        controllerpanel.SetActive(true);
    }
    public void VideoSelect()
    {
        videopanel.SetActive(true);
    }
    public void AudioSelect()
    {
        audiopanel.SetActive(true);
    }
    public void UnSelect()
    {
        falsepanel.SetActive(false);
    }
}
