import React from "react";
import "./tickets.css"

import Ticket from "./ticket/Ticket";


const Tickets = (props) => {
    let select=React.createRef();
    let Ticket=
        props.state.PageTiket.Directs[props.state.PageTiket.id].TiketsDirect.map(Tickets=>
        <Ticket directid={props.state.PageTiket.id}
                tiketsid={Tickets.id}
                name={Tickets.name}
                disEnrolled={Tickets.disEnrolled} defEnrolled={Tickets.defEnrolled}
                disProcessing={Tickets.disProcessing} defProcessing={Tickets.defProcessing}
                disCompleted={Tickets.disCompleted} defCompleted={Tickets.defCompleted}
                dispatch={props.dispatch}
        />);
    let selectChange=()=>{
        let id=select.current.value;
        props.dispatch({type:'SELECT-DIRECT-TIC',id:id});
    }
    return(
        <div className="TicketsPage">
            <div className="TicketsContent">
                <div className="NameTickets">Заявки</div>
                <div className="Name1Tickets">Район:<select ref={select} onChange={selectChange}>
                    <option value="0">Железнодорожный</option>
                    <option value="1">Кировский</option>
                    <option value="2">Ленинский</option>
                    <option value="3">Октябрьский</option>
                    <option value="4">Свердловский</option>
                    <option value="5">Советский</option>
                    <option value="6">Центральный</option>
                </select></div>
                <div className="TicketScroll">
                    {
                        zayavkimap
                    }
                </div>
            </div>
        </div>
    );
}
export default Tickets;