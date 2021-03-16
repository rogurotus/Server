import React from "react";
import {useHistory} from "react-router-dom";
import s from "./TicketEnrolled.module.css";

let TicketEnrolled = (props) => {
    const history = useHistory();
    let viewProfile = () => {
        props.ClickDirectInfo(props.id);
        history.push("/TicketInfoPage" + "/" + props.nomber);
    };
    return (
        <div className={s.TicketEnrolled} onClick={viewProfile}>
            <div className={s.TicketName}>
                {props.name}
            </div>
        </div>
    );
}
export default TicketEnrolled;