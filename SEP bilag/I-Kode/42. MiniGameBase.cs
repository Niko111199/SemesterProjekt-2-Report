//dette er en abstract klasse af mingames, som eksistere for at give de nedarvende klasser monobehavior og IMingame

using UnityEngine;

public abstract class MiniGameBase : MonoBehaviour, IMiniGame
{
    public abstract string name { get; }
    public abstract void Play();
}

//skrevet af Nikolaj Bræmer
//Valideret af: Victor