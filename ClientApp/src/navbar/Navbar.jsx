import React from "react";
import s from "./Navbar.module.css";
import {NavLink, withRouter} from "react-router-dom";
import {useHistory} from "react-router-dom";
import * as axios from "axios";

let Navbar = (props) => {
    const history = useHistory();
    let ClickLogOut = () => {
        axios.post("/WebUser/Logout", [{'Content-Type': 'application/json'}])/*http://84.22.135.132:5000*/
            .then(res => {
                if (res.data.message === null) {
                    alert(res.data.error);
                } else if (res.data.error === null) {
                    alert(res.data.message);
                    history.push("/Authorization/Entrance");
                }
            });
    }
    let visible = () => {
        if (props.buttonVisible === true) {
            return (<div className={s.App_Nav}><NavLink className={s.NavLink} to={props.links}>{props.links}</NavLink>
                <div className={s.User_Text}><p className={s.User}>{props.user}</p></div>
                <div className={s.ForBut}>
                    <button className={s.button} onClick={ClickLogOut}>{"Logout"}</button>
                </div>
            </div>);
        } else {
            return (<div className={s.App_Nav}>
                <NavLink className={s.NavLink} to={props.links}>{props.links}</NavLink>
            </div>)
        }
    }
    return (<div>
            {visible()}
        </div>
    );
}
export default withRouter(Navbar);