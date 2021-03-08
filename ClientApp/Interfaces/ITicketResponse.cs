using Server.Models;
using Server.DataBase;

namespace Server.Interfaces
{
    interface ITicketResponse
    {
        TicketResponse ToResponse(PostgreDataBase db);
    }
}
