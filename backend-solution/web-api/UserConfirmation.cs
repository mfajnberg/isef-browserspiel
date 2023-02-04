using System.Diagnostics;

namespace web_api
{
    /// <summary>
    /// represents a confrimation entity
    /// </summary>
    [DebuggerDisplay("id: {Id}, userId: {UserId}, confirm: {ConfirmationId}")]
    public class UserConfirmation
    {
        /// <summary>
        /// gets or sets the id of the confirmation
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// gets or sets the user id, which must be confirmed
        /// </summary>
        public int UserId  { get; set; }

        /// <summary>
        /// gets or sets the confirmation id, which will the user use to confirm his account
        /// </summary>
        public string ConfirmationId{ get; set; }

        /// <summary>
        /// constructor of UserConfirmation, initalizes a new <c>ConfirmationId</c>
        /// </summary>
        public UserConfirmation()
        {
            ConfirmationId = Guid.NewGuid().ToString();
        }
    }
}
