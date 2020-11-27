import React from 'react';
import { Router, Route, Switch, Redirect } from "react-router-dom";
import AdminLayout from "./../../layouts/Admin.js";
import Login from "./../../views/Login/Login.js";
import { createBrowserHistory } from "history";
import { GlobalProvider } from '../../context/Provider.js';

function App(props) {

    const hist = createBrowserHistory();

    return (
        <GlobalProvider>
            <Router history={hist}>
                <Switch>
                    <Route path="/admin" render={(props) => <AdminLayout {...props} />} />
                    <Route path="/login" render={(props) => <Login {...props} />}></Route>
                    <Redirect to="/login"></Redirect>
                </Switch>
            </Router>
        </GlobalProvider>
    );
}

export default App;