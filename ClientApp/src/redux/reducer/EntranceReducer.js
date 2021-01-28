import * as axios from "axios";

const onClickButtonEnterActionType = 'ON-CLICK-BUTTON-ENTER';
const onLoginChangeEnterActionType = 'UPDATE-LOGIN-ENTER';
const onChangePassEnterActionType = 'UPDATE-PASSWORD-ENTER';
const onClickButtonForForgotActionType = 'ON-CLICK-BUTTON-FORGOT';
const onClickButtonForRegActionType = 'ON-CLICK-BUTTON-REG';
let InitialState = {
    NamesEntrance: [
        {id: 0, name: "Войти"},
        {id: 1, name: "Имя пользователя или Email"},
        {id: 2, name: "Пароль"},
        {id: 3, name: "Забыли свой пароль?"},
        {id: 4, name: "Регистрация"}
    ], link_id: 3, user: "",
    LinksEntrance: [
        {id: 0, links: "/Authorization/ForgotPassword"},
        {id: 1, links: "/Authorization/Registration"},
        {id: 2, links: "/TicketPage"},
        {id: 3, links: "/Authorization/Entrance"}
    ],
    Login: "",
    Password: "", PasswordText: "", Type: "text", length: 0
}
const EntranceReducer = (state = InitialState, action) => {
    /*state=this._state.PageEntrance*/
    let stateCopy = {};

    switch (action.type) {
        case onClickButtonForRegActionType: {
            stateCopy = {...state};
            stateCopy.Login = "";
            stateCopy.Password = "";
            stateCopy.PasswordText = "";
            stateCopy.length = 0;
            action.history.push(stateCopy.LinksEntrance[1].links);
            return (stateCopy);
        }
        case onClickButtonForForgotActionType: {
            stateCopy = {...state};
            stateCopy.Login = "";
            stateCopy.Password = "";
            stateCopy.PasswordText = "";
            stateCopy.length = 0;
            action.history.push(stateCopy.LinksEntrance[0].links);
            return (stateCopy);
        }
        case onClickButtonEnterActionType: {
            stateCopy = {...state};
            if ((stateCopy.Login !== "") && (stateCopy.Password !== "")) {
                let data = {
                    login: stateCopy.Login,
                    pass: stateCopy.Password,
                    email: "admin1",
                    // same for other inputs ..
                };
                /*let V="http://84.22.135.132:5000";*/
                axios.post("/WebUser/Login", data, [{'Content-Type': 'application/json'}])
                    .then(res => {
                        if (res.data.message === null) {
                            alert(res.data.error);
                        } else if (res.data.error === null) {
                            alert(res.data.message);
                            action.history.push(stateCopy.LinksEntrance[2].links);
                        }
                    });
            } else {
                alert("Поля пустые")
            }
            stateCopy.user = stateCopy.Login;
            stateCopy.Login = "";
            stateCopy.Password = "";
            stateCopy.PasswordText = "";
            stateCopy.length = 0;
            return stateCopy;
        }
        case onLoginChangeEnterActionType:
            stateCopy = {...state};
            stateCopy.Login = action.login;
            return stateCopy;
        case onChangePassEnterActionType:
            stateCopy = {...state};
            if (action.pass.length > stateCopy.length) {
                let passArray = Array.from(action.pass);
                passArray.map((s) => {
                    if (s !== "*") {
                        stateCopy.Password += s;
                        stateCopy.PasswordText += "*";
                    }
                });
            } else {
                let passTextArray = Array.from(stateCopy.PasswordText);
                let PassArray = Array.from(stateCopy.Password);
                stateCopy.PasswordText = "";
                stateCopy.Password = "";
                for (let i = 0; i < action.pass.length; i++) {
                    stateCopy.PasswordText += passTextArray[i];
                    stateCopy.Password += PassArray[i];
                }
            }
            stateCopy.length = action.pass.length;
            return stateCopy;
        default:
            return state;
    }
}
export const onClickEnterActionCreator = (history) => ({type: onClickButtonEnterActionType, history: history});
export const onClickForForgotActionCreator = (history) => ({type: onClickButtonForForgotActionType, history: history});
export const onClickForRegActionCreator = (history) => ({type: onClickButtonForRegActionType, history: history});
export const onLoginChangeEnterActionCreator = (loginText) => (
    {type: onLoginChangeEnterActionType, login: loginText});
export const onPassChangeEnterActionCreator = (passText, length) => (
    {type: onChangePassEnterActionType, pass: passText, length: length});
export default EntranceReducer;