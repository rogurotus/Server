import React from "react";
import s from "./TicketPage.module.css";
import StatusColumn from "./TicketStatus/StatusColumn";
import * as axios from "axios";
import Navbar from "../../navbar/Navbar";

class TicketPage extends React.Component {
    constructor(props) {
        super(props);
        /*let v="http://84.22.135.132:5000";*/
        axios.get("/TicketType")
            .then(res => {
                let Type = res.data;
                this.props.type(Type);
            });
        axios.get("/TicketState")
            .then(res => {
                let state = res.data;
                this.props.state(state);
            });
        axios.get("/District")
            .then(res => {
                let direct = res.data;
                this.props.direct(direct);
            });
    }

    componentDidMount() {
        /*let v="http://84.22.135.132:5000"*/
        axios.get("/Ticket/TrafficLight")
            .then(res => {
                let data = res.data;
                this.props.data(data);
            });
    }

    Buttons = () => {
        /*let v="http://84.22.135.132:5000";*/
        let buttons = this.props.TypeTicket.map(a => {
                if (a.name === "Светофор") {
                    return (<div className={s.ItemsBut}>
                        <button className={s.Button} onClick={this.click}>
                            <img className={s.Img} src={a.url} alt=""/>
                            {a.name}
                        </button>
                    </div>)
                } else {
                    return (<div className={s.ItemsBut}>
                        <button className={s.Button} disabled={true}>
                            <img className={s.Img} src={a.url} alt=""/>
                            {a.name}
                        </button>
                    </div>)
                }
            }
        );
        return (buttons);
    }
    Alert = (e) => {
        let id = e.target.value;
        this.props.UpdateIdSelect(id);
    }
    Option = () => {
        let opt1 = [];
        if (this.props.directs.length > 0) {
            for (let i = 0; i < this.props.directs.length; i++) {
                opt1[i] = <option value={i + 1}>{this.props.directs[i].name}</option>
            }
        }
        return (opt1)
    }
    click = () => {
        /*let v="http://84.22.135.132:5000"*/
        axios.get("/Ticket/TrafficLight")
            .then(res => {
                let data = res.data;
                this.props.data(data);
            });

    }

    render() {
        return (
            <div>
                <Navbar links={this.props.links[this.props.link_id].link}
                        buttonVisible={true} user={this.props.user}
                />
                <div className={s.ContentPage}>
                    <div className={s.SideBar}>
                        {this.Buttons()}
                    </div>
                    <div className={s.Content}>
                        <div className={s.HelperBar}>
                            <div className={s.Sort}>
                                {this.props.NameForSelectDirect}
                                <select name={this.props.NameForSelectDirect} id={0} onChange={this.Alert}>
                                    <option
                                        value={this.props.optionValue.value}>{this.props.optionValue.name}</option>
                                    {this.Option()}
                                </select>
                            </div>
                        </div>
                        <StatusColumn
                            id={this.props.ID}
                            directs={this.props.directs}
                            Ticket={this.props.ticket}
                            StatusTicket={this.props.StatusTicket}
                            ClickDirectInfo={this.props.ClickDirect}
                        />
                    </div>
                </div>
            </div>
        );
    }
}

export default TicketPage;