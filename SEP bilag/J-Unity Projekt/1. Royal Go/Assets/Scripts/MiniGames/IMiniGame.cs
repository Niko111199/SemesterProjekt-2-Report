//dette er interfacet i MiniGame strategy pattern som er implementeret i alle minigames

using UnityEngine;

public interface IMiniGame
{
    string name { get; }
    void Play();
}

//Skrevet af Nikolaj Br�mer
//Valideret af: Victor