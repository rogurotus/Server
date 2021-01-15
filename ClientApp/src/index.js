import React from 'react';
import ReactDOM from "react-dom";
import './index.css';
import store from "./redux/store";
import App from "./App";

let rerenderTree= (state)=>{
    ReactDOM.render(
        <React.StrictMode>
            <App state={store.getState()}
                 dispatch={store.dispatch.bind(store)}
            />
        </React.StrictMode>,
        document.getElementById('root')
    );
}
rerenderTree(store.getState());
store.subscriber(rerenderTree);



