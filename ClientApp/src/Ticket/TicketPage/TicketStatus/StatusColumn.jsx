import React from "react";
import s from "../TicketPage.module.css";
import TicketEnrolled from "./TicketEnrolled/TicketEnrolled";
import TicketProcessing from "./TicketProcessing/TicketProcessing";
import TicketCompleted from "./TicketCompleted/TicketCompleted";

let StatusColumn = (props) => {
    let getQuantityTicketEnt = (name, id) => {
        let quantityTicketEnt;
        let j = 0;
        switch (id) {
            case "0": {
                for (let i = 0; i < props.Ticket.length; i++) {
                    if (props.Ticket[i].state.name === name) {
                        j++;
                    }
                }
                break;
            }
            default: {
                for (let i = 0; i < props.Ticket.length; i++) {
                    if ((props.Ticket[i].state.name === name) &&
                        (props.Ticket[i].district.name === props.directs[id - 1].name)) {
                        j++;
                    }
                }
                break;
            }
        }
        quantityTicketEnt = j;
        return (quantityTicketEnt);
    }
    let getQuantityTicketProc = (name, id) => {
        let quantityTicketProc;
        let j = 0;
        switch (id) {
            case "0": {
                for (let i = 0; i < props.Ticket.length; i++) {
                    if (props.Ticket[i].state.name === name) {
                        j++;
                    }
                }
                break;
            }
            default: {
                for (let i = 0; i < props.Ticket.length; i++) {
                    if ((props.Ticket[i].state.name === name) &&
                        (props.Ticket[i].district.name === props.directs[id - 1].name)) {
                        j++;
                    }
                }
                break;
            }
        }
        quantityTicketProc = j;
        return (quantityTicketProc);
    }
    let getQuantityTicketComp = (name, id) => {
        let quantityTicketComp;
        let j = 0;
        switch (id) {
            case "0": {
                for (let i = 0; i < props.Ticket.length; i++) {
                    if (props.Ticket[i].state.name === name) {
                        j++;
                    }
                }
                break;
            }
            default: {
                for (let i = 0; i < props.Ticket.length; i++) {
                    if ((props.Ticket[i].state.name === name) &&
                        (props.Ticket[i].district.name === props.directs[id - 1].name)) {
                        j++;
                    }
                }
                break;
            }
        }
        quantityTicketComp = j;
        return (quantityTicketComp);
    }
    let getTicketEnrolled = (name, id) => {
        let array = [];
        switch (id) {
            case "0": {
                for (let i = 0; i < props.Ticket.length; i++) {
                    if (props.Ticket[i].state.name === name) {
                        array[i] = <TicketEnrolled name={props.Ticket[i].id + " " +
                        props.Ticket[i].district.name} id={i} nomber={props.Ticket[i].id}
                                                   ClickDirectInfo={props.ClickDirectInfo}/>
                    }
                }
                break;
            }
            default: {
                for (let i = 0; i < props.Ticket.length; i++) {
                    if ((props.Ticket[i].state.name === name) &&
                        (props.Ticket[i].district.name === props.directs[id - 1].name)) {
                        array[i] = <TicketEnrolled name={props.Ticket[i].id + " " +
                        props.Ticket[i].district.name} id={i} nomber={props.Ticket[i].id}
                                                   ClickDirectInfo={props.ClickDirectInfo}/>
                    }
                }
                break;
            }
        }

        return (array);
    }
    let getTicketProcessing = (name, id) => {
        let array = [];
        switch (id) {
            case "0": {
                for (let i = 0; i < props.Ticket.length; i++) {
                    if (props.Ticket[i].state.name === name) {
                        array[i] = <TicketProcessing name={props.Ticket[i].id + " " +
                        props.Ticket[i].district.name} id={i} nomber={props.Ticket[i].id}
                                                     ClickDirectInfo={props.ClickDirectInfo}/>
                    }
                }
                break;
            }
            default: {
                for (let i = 0; i < props.Ticket.length; i++) {
                    if ((props.Ticket[i].state.name === name) &&
                        (props.Ticket[i].district.name === props.directs[id - 1].name)) {
                        array[i] = <TicketProcessing name={props.Ticket[i].id + " " +
                        props.Ticket[i].district.name} id={i} nomber={props.Ticket[i].id}
                                                     ClickDirectInfo={props.ClickDirectInfo}/>
                    }
                }
                break;
            }
        }
        return (array);
    }
    let getTicketCompleted = (name, id) => {
        let array = [];
        switch (id) {
            case "0": {
                for (let i = 0; i < props.Ticket.length; i++) {
                    if (props.Ticket[i].state.name === name) {
                        array[i] = <TicketCompleted name={props.Ticket[i].id + " " +
                        props.Ticket[i].district.name} id={i} nomber={props.Ticket[i].id}
                                                    ClickDirectInfo={props.ClickDirectInfo}/>
                    }
                }
                break;
            }
            default: {
                for (let i = 0; i < props.Ticket.length; i++) {
                    if ((props.Ticket[i].state.name === name) &&
                        (props.Ticket[i].district.name === props.directs[id - 1].name)) {
                        array[i] = <TicketCompleted name={props.Ticket[i].id + " " +
                        props.Ticket[i].district.name} id={i} nomber={props.Ticket[i].id}
                                                    ClickDirectInfo={props.ClickDirectInfo}/>
                    }
                }
                break;
            }
        }
        return (array);
    }
    let IfNull = (quantity, Fun, name, i) => {
        if (quantity <= 0) {
            return (<div className={s.NullTicket}>{""}</div>);
        } else {
            return (<div className={s.scrol}>
                {Fun(name, i)}
            </div>)
        }
    }
    let StatusGet = () => {
        let i = props.id;
        let a;
        if (props.StatusTicket.length > 0) {
            a = props.StatusTicket.map(ar => {
                    switch (ar.name) {
                        case "Поступила": {
                            return (<div className={s.StatusEnrolled}>
                                <div className={s.StatusNames}>
                                    <div className={s.StatusName}>{ar.name}</div>
                                    <div className={s.StatusQuantity}>{"Заявок: " + getQuantityTicketEnt(ar.name, i)}</div>
                                </div>
                                {IfNull(getQuantityTicketEnt(ar.name, i), getTicketEnrolled, ar.name, i)}
                            </div>);
                        }
                        case "В обработке": {
                            return (<div className={s.StatusProcessing}>
                                <div className={s.StatusNames}>
                                    <div className={s.StatusName}>{ar.name}</div>
                                    <div className={s.StatusQuantity}>{"Заявок: " + getQuantityTicketProc(ar.name, i)}</div>
                                </div>
                                {IfNull(getQuantityTicketProc(ar.name, i), getTicketProcessing, ar.name, i)}
                            </div>);
                        }
                        case "Выполнена": {
                            return (<div className={s.StatusCompleted}>
                                <div className={s.StatusNames}>
                                    <div className={s.StatusName}>{ar.name}</div>
                                    <div className={s.StatusQuantity}>{"Заявок: " + getQuantityTicketComp(ar.name, i)}</div>
                                </div>
                                {IfNull(getQuantityTicketComp(ar.name, i), getTicketCompleted, ar.name, i)}
                            </div>);
                        }
                    }
                }
            );
        }
        /*        let ar=props.StatusTicket.map(

                )*/
        return (a);
    }
    return (
        <div className={s.ForStatus}>
            {StatusGet()}
        </div>
    );
}
export default StatusColumn;/*Данная функия генерирует и сортирует приходящие заявки*/