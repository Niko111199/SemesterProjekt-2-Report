// burges til instancer der skal subscribe ti NavigationData

using UnityEngine;

public interface ISubscribeToNavigationData
{
    void SubscribeToNavigationData();
    void UnsubscribeToNavigationData();

    void OnNavigationDataUpdate(NavigationSystemData navData);

}
// skrevet af: Peter
// valideret af: Victor