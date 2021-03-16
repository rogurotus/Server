import React from "react";

import s from "./Registration.module.css";
import {withRouter} from "react-router-dom";
import Navbar from "../../navbar/Navbar";

class Registration extends React.Component {
    onLoginChange = (e) => {
        let loginText = e.target.value;
        this.props.UpdateLogin(loginText);
    };/*На ввод логина*/
    onPassChange = (e) => {
        let passText = e.target.value;
        this.props.UpdatePass(passText, this.props.lengthPass);
    };/*На ввод пароля*/
    onPassRepChange = (e) => {
        let passRepText = e.target.value;
        this.props.UpdatePassRep(passRepText, this.props.lengthPassRep);
    };/*На ввод повторного пароля*/
    /*    onEmailChange = (e) => {
            let emailText = e.target.value;
            this.props.UpdateEmail(emailText);
        };*//*На ввод почты*/
    onclick = () => {
        const {history} = this.props;
        this.props.onClickReg(history);
    };/*На регистрацию*/
    onclickCancel = () => {
        const {history} = this.props;
        this.props.onClickCancel(history);
    }/*Отмена*/

    render() {
        return (
            <div>
                <Navbar links={this.props.links[this.props.link_id].links}
                        buttonVisible={false} user={""}
                />
                <div className={s.RegistrationPage}>
                    <div className={s.RegistrationContent}>
                        <div className={s.NameReg}>
                            {this.props.NameReg}
                        </div>
                        <div className={s.ForInputReg}>
                            <div>
                                <div>{this.props.NameUser}</div>
                                <input onChange={this.onLoginChange}
                                       value={this.props.valueLoginReg}
                                       type={this.props.Type}
                                       className={s.UserInputReg}/>{/*Окно логина*/}
                                {/*                   <div className={props.store.PageReg.ClassNameReg[7].name}>
                            {props.store.PageReg.NamesReg[2].name}</div>
                        <input type={props.store.Type} className={props.store.PageReg.ClassNameReg[8].name}/>*/}
                                <div>{this.props.NamePass}</div>
                                <input onChange={this.onPassChange} value={this.props.valuePasswordReg}
                                       type={this.props.Type} className={s.PasswordInputReg}/>{/*Окно пароля*/}
                                <div>{this.props.NamePassRep}</div>
                                <input onChange={this.onPassRepChange}
                                       value={this.props.valuePassRepReg}
                                       type={this.props.Type}
                                       className={s.RepPasswordInputReg}/>{/*Окно повтора пароля*/}
                                {/*<div>{this.props.NameEmail}</div>*/}
                                {/*                           <input onChange={this.onEmailChange}
                                       value={this.props.valueEmail}
                                       type={this.props.Type}
                                       className={s.EmailInputReg}/>*/}
                            </div>
                        </div>
                        <div className={s.ForNavLinkReg}>
                            <button className={s.CancelNavLink}
                                    onClick={this.onclickCancel}>{this.props.NameCancel}</button>
                            {/*на рег-ю*/}
                            <button className={s.ButNavLinkReg} onClick={this.onclick}>
                                {this.props.NameBut}</button>
                            {/*Отмена*/}
                        </div>
                    </div>
                </div>
            </div>
        );
    }
}

export default withRouter(Registration);