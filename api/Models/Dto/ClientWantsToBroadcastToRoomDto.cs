using lib;

namespace api.Models.Dto;

public class ClientWantsToBroadcastToRoomDto : BaseDto
{
    public string Message { get; set; }
    public int RoomId { get; set; }
}