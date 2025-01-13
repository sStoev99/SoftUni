using System.Text.RegularExpressions;

int[] beeGroups = Console.ReadLine().Split().Select(int.Parse).ToArray();
int[] beeEatersGroups = Console.ReadLine().Split().Select(int.Parse).ToArray();

Queue<int> bees = new Queue<int>(beeGroups);
Stack<int> beeEaters = new Stack<int>(beeEatersGroups);
int survivals = 0;

while (bees.Count > 0 && beeEaters.Count > 0)
{
    int currentBees = bees.Dequeue();
    int currentBeeEaters = beeEaters.Pop();

    int attackPower = currentBeeEaters * 7;
    int defencePower = currentBees;

    if (attackPower == defencePower)
        continue;

    else if (attackPower > defencePower)
    {
        attackPower -= defencePower;
        if (attackPower % 7 != 0)

            survivals = attackPower / 7 + 1;

        else
            survivals = attackPower / 7;

        if (beeEaters.Count > 0)
        {
            int i = beeEaters.Pop();
            i += survivals;
            beeEaters.Push(i);
        }

        else
            beeEaters.Push(survivals);
    }

    else if (attackPower < defencePower)
    {
        defencePower -= attackPower;
        currentBees = defencePower;
        bees.Enqueue(currentBees);
    }
}


Console.WriteLine("The final battle is over!");

if (bees.Count > 0)

    Console.WriteLine($"Bee groups left: {string.Join(", ", bees)}");

else if (beeEaters.Count > 0)
{

    Console.WriteLine($"Bee-eater groups left: {string.Join(", ", beeEaters)}");

}


else
    Console.WriteLine("But no one made it out alive!");


