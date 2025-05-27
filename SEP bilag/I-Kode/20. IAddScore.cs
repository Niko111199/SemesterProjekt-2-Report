// Dette script definerer et interface kaldet IAddScore, som bruges til at tilf�je point til en score.
// Klassen der implementerer dette interface skal selv definere hvordan AddScore metoden fungerer.
// Dette sikrer, at alle objekter der kan f� point, har en ensartet m�de at modtage dem p�.

using UnityEngine;

public interface IAddScore
{
    public void AddScore(int amount);
}

// skrevet af: Peter
// valideret af: TODO valider
