import s from './App.module.css';
import React from "react";
import {BrowserRouter, Redirect, Route} from "react-router-dom";
/*import logo from "./logo.png";*/
import EntranceContainer from "./AuthorizationPage/Entrance/EnteranceContainer";
import RegistrationContainer from "./AuthorizationPage/Registration/RegContainer";
import ForgotContainer from "./AuthorizationPage/ForgotPassword/ForgotContainer";
import TicketInfoContainer from "./Ticket/TicketInfoPage/TicketInfoContainer";
import TicketPageContainer from "./Ticket/TicketPage/TicketPageContainer";

class App extends React.Component {
    link = "/";
    linkAutho = "/Authorization";
    linkEnt = "/Authorization/Entrance";
    linkReg = "/Authorization/Registration";
    linkPageTicket = "/TicketPage";
    linkPageTicketInfo = "/TicketInfoPage";
    linkForgot = "/Authorization/ForgotPassword";

    render() {
        return (
            <BrowserRouter>
                <div className={s.App}>
                    <header className={s.App_header}>
                        {/*<img src={logo} className={s.App_logo} alt=""/>*/}
                    </header>
                    <div className={s.App_Content}>
                        <Route exact path={this.link} render={() => <Redirect to={this.linkEnt}/>}/>

                        <Route exact path={this.linkAutho} render={() => <Redirect to={this.linkEnt}/>}/>

                        <Route path={this.linkEnt} render={() => <EntranceContainer/>}/>

                        <Route path={this.linkReg} render={() => <RegistrationContainer/>}/>

                        <Route path={this.linkForgot} render={() => <ForgotContainer/>}/>

                        <Route path={this.linkPageTicket} render={() => <TicketPageContainer/>}/>

                        <Route path={this.linkPageTicketInfo} render={() => <TicketInfoContainer/>}/>
                        <Route exact path={this.linkPageTicketInfo + "/"}
                               render={() => <Redirect to={this.linkPageTicketInfo}/>}/>
                    </div>

                </div>
            </BrowserRouter>
        );
    }
}

export default App;
