import {connect} from "react-redux";
import TicketInfoPage from "./TicketInfoPage";
import {
    ClickCompActionCreator,
    ClickProcActionCreator,
    historyActionCreator,
    imgActionCreator
} from "../../redux/reducer/TicketReducer";

let mapStateToProps = (state) => {
    return {
        Ticket: state.PageTicketInfo.TicketInfo,
        HistoryText: state.PageTicketInfo.history,
        NameTicket: state.PageTicketInfo.NamesTicketInfo[0].name,
        SideBarName: state.PageTicketInfo.NamesTicketInfo[1].name,
        DetailName: state.PageTicketInfo.NamesTicketInfo[2].name,
        HistoryTicket: state.PageTicketInfo.NamesTicketInfo[3].name,
        Img: state.PageTicketInfo.NamesTicketInfo[4].name,
        StateTicket: state.PageTicketInfo.Status, image: state.PageTicketInfo.img,
        user: state.PageEntrance.user, links: state.PageTicket.links, link_id: state.PageTicket.link_id
    };
};
let mapDispatchToProps = (dispatch) => {
    return {
        ClickProc: (id_ticket, id_status) => {
            debugger
            dispatch(ClickProcActionCreator(id_ticket, id_status))
        },
        ClickComp: (id_ticket, id_status) => {
            debugger
            dispatch(ClickCompActionCreator(id_ticket, id_status))
        },
        img: (img) => {
            dispatch(imgActionCreator(img))
        },
        Hist: (history) => {
            dispatch(historyActionCreator(history))
        }
    };
};
let TicketInfoContainer = connect(mapStateToProps, mapDispatchToProps)(TicketInfoPage);
export default TicketInfoContainer;