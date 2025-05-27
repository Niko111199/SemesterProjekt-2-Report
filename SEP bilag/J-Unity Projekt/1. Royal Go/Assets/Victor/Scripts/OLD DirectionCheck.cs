using UnityEngine;
/*
 * Scriptet er ekslusivt udregning og udnytter derfor ikke monobehaviour, udefra kan isWithinLookRange() kaldes som vil output enten true eller false afhængig om de givne parametre viser om spilleren kigger tæt nok i den rigtige retning af objektet.
 * playerPos og targetPos skal være henholdsvis latitude og longitude i grader, spillerens lat og long kan findes ved bruges af WorldPos klassen, playerDirection kan fås fra Compas klassen og fieldOfView definerer i grader synsfeltet af søgningen.
 * For formlerne for bearing er følgende kilde brugt: https://www.movable-type.co.uk/scripts/latlong.html
 */
public class OLDDirectionCheck
{

    //ændring lavet til at inkludere indbygget rader til raddianer, for mere læslig kode
    public bool isWithinLookRange(float playerDirection, Vector2 playerPos, Vector2 targetPos, float fieldOfView)
    {
        //Converts lat and lon to radians for calculation:
        //playerPos = new Vector2(DegreesToRadians(playerPos.x),DegreesToRadians(playerPos.y));
        //targetPos = new Vector2(DegreesToRadians(targetPos.x),DegreesToRadians(targetPos.y));

        playerPos = new Vector2(playerPos.x * Mathf.Deg2Rad, playerPos.y * Mathf.Deg2Rad);
        targetPos = new Vector2(targetPos.x * Mathf.Deg2Rad, targetPos.y * Mathf.Deg2Rad);

        float y = Mathf.Sin(targetPos.y - playerPos.y) * Mathf.Cos(targetPos.x);
        float x = Mathf.Cos(playerPos.x) * Mathf.Sin(targetPos.x) -
                           Mathf.Sin(playerPos.x) * Mathf.Cos(playerPos.y) * Mathf.Cos(targetPos.y - playerPos.y);
        float teta = Mathf.Atan2(y, x);

        //Convert to degrees on a compas:
        float d = (teta * Mathf.Rad2Deg + 360) % 360;

        return d <= Mathf.Clamp(playerDirection + (fieldOfView/2), 0, 360) ||
               d >= Mathf.Clamp(playerDirection - (fieldOfView/2), 0, 360);

    } 

   /* private float DegreesToRadians(float degrees)
    {
        return degrees * Mathf.PI / 180.0f;
    }

    private float RadiansToDegrees(float radians)
    {
        return radians * 180.0f / Mathf.PI;
    }
   */
}

/*
  * Valideringsstatus: Valideret af Nikolaj Bræmer
  * Skrevet af: Victor
*/