// bruges til at implementere timer klassen

interface IHasTimer
{
    Timer timer { get; set; }

    Timer OnTimerTrigger();
}
// skrevet af: Peter
// valideret af: Victor