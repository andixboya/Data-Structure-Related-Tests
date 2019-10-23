using System;
using System.Collections.Generic;
using System.Linq;
using Wintellect.PowerCollections;

public class Computer : IComputer
{

    private Dictionary<int, List<LinkedListNode<Invader>>> byDistance;
    private LinkedList<Invader> byInsertion;

    private int energy;
    private int steps = 0;

    public Computer(int energy)
    {
        CheckIfBelowZero(energy);
        this.energy = energy;
        this.byDistance = new Dictionary<int, List<LinkedListNode<Invader>>>();
        this.byInsertion = new LinkedList<Invader>();
    }

    private void CheckIfBelowZero(int energy)
    {
        if (energy < 0)
        {
            throw new ArgumentException();
        }
    }

    public int Energy
    {
        get
        {
            return this.energy;
        }

        private set
        {
            if (value < 0)
            {
                this.energy = 0;
            }
            else
            {
                this.energy = value;
            }

        }
    }

    //it STACKS and is kept for EVERY SINGLE MOVEMENT?
    public void Skip(int turns)
    {
        steps += turns;

        this.byDistance = this.byDistance.Where((x) =>
        {

            int remDistance = x.Key - this.steps;

            if (remDistance <= 0)
            {
                this.Energy -= x.Value.Sum(y => y.Value.Damage);
                x.Value.ForEach(y => this.byInsertion.Remove(y));
            }

            return remDistance > 0;
        }).ToDictionary(x => x.Key, y => y.Value);
    }

    public void AddInvader(Invader invader)
    {
        var node = new LinkedListNode<Invader>(invader);
        if (!this.byDistance.ContainsKey(invader.Distance))
        {
            this.byDistance.Add(invader.Distance, new List<LinkedListNode<Invader>>());
        }
        this.byInsertion.AddLast(node);
        this.byDistance[invader.Distance].Add(node);

    }

    public void DestroyHighestPriorityTargets(int count)
    {
        foreach (var linkedListNode in this.byDistance.SelectMany(x => x.Value)
              .OrderBy(x => x.Value)
              .Take(count))
        {
            this.byInsertion.Remove(linkedListNode);
        }

        //delete from second 
        var newDict = this.byDistance.SelectMany(x => x.Value)
            .OrderBy(x => x.Value)
            .Skip(count);

        this.byDistance = new Dictionary<int, List<LinkedListNode<Invader>>>();
        foreach (var item in newDict)
        {
            if (!this.byDistance.ContainsKey(item.Value.Distance))
            {
                this.byDistance.Add(item.Value.Distance, new List<LinkedListNode<Invader>>());
            }

            this.byDistance[item.Value.Distance].Add(item);
        }


    }

    public void DestroyTargetsInRadius(int radius)
    {
        this.byDistance = this.byDistance
           .Where(x =>
           {
               bool result = x.Key - this.steps > radius;

                //delete from second 
                if (!result)
               {
                   x.Value.ForEach(y => this.byInsertion.Remove(y));
               }
                //delete from first
                return result;
           })
           .ToDictionary(x => x.Key, y => y.Value);
    }

    public IEnumerable<Invader> Invaders()
    {
        //we`ll use by order of input here 
        return this.byInsertion;
        //return this.byOrdeOfInput

    }
}
