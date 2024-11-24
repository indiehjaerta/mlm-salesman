namespace Salesman
{
    public class House(int id)
    {
        public int MyID { get; set; } = id;
        private readonly List<Person> myPersons = [];

        public void AddPerson(Person person)
        {
            myPersons.Add(person);
        }

        public void RemovePerson(Person person)
        {
            bool result = myPersons.Remove(person);
            if (!result)
            {
                Console.WriteLine("Person not found in house");
            }
        }

        public bool HasSalesmen()
        {
            foreach (Person person in myPersons)
            {
                if (!person.MyIsUnemployed)
                {
                    return true;
                }
            }
            return false;
        }

        public void ConvertToSalesmen()
        {
            foreach (Person person in myPersons)
            {
                person.MyIsUnemployed = false;
            }
        }

        public string PrintString()
        {
            int numSalesmen = 0;
            int numUnemployed = 0;
            foreach (Person person in myPersons)
            {
                if (!person.MyIsUnemployed)
                {
                    numSalesmen++;
                }
                else
                {
                    numUnemployed++;
                }
            }

            return "[" + numSalesmen + "," + numUnemployed + "]";
        }

    }
}