import React from "react";
import "./ticket.css"
import {onChangeCompletedActionCreator, onChangeProcessingActionCreator} from "../../redux/store";
const Ticket = (props) => {

    let onchangeProcessing=()=>{
        props.dispatch(onChangeProcessingActionCreator(props.directid,props.tiketsid));
    }
    let onchangeCompleted=()=>{
        props.dispatch(onChangeCompletedActionCreator(props.directid,props.tiketsid));
    }
    return(
        <div className="ticketPattern">
            <div className="TicketContent">{props.name}
                <input disabled={props.disEnrolled} defaultChecked={props.defEnrolled}
                       name={props.name}
                       type="radio" value="1"
                />поступила
                <input disabled={props.disProcessing} defaultChecked={props.defProcessing}
                       name={props.name}
                       type="radio" value="2"
                       onChange={onchangeProcessing}
                />в обработке
                <input disabled={props.disCompleted} defaultChecked={props.defCompleted}
                       name={props.name}
                       type="radio" value="3"
                       onChange={onchangeCompleted}
                />выполнена
            </div>
        </div>
    );
}
export default Ticket;