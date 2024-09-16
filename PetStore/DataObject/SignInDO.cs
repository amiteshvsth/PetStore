namespace PetStore.DataObject
{
    public class SignInDO
    {
        public string UserId { get; set; }
        public string Password { get; set; }
    }

    public class SignInList
    {
        // list initialized SignIn which is of type SignInDataModel
        public List<SignInDO> signIn;

        // list initialized SignInDataModels of type SignInDataModel 
        // which will get data for type SignIn meaning will return a list of type SignInDataModel    => get
        // and will set the value if provided in parenthesis of type SignInDataModel               => set
        public List<SignInDO> SignInDataModels { get; set; } = [];

    }
}
