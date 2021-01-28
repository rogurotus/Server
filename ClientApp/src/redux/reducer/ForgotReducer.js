const updateEmailForgotActionType = 'UPDATE-EMAIL-FORGOT';
const onClickButtonReturnActionType = 'ON-CLICK-BUTTON-RETURN';
const onClickButtonSendActionType = 'ON-CLICK-BUTTON-SEND';
let InitialState = {
    NamesForPas: [
        {id: 0, name: "Сброс пароля"},
        {
            id: 1,
            name: "Чтобы сбросить пароль, укажите свой адрес электронной почты. Вам будет отправлено письмо со ссылкой для входа."
        },
        {id: 2, name: "Email Адрес"},
        {id: 3, name: "Отмена"},
        {id: 4, name: "Отправить письмо"}
    ], link_id: 1,
    LinksForPas: [
        {id: 0, links: "/Authorization/Entrance"},
        {id: 1, links: "/Authorization/ForgotPassword"}
    ],
    Email: "", Type: "text"
}
const ForgotReducer = (state = InitialState, action) => {
    /*state=this._state.PageReg*/
    switch (action.type) {
        case onClickButtonReturnActionType: {
            let stateCopy = {...state};
            stateCopy.Email = "";
            action.history.push(stateCopy.LinksForPas[0].links);
            return stateCopy;
        }
        case onClickButtonSendActionType: {
            let stateCopy = {...state};
            alert("Письмо отправленно на электронную почту: " + stateCopy.Email);
            stateCopy.Email = "";
            action.history.push(stateCopy.LinksForPas[0].links);
            return stateCopy;
        }
        case updateEmailForgotActionType: {
            let stateCopy = {...state};
            stateCopy.Email = action.email;
            return stateCopy;
        }
        default:
            return state;
    }
}
export const onEmailChangeForgotActionCreator = (emailText) => (
    {type: updateEmailForgotActionType, email: emailText});
export const onClickReturnActionCreator = (history) => ({type: onClickButtonReturnActionType, history: history});
export const onClickSendActionCreator = (history) => ({type: onClickButtonSendActionType, history: history});
export default ForgotReducer;