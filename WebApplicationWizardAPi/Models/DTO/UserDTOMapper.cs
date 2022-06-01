namespace WebApplicationWizardAPi.Models.DTO
{
    public static class UserDTOMapper
    {
        public static UserDTO MapToDto(User user)
        {
            if (user == null)
                return null;
            return new UserDTO()
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Type = user.Type,
            };
        }
    }
}
