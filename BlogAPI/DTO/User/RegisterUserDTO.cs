namespace BlogAPI.DTO.User
{
    public class RegisterUserDTO
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public int YearOfBirth { get; set; }
        public int MonthOfBirth { get; set; }
        public int DayOfBirth { get; set; }
        public string Password { get; set;}
        public string ConfirmPassword { get; set;}
    }
}
