// Dette script definerer et interface kaldet IAddScore, som bruges til at tilføje point til en score.
// Klassen der implementerer dette interface skal selv definere hvordan AddScore metoden fungerer.
// Dette sikrer, at alle objekter der kan få point, har en ensartet måde at modtage dem på.

using UnityEngine;

public interface IAddScore
{
    public void AddScore(int amount);
}

// skrevet af: Peter
// valideret af: TODO valider
