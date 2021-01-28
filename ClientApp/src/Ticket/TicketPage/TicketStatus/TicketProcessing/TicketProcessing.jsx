import React from "react";
import s from "./TicketProcessing.module.css"
import {useHistory} from "react-router-dom";

let TicketProcessing = (props) => {
    const history = useHistory();
    let viewProfile = () => {
        props.ClickDirectInfo(props.id);
        history.push("/TicketInfoPage");
    };
    return (
        <div className={s.TicketProcessing} onClick={viewProfile}>
            <div className={s.TicketName}>
                {props.name}
            </div>
        </div>
    );
}
export default TicketProcessing;