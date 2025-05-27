//TODO tag stilling til om dette script skal slettes, bruges vist ikke.

using UnityEngine;

public class CompasPresenter : MonoBehaviour
{
    private void OnEnable()
    {
        NavigationSystem.OnNavigationSystemEvent += HandleNavigationSystemEvent;
    }

    private void OnDisable()
    {
        NavigationSystem.OnNavigationSystemEvent -= HandleNavigationSystemEvent;
    }

    private void HandleNavigationSystemEvent(NavigationSystemData data)
    {
        // TODO: Implement event handling logic here
    }
}
