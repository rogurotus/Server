import React from "react";
import "./tickets.css"

import Ticket from "./ticket/Ticket";
import {selectChangeActionCreator} from "../redux/store";


const Tickets = (props) => {
    let select=React.createRef();
    let ticket=
        props.state.PageTiket.Directs[props.state.PageTiket.id].TiketsDirect.map(tickets=>
        <Ticket directid={props.state.PageTiket.id}
                tiketsid={tickets.id}
                name={tickets.name}
                disEnrolled={tickets.disEnrolled} defEnrolled={tickets.defEnrolled}
                disProcessing={tickets.disProcessing} defProcessing={tickets.defProcessing}
                disCompleted={tickets.disCompleted} defCompleted={tickets.defCompleted}
                dispatch={props.dispatch}
        />);
    let selectChange=()=>{
        let id=select.current.value;
        props.dispatch(selectChangeActionCreator(id));
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
                        ticket
                    }
                </div>
            </div>
        </div>
    );
}
export default Tickets;