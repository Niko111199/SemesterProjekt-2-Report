// bruges til at implementer subscription til juvel eventet

using UnityEngine;

interface IKronJuvelListener
{
    public void SubscribeToJuvelEvent();
    public void UnSubscribeToJuvelEvent();

    public void OnJuvelEvent();
}

// skrevet af: Peter
// valideret af: Victor