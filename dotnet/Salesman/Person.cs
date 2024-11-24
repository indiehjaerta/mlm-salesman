namespace Salesman
{
    public class Person(int id, bool unemployed = true)
    {

        private int MyID { get; set; } = id;
        public bool MyIsUnemployed { get; set; } = unemployed;
        public House CurrentHouse { get; set; } = null!;
        public House? NextHouse { get; set; } = null;

        public bool IsSalesman()
        {
            return !MyIsUnemployed;
        }

        public void SetNextHouse(House house)
        {
            NextHouse = house;
        }

        public void MoveToNextHouse()
        {
            if (NextHouse != null)
            {

                CurrentHouse.RemovePerson(this);
                NextHouse.AddPerson(this);

                CurrentHouse = NextHouse;
                NextHouse = null;
            }
        }

        public bool HasNextHouse()
        {
            return NextHouse != null;
        }
    }
}