using System;

public class Invader : IInvader
{
    private int damage;
    private int distance;

    public Invader(int damage, int distance)
    {
        this.damage = damage;
        this.distance = distance;
    }

    public int Damage
    {
        get
        {
            return this.damage;
        }
        set
        {
            this.damage = value;
        }
    }
    public int Distance
    {
        get
        {
            return this.distance;
        }
        set
        {
            this.distance = value;
        }
    }

    public int CompareTo(IInvader other)
    {
        int result = this.distance.CompareTo(other.Distance);

        if (result != 0)
        {
            return result;
        }

        result = other.Damage.CompareTo(this.Damage);

        return result;

    }
}
