import React from "react";
import {withRouter} from "react-router-dom";
import s from "./Entrance.module.css";
import Navbar from "../../navbar/Navbar";

class Entrance extends React.Component {
    onClick = () => {
        const {history} = this.props;
        this.props.onClickEnter(history);
    };/*На вход*/
    /*    OnClickForgot = () => {
            const {history} = this.props;
            this.props.onClickForgot(history);
        }*//*для востановления почты*/
    OnClickReg = () => {
        const {history} = this.props;
        this.props.onClickReg(history);
    }/*на регистрацию*/
    onLoginChange = (e) => {
        let loginText = e.target.value;
        this.props.onLoginChangeEnter(loginText);
    };/*Ввод логина*/
    onPassChange = (e) => {
        let passText = e.target.value;
        this.props.onPassChangeEnter(passText, this.props.length);
    };/*Ввод логина*/

    render() {
        return (
            <div>
                <Navbar links={this.props.links[this.props.link_id].links}
                        buttonVisible={false} user={""}
                />
                <div className={s.EntrancePage}>
                    <div className={s.EntranceContent}>
                        <div className={s.NameEntrance}>
                            {this.props.NameEntr}
                        </div>
                        <div className={s.ForInput}>
                            <div>
                                <div>
                                    {this.props.NameLogin}</div>
                                <input onChange={this.onLoginChange}
                                       value={this.props.valueLogin}
                                       type={this.props.type}
                                       className={s.LoginInput}
                                />
                            </div>
                            <div>
                                <div>
                                    {this.props.NamePassword}</div>
                                <input onChange={this.onPassChange}
                                       value={this.props.valuePass}
                                       type={this.props.type}
                                       className={s.PasswordInput}/>
                            </div>
                        </div>
                        <div className={s.ForNavLink}>
                            {/*<p className={s.ForgotPassNavLink} onClick={this.OnClickForgot}>{this.props.NameForgot}</p>*/}
                            <button className={s.RegButNavLink} onClick={this.OnClickReg}>{this.props.NameReg}</button>
                            <button onClick={this.onClick}
                                    className={s.EnterBut}>
                                {this.props.NameEntr}</button>
                        </div>
                    </div>
                </div>
            </div>
        );
    }
}

export default withRouter(Entrance);