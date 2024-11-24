namespace Salesman
{

    public class Scenario
    {

        private readonly int MyID;
        public int MyHoursSpent { get; private set; }
        private readonly House[][] MyHouses;
        private readonly List<Person> MyPersons;
        public Scenario(int id, int width, int height)
        {

            MyID = id;
            MyHoursSpent = 0;

            MyPersons = [];

            MyHouses = new House[height][];
            for (int i = 0; i < height; i++)
            {

                MyHouses[i] = new House[width];
                for (int j = 0; j < width; j++)
                {

                    var house = new House(i * width + j);
                    var person = new Person(MyPersons.Count);

                    house.AddPerson(person);
                    person.CurrentHouse = house;

                    MyHouses[i][j] = house;
                    MyPersons.Add(person);
                }
            }
        }

        public void Run()
        {
            Console.WriteLine($"-- Running Scenario ID: {MyID} --");

            Console.WriteLine("Initial House Layout");
            PrintHouseLayout();

            Console.WriteLine("Add initial salesman");
            AddSalesman();

            Console.WriteLine("House Layout with initial Salesman");
            PrintHouseLayout();

            CheckForPeopleToConvert();

            Console.WriteLine("House Layout after conversion");
            PrintHouseLayout();

            while (!IsAllPersonsSalesmen())
            {
                CheckForSalesmenToMove();
                MoveSalesmen();

                Console.WriteLine("House Layout after movement");
                PrintHouseLayout();

                CheckForPeopleToConvert();

                Console.WriteLine("House Layout after conversion");
                PrintHouseLayout();
            }

            Console.WriteLine($"-- Scenario ID: {MyID} Complete --");
        }

        public House GetRandomCornerHouse()
        {
            House[] cornerHouses =
            [
                MyHouses[0][0],
                MyHouses[0][^1],
                MyHouses[^1][0],
                MyHouses[^1][^1]
            ];

            Random random = new();

            int index = random.Next(cornerHouses.Length);

            return cornerHouses[index];
        }

        public void AddSalesman()
        {
            Person salesman = new(MyPersons.Count, false);

            House startingHouse = GetRandomCornerHouse();
            startingHouse.AddPerson(salesman);
            MyPersons.Add(salesman);

            salesman.CurrentHouse = startingHouse;

            MyHoursSpent++;
        }

        public void PrintHouseLayout()
        {
            for (int i = 0; i < MyHouses.Length; i++)
            {
                for (int j = 0; j < MyHouses[i].Length; j++)
                {
                    Console.Write(MyHouses[i][j].PrintString() + " ");
                }
                Console.WriteLine();
            }
        }

        public bool IsAllPersonsSalesmen()
        {
            foreach (Person person in MyPersons)
            {
                if (person.MyIsUnemployed)
                {
                    return false;
                }
            }

            return true;
        }


        public void CheckForPeopleToConvert()
        {
            for (int i = 0; i < MyHouses.Length; i++)
            {
                for (int j = 0; j < MyHouses[i].Length; j++)
                {
                    if (MyHouses[i][j].HasSalesmen())
                    {
                        MyHouses[i][j].ConvertToSalesmen();
                    }
                }
            }
        }

        public List<House> GetAdjacentHouses(House house)
        {
            ArgumentNullException.ThrowIfNull(house);

            var adjacentHouses = new List<House>();

            int x = house.MyID / MyHouses[0].Length;
            int y = house.MyID % MyHouses[0].Length;

            if (x > 0)
            {
                adjacentHouses.Add(MyHouses[x - 1][y]);
            }
            if (x < MyHouses.Length - 1)
            {
                adjacentHouses.Add(MyHouses[x + 1][y]);
            }
            if (y > 0)
            {
                adjacentHouses.Add(MyHouses[x][y - 1]);
            }
            if (y < MyHouses[x].Length - 1)
            {
                adjacentHouses.Add(MyHouses[x][y + 1]);
            }

            return adjacentHouses;
        }

        public static House GetRandomHouse(List<House> houses)
        {
            if (houses.Count == 0)
            {
                throw new ArgumentException("No houses to choose from");
            }

            Random random = new();

            int index = random.Next(houses.Count);

            return houses[index];
        }

        public void CheckForSalesmenToMove()
        {
            foreach (Person person in MyPersons)
            {
                if (person.IsSalesman())
                {
                    List<House> adjacentHouses = GetAdjacentHouses(person.CurrentHouse);

                    House randomHouse = GetRandomHouse(adjacentHouses);

                    person.SetNextHouse(randomHouse);
                }
            }
        }

        public void MoveSalesmen()
        {
            foreach (Person person in MyPersons)
            {
                if (person.IsSalesman() && person.HasNextHouse())
                {
                    person.MoveToNextHouse();
                }
            }

            MyHoursSpent++;
        }
    }
}
