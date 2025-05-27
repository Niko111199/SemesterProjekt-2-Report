//dette er interfacet i MiniGame strategy pattern som er implementeret i alle minigames

using UnityEngine;

public interface IMiniGame
{
    string name { get; }
    void Play();
}

//Skrevet af Nikolaj Bræmer
//Valideret af: Victor