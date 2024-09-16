namespace PetStore.DataObject
{
    // Creating a Model for Input Data
    public class User
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserId { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Country { get; set; }
        public string LanguagePreference { get; set; }
        public string FavoriteCategory { get; set; }
        public bool EnableMyList { get; set; }
        public bool EnableMyBanner { get; set; }

    }
    public class SignUpDO
    {
        public class SignUpList
        {
            // list initialized SignIn which is of type RegisterDataModel
            public List<User> register;

            // list initialized RegisterDataModels of type RegisterDataModel 
            // which will get data for type SignIn meaning will return a list of type RegisterDataModel    => get
            // and will set the value if provided in parenthesis of type RegisterDataModel               => set
            public List<User> SignUpDataModels { get => register; set => register = value; }
        }
    }
}
