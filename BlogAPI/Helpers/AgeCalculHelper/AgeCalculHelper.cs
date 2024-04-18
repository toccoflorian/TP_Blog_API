namespace BlogAPI.Helpers.AgeCalculHelper
{
    public static class AgeCalculHelper
    {
        public static bool IsUserAdult(DateTime dateOfBirth)
        {
            int age = DateTime.Today.Year - dateOfBirth.Year;
            if (dateOfBirth > DateTime.Today.AddYears(-age)) age--;
            return age >= 18;
        }
    }
}
