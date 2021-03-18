using UnityEngine;

public delegate void OnEnter(PathLine pathLine);
public delegate void ChekExit(PathLine pathLine);
public class PathLine : MonoBehaviour
{
    public int Index;
    public bool IsPassed;
    public event OnEnter OnEnterEvent;
    public event ChekExit ChekExitEvent;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!IsPassed)
            {
                IsPassed = true;
                OnEnterEvent?.Invoke(this);                
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //при выходе проверять равен ли текущий старому
            ChekExitEvent(this);
        }
    }
}
