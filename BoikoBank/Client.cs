namespace BoikoBank
{
    public class Client
    { 
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DateOfBirth { get; set; }
        public decimal Balance { get; set; }

        public Client(int id=0, string lastName="", string firstName ="", string data = "", decimal balance = 0)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = data;
            Balance = balance;
        }
    }
}