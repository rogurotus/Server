import React from "react";
import {NavLink} from "react-router-dom";
import "./Registration.css";
import {
    onClickRegActionCreator, onEmailChangeRegActionCreator,
    onLoginChangeRegActionCreator,
    onPassChangeRegActionCreator,
    onPassRepChangeRegActionCreator
} from "../../redux/store";
const Registration=(props)=>{
    let login=React.createRef();
    let pass=React.createRef();
    let passRep=React.createRef();
    let email=React.createRef();
    let onLoginChange=()=>{
        let loginText=login.current.value;
        props.dispatch(onLoginChangeRegActionCreator(loginText));
    };
    let onPassChange=()=>{
        let passText=pass.current.value;
        props.dispatch(onPassChangeRegActionCreator(passText));
    };
    let onPassRepChange=()=>{
        let passRepText=passRep.current.value;
        props.dispatch(onPassRepChangeRegActionCreator(passRepText));
    };
    let onEmailChange=()=>{
        let emailText=email.current.value;
        props.dispatch(onEmailChangeRegActionCreator(emailText));
    };
    let onclick=()=>{
        props.dispatch(onClickRegActionCreator());
    }
    return(
        <div className={props.state.PageReg.ClassNameReg[0].name}>
            <div className={props.state.PageReg.ClassNameReg[1].name}>
                <div className={props.state.PageReg.ClassNameReg[2].name}>
                    {props.state.PageReg.NamesReg[0].name}
                </div>
                <div className={props.state.PageReg.ClassNameReg[3].name}>
                    <div className={props.state.PageReg.ClassNameReg[4].name}>
                        <div className={props.state.PageReg.ClassNameReg[5].name}>
                            {props.state.PageReg.NamesReg[1].name}</div>
                        <input onChange={onLoginChange}
                               value={props.state.PageReg.Login}
                               ref={login}
                               type={props.state.Type}
                               className={props.state.PageReg.ClassNameReg[6].name}/>
     {/*                   <div className={props.store.PageReg.ClassNameReg[7].name}>
                            {props.store.PageReg.NamesReg[2].name}</div>
                        <input type={props.store.Type} className={props.store.PageReg.ClassNameReg[8].name}/>*/}
                        <div className={props.state.PageReg.ClassNameReg[9].name}>
                            {props.state.PageReg.NamesReg[3].name}</div>
                        <input onChange={onPassChange}
                               value={props.state.PageReg.PasswordText}
                               ref={pass}
                               type={props.state.Type}
                               className={props.state.PageReg.ClassNameReg[10].name}/>
                        <div className={props.state.PageReg.ClassNameReg[11].name}>
                            {props.state.PageReg.NamesReg[4].name}</div>
                        <input onChange={onPassRepChange}
                               value={props.state.PageReg.PassRepText}
                               ref={passRep}
                               type={props.state.Type}
                               className={props.state.PageReg.ClassNameReg[12].name}/>
                        <div className={props.state.PageReg.ClassNameReg[13].name}>
                            {props.state.PageReg.NamesReg[5].name}</div>
                        <input onChange={onEmailChange}
                               value={props.state.PageReg.Email}
                               ref={email}
                               type={props.state.Type}
                               className={props.state.PageReg.ClassNameReg[14].name}/>
                    </div>
                </div>
                <div className={props.state.PageReg.ClassNameReg[15].name}>
                    <NavLink to={props.state.PageReg.LinksReg[0].links}
                             className={props.state.PageReg.ClassNameReg[16].name}>
                        {props.state.PageReg.NamesReg[6].name}
                    </NavLink>
                    <NavLink
                        to={props.state.PageReg.LinksReg[1].links}
                        className={props.state.PageReg.ClassNameReg[17].name}>
                        {props.state.PageReg.NamesReg[7].name}
                    </NavLink>
                    <button onClick={onclick}/>
                </div>
            </div>
        </div>
    );
}
export default Registration;