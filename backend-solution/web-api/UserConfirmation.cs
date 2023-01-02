namespace web_api
{
    public class UserConfirmation
    {
        public int Id { get; set; }
        public int UserId  { get; set; }
        public string ConfirmationId{ get; set; }
        public UserConfirmation()
        {
            ConfirmationId = Guid.NewGuid().ToString();
        }
    }
}
