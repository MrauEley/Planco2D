using UnityEngine;

public class ChangeState : MonoBehaviour
{

    public void StateChange()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }
}
