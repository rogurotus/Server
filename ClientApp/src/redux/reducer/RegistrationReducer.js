const onClickButtonRegActionType = 'ON-CLICK-BUTTON-REG';
const updateLoginRegActionType = 'UPDATE-LOGIN-REG';
const updatePasswordRegActionType = 'UPDATE-PASSWORD-REG';
const updatePasswordRepRegActionType = 'UPDATE-PASSWORD-REP-REG';
const updateEmailRegActionType = 'UPDATE-EMAIL-REG';
const RegistrationReducer = (state,action)=>{
    /*state=this._state.PageReg*/
    switch (action.type) {
        case onClickButtonRegActionType:
            alert(state.Login+ " " + state.Password + " " + state.PassRep + " " + state.Email);
            state.Login="";
            state.Password="";
            state.PasswordText="";
            state.PassRep="";
            state.PassRepText="";
            state.Email="";
            return state;
        case updateLoginRegActionType:
            state.Login=action.login;
            return state;
        case updatePasswordRegActionType:
            let length=action.pass.length;
            state.Password=state.Password+action.pass[length-1];
            let text="";
            for(let i of action.pass){
                text=text+"*"
            }
            state.PasswordText=text;
            return state;
        case updatePasswordRepRegActionType:
            let lengthRep=action.PassRep.length;
            state.PassRep=state.PassRep+action.PassRep[lengthRep-1];
            let textRep="";
            for(let i of action.PassRep){
                textRep=textRep+"*"
            }
            state.PassRepText=textRep;
            return state;
        case updateEmailRegActionType:
            state.Email=action.email;
            return state;
        default:return state;
    }
}

export default RegistrationReducer;