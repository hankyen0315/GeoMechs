using UnityEngine;

public class AnimatedUIElement : MonoBehaviour
{
    public virtual void OnAnimationStart()
    {
        gameObject.SetActive(true);
    }
    public virtual void OnAnimationEnd()
    {
        gameObject.SetActive(false);
        if (GameManager.Instance.State == GameManager.GameState.Pause)
        {
            GameManager.Instance.ResumeGame();
        }
    }
}
