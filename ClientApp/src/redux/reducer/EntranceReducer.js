const onClickButtonEnterActionType = 'ON-CLICK-BUTTON-ENTER';
const onLoginChangeEnterActionType = 'UPDATE-LOGIN-ENTER';
const onChangePassEnterActionType = 'UPDATE-PASSWORD-ENTER';
const EntranceReducer = (state,action)=>{
    /*state=this._state.PageEntrance*/
    switch (action.type) {
        case onClickButtonEnterActionType:
            alert(state.Login+" "+state.Password);

            state.Login="";
            state.Password="";
            state.PasswordText="";
            return state;
        case onLoginChangeEnterActionType:
            state.Login=action.login;
            return state;
        case onChangePassEnterActionType:
            let length=action.pass.length;
            state.Password=state.Password+action.pass[length-1];
            let text="";
            for(let i of action.pass){
                text=text+"*"
            }
            state.PasswordText=text;
            return state;
        default:return state;
    }
}

export default EntranceReducer;