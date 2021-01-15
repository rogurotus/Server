const onChangeProcessingTicActionType = 'ON-CHANGE-PROCESSING-TIC';
const onChangeCompletedTicActionType = 'ON-CHANGE-COMPLETED-TIC';
const selectDirectTicActionType = 'SELECT-DIRECT-TIC';
const TicketReducer = (state, action)=>{
    /*state=this._state.PageTiket*/
    switch (action.type) {
        case onChangeProcessingTicActionType:
            state.Directs[action.directid].TiketsDirect[action.tiketsid].defEnrolled=false;
            state.Directs[action.directid].TiketsDirect[action.tiketsid].disEnrolled=true;
            state.Directs[action.directid].TiketsDirect[action.tiketsid].defProcessing=true;
            state.Directs[action.directid].TiketsDirect[action.tiketsid].disProcessing=true;
            state.Directs[action.directid].TiketsDirect[action.tiketsid].defCompleted=false;
            state.Directs[action.directid].TiketsDirect[action.tiketsid].disCompleted=false;
            return state;
        case onChangeCompletedTicActionType:
            state.Directs[action.directid].TiketsDirect[action.tiketsid].defEnrolled=false;
            state.Directs[action.directid].TiketsDirect[action.tiketsid].disEnrolled=true;
            state.Directs[action.directid].TiketsDirect[action.tiketsid].defProcessing=false;
            state.Directs[action.directid].TiketsDirect[action.tiketsid].disProcessing=true;
            state.Directs[action.directid].TiketsDirect[action.tiketsid].defCompleted=true;
            state.Directs[action.directid].TiketsDirect[action.tiketsid].disCompleted=true;
            return state;
        case selectDirectTicActionType:
            state.id=action.id;
            return state;
        default:return state;
    }
}

export default TicketReducer;