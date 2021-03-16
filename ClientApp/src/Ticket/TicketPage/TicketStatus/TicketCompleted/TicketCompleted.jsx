import React from "react";
import s from "./TicketCompleted.module.css"
import {useHistory} from "react-router-dom";

let TicketCompleted = (props) => {
    const history = useHistory();
    let viewProfile = () => {
        props.ClickDirectInfo(props.id);
        history.push("/TicketInfoPage" + "/" + props.nomber);
    };
    return (
        <div className={s.TicketCompleted} onClick={viewProfile}>
            <div className={s.TicketName}>
                {props.name}
            </div>
        </div>
    );
}
export default TicketCompleted;