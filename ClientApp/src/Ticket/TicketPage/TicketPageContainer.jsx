import {connect} from "react-redux";
import TicketPage from "./TicketPage";
import {
    ClickDirectInfoActionCreator, dataActionCreator, directActionCreator,
    selectChangeActionCreator, stateActionCreator, typeActionCreator
} from "../../redux/reducer/TicketReducer";

let mapStateToProps = (state) => {
    return {
        /*PageTicket*/
        ticket: state.PageTicket.ticket,
        TypeTicket: state.PageTicket.typeTicket,
        optionValue: state.PageTicket.optionValue,
        NameForSelectDirect: state.PageTicket.NamesTickets[0].name,
        QuantityName: state.PageTicket.NamesTickets[1].name,
        StatusTicket: state.PageTicket.Status,
        directs: state.PageTicket.direct, ID: state.PageTicket.id,
        user: state.PageEntrance.user, links: state.PageTicket.links, link_id: state.PageTicket.link_id
    };
};
let mapDispatchToProps = (dispatch) => {
    return {
        UpdateIdSelect: (id) => {
            dispatch(selectChangeActionCreator(id))
        },
        ClickDirect: (id) => {
            dispatch(ClickDirectInfoActionCreator(id))
        },
        data: (data) => {
            dispatch(dataActionCreator(data))
        },
        state: (state) => {
            dispatch(stateActionCreator(state))
        },
        type: (type) => {
            dispatch(typeActionCreator(type))
        },
        direct: (direct) => {
            dispatch(directActionCreator(direct))
        }
    }
        ;
};
let TicketPageContainer = connect(mapStateToProps, mapDispatchToProps)(TicketPage);
export default TicketPageContainer;