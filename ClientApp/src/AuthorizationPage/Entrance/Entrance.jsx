import React from "react";
import {NavLink} from "react-router-dom";
import "./Entrance.css";
import {
    onClickEnterActionCreator,
    onLoginChangeEnterActionCreator,
    onPassChangeEnterActionCreator,
} from "../../redux/store";
const Entrance=(props)=>{
    let login=React.createRef();
    let pass=React.createRef();
    let onClick=()=>{
        props.dispatch(onClickEnterActionCreator());
    };
    let onLoginChange=()=>{
        let loginText=login.current.value;
        props.dispatch(onLoginChangeEnterActionCreator(loginText));
    };
    let onPassChange=()=>{
        let passText=pass.current.value;
        props.dispatch(onPassChangeEnterActionCreator(passText));
    };
    return(
        <div className={props.state.PageEntrance.ClassNameEntrance[0].name}>
            <div className={props.state.PageEntrance.ClassNameEntrance[1].name}>
                <div className={props.state.PageEntrance.ClassNameEntrance[2].name}>
                    {props.state.PageEntrance.NamesEntrance[0].name}
                </div>
                <div className={props.state.PageEntrance.ClassNameEntrance[3].name}>
                    <div className={props.state.PageEntrance.ClassNameEntrance[4].name}>
                        <div className={props.state.PageEntrance.ClassNameEntrance[5].name}>
                            {props.state.PageEntrance.NamesEntrance[1].name}</div>
                        <input onChange={onLoginChange}
                               value={props.state.PageEntrance.Login}
                            ref={login}
                            type={props.state.Type}
                               className={props.state.PageEntrance.ClassNameEntrance[6].name}
                        />
                    </div>
                    <div className={props.state.PageEntrance.ClassNameEntrance[7].name}>
                        <div className={props.state.PageEntrance.ClassNameEntrance[8].name}>
                            {props.state.PageEntrance.NamesEntrance[2].name}</div>
                        <input onChange={onPassChange}
                               value={props.state.PageEntrance.PasswordText}
                               ref={pass}
                            type={props.state.Type}
                               className={props.state.PageEntrance.ClassNameEntrance[9].name}/>
                    </div>
                </div>
                <div className={props.state.PageEntrance.ClassNameEntrance[10].name}>
                    <NavLink to={props.state.PageEntrance.LinksEntrance[0].links}
                             className={props.state.PageEntrance.ClassNameEntrance[11].name}>
                        {props.state.PageEntrance.NamesEntrance[3].name}
                    </NavLink>
                    <NavLink to={props.state.PageEntrance.LinksEntrance[1].links}>
                        <button className={props.state.PageEntrance.ClassNameEntrance[12].name}>
                            {props.state.PageEntrance.NamesEntrance[4].name}
                        </button>
                    </NavLink>
                    <button onClick={onClick}
                        className={props.state.PageEntrance.ClassNameEntrance[13].name}>
                        {props.state.PageEntrance.NamesEntrance[0].name}</button>
                </div>
            </div>
        </div>
    );
}
export default Entrance;