import React from "react";
import {NavLink} from "react-router-dom";
import "./ForgotPasswordPage.css";
const ForgotPasswordPage=(props)=>{
    return(
        <div className={props.state.PageForPas.ClassForPas[0].name}>
            <div className={props.state.PageForPas.ClassForPas[1].name}>
                <div className={props.state.PageForPas.ClassForPas[2].name}>
                    {props.state.PageForPas.NamesForPas[0].name}
                </div>
                <div className={props.state.PageForPas.ClassForPas[3].name}>
                    {props.state.PageForPas.NamesForPas[1].name}
                </div>
                <div className={props.state.PageForPas.ClassForPas[4].name}>
                    <div className={props.state.PageForPas.ClassForPas[5].name}>
                        {props.state.PageForPas.NamesForPas[2].name}
                    </div>
                    <div className={props.state.PageForPas.ClassForPas[6].name}>
                        <input type={props.state.Type} className={props.state.PageForPas.ClassForPas[7].name}/>
                    </div>
                </div>
                <div className={props.state.PageForPas.ClassForPas[8].name}>
                    <NavLink to={props.state.PageForPas.LinksForPas[0].links}
                             className={props.state.PageForPas.ClassForPas[9].name}>
                        {props.state.PageForPas.NamesForPas[3].name}</NavLink>
                    <NavLink to={props.state.PageForPas.LinksForPas[0].links}
                             className={props.state.PageForPas.ClassForPas[10].name}>
                        {props.state.PageForPas.NamesForPas[4].name}</NavLink>
                </div>
            </div>
        </div>
    );
}
export default ForgotPasswordPage;