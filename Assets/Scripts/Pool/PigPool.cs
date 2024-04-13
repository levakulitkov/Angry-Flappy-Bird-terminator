using System;

public class PigPool : Pool<Pig>
{
    public override void Reset()
    {
        Pig[] pigs = FindObjectsOfType<Pig>(true);

        foreach (Pig pig in pigs)
            pig.Reset();

        base.Reset();
    }
}
