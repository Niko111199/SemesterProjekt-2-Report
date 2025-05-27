using UnityEngine;

public class NavigationSystemData
{
    public float horizontalAccuracy { get; private set; }
    public float latitude { get; private set; }
    public float longitude { get; private set; }
    public double timestamp   { get; private set;}
    public bool isInRangeOfEvent { get; private set; }
    public bool isLookingTowardsEvent { get; private set; }
    public float headingDirection { get; private set; }
    public double targetDirection { get; private set; }
    public float distanceToTarget { get; private set; }

    public NavigationSystemData()
    {

    }

    public NavigationSystemData(
        float horizontalAccuracy,
        float latitude,
        float longitude,
        double timestamp,
        bool isInRangeOfEvent,
        bool isLookingTowardsEvent,
        float headingDirection,
        double targetDirection,
        float distanceToTarget

        )
    {
        this.horizontalAccuracy = horizontalAccuracy;
        this.latitude = latitude;
        this.longitude = longitude;
        this.timestamp = timestamp;
        this.isInRangeOfEvent = isInRangeOfEvent;
        this.isLookingTowardsEvent = isLookingTowardsEvent;
        this.targetDirection = targetDirection;
        this.headingDirection = headingDirection;
        this.targetDirection = targetDirection;
        this.distanceToTarget = distanceToTarget;
    }

}
