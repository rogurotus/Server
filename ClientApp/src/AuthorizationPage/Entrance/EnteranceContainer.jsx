import {
    onClickEnterActionCreator, onClickForForgotActionCreator, onClickForRegActionCreator,
    onLoginChangeEnterActionCreator,
    onPassChangeEnterActionCreator
} from "../../redux/reducer/EntranceReducer";
import Entrance from "./Entrance";
import {connect} from "react-redux";

const mapStateToProps = (state) => {
    return {
        NameEntr: state.PageEntrance.NamesEntrance[0].name,
        NameLogin: state.PageEntrance.NamesEntrance[1].name,
        NamePassword: state.PageEntrance.NamesEntrance[2].name,
        NameForgot: state.PageEntrance.NamesEntrance[3].name,
        NameReg: state.PageEntrance.NamesEntrance[4].name,
        links: state.PageEntrance.LinksEntrance, link_id: state.PageEntrance.link_id,
        valueLogin: state.PageEntrance.Login,
        valuePass: state.PageEntrance.PasswordText,
        type: state.PageEntrance.Type, length: state.PageEntrance.length
    };
};
const mapDispatchToProps = (dispatch) => {
    return {
        onClickEnter: (history) => {
            dispatch(onClickEnterActionCreator(history))
        },
        onClickForgot: (history) => {
            dispatch(onClickForForgotActionCreator(history))
        },
        onClickReg: (history) => {
            dispatch(onClickForRegActionCreator(history))
        },
        onLoginChangeEnter: (loginText) => {
            dispatch(onLoginChangeEnterActionCreator(loginText))
        },
        onPassChangeEnter: (passText, length) => {
            dispatch(onPassChangeEnterActionCreator(passText, length))
        }
    };
};
const EntranceContainer = connect(mapStateToProps, mapDispatchToProps)(Entrance);
export default EntranceContainer;