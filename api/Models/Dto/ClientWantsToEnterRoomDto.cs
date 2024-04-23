using lib;

namespace api.Models.Dto;

public class ClientWantsToEnterRoomDto : BaseDto
{
    public int RoomId { get; set; }
}