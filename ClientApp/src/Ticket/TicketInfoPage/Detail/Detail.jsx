import React from "react";
import s from "./Detail.module.css";

let Detail = (props) => {
    return (
        <div className={s.DetailDescription}>
            <div className={s.DetailDescriptionType}>{props.DetailDescriptionType}</div>
            <div className={s.DetailDescriptionInfo}>{props.DetailDescriptionInfo}</div>
        </div>
    );
}
export default Detail;