import {
    onClickReturnActionCreator,
    onClickSendActionCreator,
    onEmailChangeForgotActionCreator
} from "../../redux/reducer/ForgotReducer";
import ForgotPasswordPage from "./ForgotPasswordPage";
import {connect} from "react-redux";

let mapStateToProps = (state) => {
    return {
        NamePage: state.PageForPas.NamesForPas[0].name,
        NameInfo: state.PageForPas.NamesForPas[1].name,
        NameInput: state.PageForPas.NamesForPas[2].name,
        NameForgotCancel: state.PageForPas.NamesForPas[3].name,
        NameSend: state.PageForPas.NamesForPas[4].name,
        valueEmailForgot: state.PageForPas.Email,
        type: state.PageForPas.Type,
        links: state.PageForPas.LinksForPas, link_id: state.PageForPas.link_id,
    };
};
let mapDispatchToProps = (dispatch) => {
    return {
        UpdateEmailForgot: (emailText) => {
            dispatch(onEmailChangeForgotActionCreator(emailText));
        },
        OnClickSend: (history) => {
            dispatch(onClickSendActionCreator(history));
        },
        OnClickReturn: (history) => {
            dispatch(onClickReturnActionCreator(history));
        }
    };
};
let ForgotContainer = connect(mapStateToProps, mapDispatchToProps)(ForgotPasswordPage);
export default ForgotContainer;