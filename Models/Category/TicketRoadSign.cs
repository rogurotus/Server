using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Server.Interfaces;
using Server.DataBase;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;

namespace Server.Models
{
    [Table("ticket_road_sign")]
    public class TicketRoadSign : ITicketResponse
    {
        [Key]
        public int ticket_id {get;set;} 
        
        public Ticket ticket {get; set;}

        // public RoadSign road_sign {get; set;}

        public TicketResponse ToResponse(PostgreDataBase db)
        {
            ticket = db.tikets
                .Where(t => t.id == ticket_id).FirstOrDefault();
            ticket.state = db.ticket_states
                .Where(t => t.id == ticket.state_id).FirstOrDefault();
            ticket.type = db.tiket_types
                .Where(t => t.id == ticket.type_id).FirstOrDefault();
            ticket.mobile_user = db.mobile_users
                .Where(t => t.token == ticket.mobile_token).FirstOrDefault();
                
            List<TicketHistory> history = db.ticket_historys
                .Where(h => h.ticket_id == ticket_id)
                .Join(db.ticket_states, h => h.ticket_state_old_id, s => s.id, (h,s) => 
                new TicketHistory
                {
                    id = h.id,
                    date = h.date,
                    ticket_id = h.ticket_id,
                    ticket_state_new_id = h.ticket_state_new_id,
                    ticket_state_old_id = h.ticket_state_old_id,
                    ticket_state_old = s
                })
                .Join(db.ticket_states, h => h.ticket_state_new_id, s => s.id, (h,s) => 
                new TicketHistory
                {
                    id = h.id,
                    date = h.date,
                    ticket_id = h.ticket_id,
                    ticket_state_new_id = h.ticket_state_new_id,
                    ticket_state_new = s,
                    ticket_state_old_id = h.ticket_state_old_id,
                    ticket_state_old = h.ticket_state_old,
                })
                .ToList();
            
            TicketResponse response = new
            TicketResponse
            {
                id = ticket_id,
                date_add = ticket.date_add,
                state = ticket.state,
                type = ticket.type,
                mobile_user = ticket.mobile_user,
                description = ticket.description,
                long_ = ticket.long_,
                lat = ticket.lat,
                dublicates_id = db.ticket_dublicate
                    .Where(d => d.main_tiket == ticket_id)
                    .Select(t => t.tiket)
                    .ToList(),
                photo_id = db.photos
                    .Where(p => p.ticket == ticket_id).Select(p => p.id).ToList(),
                histories = history,
                district = db.districts.Where(d => d.id == 1).FirstOrDefault(),
            };

            return response;
        }
    }
}