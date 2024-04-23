using lib;

namespace api.Models.Dto;

public class ClientWantsToSignInDto : BaseDto
{
    public String Username { get; set; }
}