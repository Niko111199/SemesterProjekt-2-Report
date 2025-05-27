using UnityEngine;

/*
 * Et simpelt script som subscriber til navigationsystemdata eventet og bruger de to værdier: "headingDirection" for spillerens nuværende retning &
 * "targetDirection" for retningen af næste kongekrone objekt. 
 */

public class DirectionPresenter : MonoBehaviour
{
    [SerializeField] private Transform playerDirectionArrow;
    [SerializeField] private Transform targetDirectionPointer;

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
        if (data.headingDirection >= 0 && data.headingDirection < 360)
            playerDirectionArrow.rotation = Quaternion.Euler(0f, 0f, data.headingDirection);
        else
        {
            Debug.LogWarning("Player heading direction outside expected range: 0-359");
                playerDirectionArrow.rotation = Quaternion.Euler(0f, 0f, 0f);
        }

        if (data.targetDirection >= 0 && data.targetDirection <= 359)
            targetDirectionPointer.rotation = Quaternion.Euler(0f, 0f, (float) data.targetDirection);
        else
        {
            Debug.LogWarning("Target direction outside expected range: 0-359");
            targetDirectionPointer.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
    }
}

/*
 * Skrevet af: Victor
 * Valideringsstatus: Ikke valideret
 */