
using Salesman;
class Program
{
    static void Main()
    {

        int totalHoursSpent = 0;

        Console.WriteLine("Input the width of the grid.");
        int width = GetInput(4, 16);

        Console.WriteLine("Input the height of the grid.");
        int height = GetInput(4, 16);

        Console.WriteLine("Input the number of scenarios to run.");
        int numberOfScenarios = GetInput(2, 100);

        for (int i = 0; i < numberOfScenarios; i++)
        {
            Scenario myScenario = new(i, width, height);

            myScenario.Run();

            totalHoursSpent += myScenario.MyHoursSpent;
        }

        Console.WriteLine($"Total Hours Spent: {totalHoursSpent}");

        if (numberOfScenarios == 1)
        {
            Console.WriteLine($"Only one scenario ran, there is no average. Total Hours Spent: {totalHoursSpent}");
        }
        else
        {
            Console.WriteLine($"{numberOfScenarios} scenarios ran. Average Hours Spent: {totalHoursSpent / numberOfScenarios}");
        }

    }

    static int GetInput(int min, int max)
    {
        int input = 0;
        bool isValid = false;

        Console.WriteLine($"Please enter an integer between {min} and {max}:");

        while (!isValid)
        {
            string? userInput = Console.ReadLine();

            if (int.TryParse(userInput, out input) && input >= min && input <= max)
            {
                isValid = true;
            }
            else
            {
                Console.WriteLine($"Invalid input. Please enter an integer between {min} and {max}:");
            }
        }

        return input;
    }
}