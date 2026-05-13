namespace BoikoBank
{
    public class Client
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DataR { get; set; }
        public decimal Balance { get; set; }

        public Client(int id, string lastName, string firstName, string data, decimal balance = 0)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            DataR = data;
            Balance = balance;
        }
    }
}