import * as axios from "axios";

const selectDirectTicActionType = 'SELECT-DIRECT-TIC';
const ClickDirectInfoActionType = 'CLICK-DIRECT-INFO';
const dataActionType = 'DATA';
const stateActionType = 'STATE';
const typeActionType = 'TYPE';
const directActionType = 'DIRECT';
const ClickProcActionType='CLICK-PROC';
const ClickCompActionType='CLICK-COMP';
const imgActionType='IMG';
let InitialState = {
    ticket: [], Status: [], typeTicket: [], TicketInfo: [], direct: [], id: "0",img:[],
    NamesTickets: [
        {id: 0, name: "Сортировка по району:"},
        {id: 1, name: "Заявок: "}
    ],
    optionValue: {id: 0, value: 0, name: "Все"},
    NamesTicketInfo: [
        {id: 0, name: "Имя заявки"},
        {id: 1, name: "Панель состояний"},
        {id: 2, name: "Детали заявки"},
        {id: 3, name: "История заявки"},
        {id: 4, name: "Изображения прикреплены:"}
    ],
    links:[{id:0,link:"/TicketPage"},{id:1,link:"/TicketInfoPage"}],link_id:0,
    src: "http://avanpress.ru/wp-content/uploads/2017/03/Светофор.jpg",
    HistoryText: "Пришла от пользователя В 66.66.6666"

}
const TicketReducer = (state = InitialState, action) => {
    switch (action.type) {
        case selectDirectTicActionType: {
            let stateCopy = {...state};
            stateCopy.id = action.id;
            debugger
            return stateCopy;
        }
        case ClickDirectInfoActionType: {
            let stateCopy = {...state};
            stateCopy.TicketInfo = stateCopy.ticket[action.id];
            return stateCopy;
        }
        case dataActionType: {
            let stateCopy = {...state};
            stateCopy.ticket = action.data;
            return stateCopy;
        }
        case stateActionType: {
            let stateCopy = {...state};
            stateCopy.Status = action.state;
            return stateCopy;
        }
        case typeActionType: {
            let stateCopy = {...state};
            stateCopy.typeTicket = action.typeTicket;
            return stateCopy;
        }
        case directActionType: {
            let stateCopy = {...state};
            stateCopy.direct = action.direct;
            return stateCopy;
        }
        case ClickProcActionType:{
            let data = {
                id: action.idTicket,
                state_id: action.StatusId,
            };
            debugger
            axios.post("/Ticket/Update", data, [{'Content-Type': 'application/json'}])/*http://84.22.135.132:5000/*/
                .then(res => {
                    debugger
                    if (res.data.message === null) {
                        alert(res.data.error);
                    } else if (res.data.error === null) {
                        alert(res.data.message);
                    }

                });
            return state;
        }
        case ClickCompActionType:{
            let data = {
                id: action.idTicket,
                state_id: action.StatusId,
            };
            debugger
            axios.post("/Ticket/Update", data, [{'Content-Type': 'application/json'}])/*http://84.22.135.132:5000/*/
                .then(res => {
                    debugger
                    if (res.data.message === null) {
                        alert(res.data.error);
                    } else if (res.data.error === null) {
                        alert(res.data.message);
                    }

                });
            return state;
        }
            case imgActionType:{
                let stateCopy = {...state};
                stateCopy.img=action.img;
                debugger
                return stateCopy;
            }
        default:
            return state;
    }
}
export const selectChangeActionCreator = (id) => ({type: selectDirectTicActionType, id: id});
export const ClickDirectInfoActionCreator = (id) => ({type: ClickDirectInfoActionType, id: id});
export const dataActionCreator = (data) => ({type: dataActionType, data: data});
export const stateActionCreator = (state) => ({type: stateActionType, state: state});
export const typeActionCreator = (type) => ({type: typeActionType, typeTicket: type});
export const directActionCreator = (direct) => ({type: directActionType, direct: direct});
export const ClickProcActionCreator = (id_ticket,id_status) => ({type: ClickProcActionType, idTicket: id_ticket,
    StatusId:id_status});
export const ClickCompActionCreator = (id_ticket,id_status) => ({type: ClickCompActionType, idTicket: id_ticket,
StatusId:id_status});
export const imgActionCreator=(img)=>({type: imgActionType, img: img});
export default TicketReducer;