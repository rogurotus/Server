import React from "react";
import s from "./TicketInfoPage.module.css";
import Detail from "./Detail/Detail";
import Navbar from "../../navbar/Navbar";
import {NavLink, withRouter} from "react-router-dom";


class TicketInfoPage extends React.Component {
    constructor(props) {
        super(props);
        if (this.props.Ticket.length != 0) {
            localStorage.setItem("Ticket", JSON.stringify(this.props.Ticket));
        }
        if (this.props.StateTicket.length != 0) {
            localStorage.setItem("State", JSON.stringify(this.props.StateTicket));
        }/*Сохранение в локал стейдж чтобы при перезагрузке все работало*/
    }

    History = () => {
        let gethistory = () => {
            let historyText = JSON.parse(localStorage.getItem("Ticket")).histories.map(ar => <div>{
                ar.date + " из статуса " + ar.ticket_state_old.name
                + " перешла в " + ar.ticket_state_new.name}</div>);/*Получение истории заявки*/
            return (historyText);
        }
        let History = <div>{gethistory()}</div>;
        return (History);
    }
    Details = () => {
        let array = <div>
            <Detail DetailDescriptionType={"Описание:"} DetailDescriptionInfo={
                JSON.parse(localStorage.getItem("Ticket")).description}/>
            <Detail DetailDescriptionType={"Дата добавления:"}
                    DetailDescriptionInfo={JSON.parse(localStorage.getItem("Ticket")).date_add}/>
            <Detail DetailDescriptionType={"От кого:"} DetailDescriptionInfo={
                JSON.parse(localStorage.getItem("Ticket")).mobile_user.surname + " "
                + JSON.parse(localStorage.getItem("Ticket")).mobile_user.name + " " +
                JSON.parse(localStorage.getItem("Ticket")).mobile_user.phone}/>
            <Detail DetailDescriptionType={"Геолокация:"} DetailDescriptionInfo={
                JSON.parse(localStorage.getItem("Ticket")).lat + " "
                + JSON.parse(localStorage.getItem("Ticket")).long_}/>
            <Detail DetailDescriptionType={"Район:"} DetailDescriptionInfo={
                JSON.parse(localStorage.getItem("Ticket")).district.name}/>
        </div>
        return (array);/*Детали о заявки такие как описание,от кого и т.д*/
    }
    ClickProc = (idStatus) => {
        this.props.ClickProc(JSON.parse(localStorage.getItem("Ticket")).id, idStatus)
    }/*на нажатие в процессе*/
    ClickComp = (idStatus) => {
        this.props.ClickComp(JSON.parse(localStorage.getItem("Ticket")).id, idStatus)
    }/*на нажатие выполнено*/
    Buttons = () => {
        let button = JSON.parse(localStorage.getItem("State")).map(ar => {
            switch (ar.name) {
                case "В обработке": {
                    return (<div className={s.ForButton}>
                        <button className={s.Button} onClick={() => this.ClickProc(ar.id)}>{ar.name}</button>
                    </div>);
                }
                case "Выполнена": {
                    return (<div className={s.ForButton}>
                        <button className={s.Button} onClick={() => this.ClickComp(ar.id)}>{ar.name}</button>
                    </div>);
                }
            }
        });
        return (button);/*Генерация кнопок*/
    }
    getImage = () => {
        const {history} = this.props;
        let img1 = {
            mini: [], photo: []
        };
        let img = [];
        if (JSON.parse(localStorage.getItem("Ticket")).mini_photo_id != null) {
            img1.mini = JSON.parse(localStorage.getItem("Ticket")).mini_photo_id.map(a => {
                return (a)
            });
            img1.photo = JSON.parse(localStorage.getItem("Ticket")).photo_id.map(a => {
                return (a)
            });

            for (let i = 0; i < img1.mini.length; i++) {
                let str="/Photo/" + img1.photo[i];
                img[i] = <a href={str}>
                    <img className={s.img} src={"/Photo/" + img1.mini[i]} alt={""}/>
                </a>
            }
        }
        return (img);/*Генерация изображений,проверка на случай отсутвия из-ий*/
    }

    render() {
        return (
            <div>
                <Navbar links={this.props.links[this.props.link_id].link}
                        buttonVisible={true} user={localStorage.getItem('user')}/>
                <div className={s.TicketInfoPage}>
                    <div className={s.NameTicket}>
                        <div className={s.NameTicketText}>{"Заявка по: "
                        + JSON.parse(localStorage.getItem("Ticket")).type.name
                        + " ,№" + JSON.parse(localStorage.getItem("Ticket")).id}</div>
                    </div>
                    <div className={s.Content}>
                        <div className={s.SideBarRight}>
                            <div className={s.DetailNames}>{this.props.SideBarName}</div>
                            {this.Buttons()}
                        </div>
                        <div className={s.ContentTicket}>
                            <div className={s.Detail}>
                                <div className={s.DetailName}>
                                    <div className={s.DetailNames}>{this.props.DetailName}</div>
                                </div>
                                <div className={s.DetailDescription}>
                                    {this.Details()}
                                </div>
                            </div>
                            <div className={s.HistoryTicket}>
                                <div className={s.HistoryTicketText}>{this.props.HistoryTicket}</div>
                                <div className={s.HistoryTicketDescription}>
                                    <div className={s.HistoryTicketText}>
                                        {this.History()}
                                    </div>
                                    <div className={s.ForImage}>
                                        {this.getImage()}
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        );
    }
}

export default withRouter(TicketInfoPage);