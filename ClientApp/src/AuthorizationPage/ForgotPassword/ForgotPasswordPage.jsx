import React from "react";
import {withRouter} from "react-router-dom";
import s from "./ForgotPasswordPage.module.css";
import Navbar from "../../navbar/Navbar";

class ForgotPasswordPage extends React.Component {
    onEmailChange = (e) => {
        let emailText = e.target.value;
        this.props.UpdateEmailForgot(emailText);
    };
    ClickCancel = () => {
        const {history} = this.props;
        this.props.OnClickReturn(history);
    }
    ClickSend = () => {
        const {history} = this.props;
        this.props.OnClickSend(history);
    }

    render() {
        return (
            <div>
                <Navbar links={this.props.links[this.props.link_id].links}
                        buttonVisible={false} user={""}
                />
                <div className={s.ForgotPage}>
                    <div className={s.ForgotContent}>
                        <div className={s.NameForgot}>
                            {this.props.NamePage}
                        </div>
                        <div className={s.TextForgot}>
                            {this.props.NameInfo}
                        </div>
                        <div className={s.ForgotForInput}>
                            <div className={s.ForgotNameInput}>
                                {this.props.NameInput}
                            </div>
                            <div className={s.ForgotDivInput}>
                                <input onChange={this.onEmailChange}
                                       value={this.props.valueEmailForgot}
                                       type={this.props.type}
                                       className={s.ForgotInput}/>
                            </div>
                        </div>
                        <div className={s.ForgotNavLink}>
                            <button className={s.ForgotCancelLink} onClick={this.ClickCancel}>
                                {this.props.NameForgotCancel}</button>
                            <button className={s.ForgotLink} onClick={this.ClickSend}>
                                {this.props.NameSend}</button>
                        </div>
                    </div>
                </div>
            </div>
        );
    }
}

export default withRouter(ForgotPasswordPage);