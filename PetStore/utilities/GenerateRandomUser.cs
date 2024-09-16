using PetStore.DataObject;
using System.Text.Json;
namespace PetStore.utilities
{
    public class GenerateRandomUser
    {


        public static User GenerateAndSaveUserCredentials(string filePath)
        {
            string[] countries = ["USA", "Canada", "Japan", "Germany", "France", "India", "Australia", "Brazil", "UK", "China"];
            string[] languages = ["english", "japanese"];
            string[] categories = ["FISH", "DOGS", "CATS", "REPTILES", "BIRDS"];

            // Generate random user data
            var user = new User
            {
                UserId = GenerateRandomString(5) + random.Next(1000, 9999),
                Password = GenerateRandomString(10),
                FirstName = GenerateRandomAlphabets(6),
                LastName = GenerateRandomAlphabets(8),
                Email = GenerateRandomEmail(),
                Phone = GenerateRandomNumberString(10),
                Address1 = GenerateRandomString(10),
                Address2 = GenerateRandomString(10),
                City = GenerateRandomAlphabets(5),
                State = GenerateRandomAlphabets(5),
                Zip = GenerateRandomNumberString(6),
                Country = countries[random.Next(countries.Length)],
                LanguagePreference = languages[random.Next(languages.Length)],
                FavoriteCategory = categories[random.Next(categories.Length)],
                EnableMyList = random.Next(2) == 0,
                EnableMyBanner = random.Next(2) == 0
            };

            Console.WriteLine(user);



            // Create an object to store UserId and Password
            var newUserCredentials = new SignInDO
            {
                UserId = user.UserId,
                Password = user.Password
            };

            // Initialize a list to hold user credentials with the desired JSON structure
            SignInList signInRoot;

            // Check if the file exists and contains valid JSON data
            if (File.Exists(filePath))
            {
                try
                {
                    // Read existing data from the file
                    string existingJson = File.ReadAllText(filePath);

                    // Deserialize the existing JSON data into the signInRoot object
                    signInRoot = JsonSerializer.Deserialize<SignInList>(existingJson) ?? new SignInList();
                }
                catch
                {
                    // In case of an error (like file corruption), initialize a new signInRoot
                    signInRoot = new SignInList();
                }
            }
            else
            {
                // If the file does not exist, initialize a new SignInRoot
                signInRoot = new SignInList();
            }

            // Add the new user credentials to the signIn list
            signInRoot.SignInDataModels.Add(newUserCredentials);

            // Serialize the updated structure back to JSON format
            string updatedJson = JsonSerializer.Serialize(signInRoot, new JsonSerializerOptions { WriteIndented = true });

            // Write the updated JSON back to the file
            File.WriteAllText(filePath, updatedJson);

            Console.WriteLine($"UserId and Password appended to {filePath}");
            return user;

        }
        private static readonly Random random = new();

        // Helper methods for generating random data
        private static string GenerateRandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private static string GenerateRandomAlphabets(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private static string GenerateRandomNumberString(int length)
        {
            const string numbers = "0123456789";
            return new string(Enumerable.Repeat(numbers, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private static string GenerateRandomEmail()
        {
            string username = GenerateRandomAlphabets(5).ToLower();
            string domain = GenerateRandomAlphabets(5).ToLower();
            return $"{username}@{domain}.com";
        }


        public static User GetUserDetails()
        {
            string filePath = "C:\\Users\\amitesh\\source\\repos\\PetStore\\PetStore\\DataFactory\\SignInData.json";
            return GenerateAndSaveUserCredentials(filePath);
        }
    }
}
