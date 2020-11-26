import React from 'react';
import { Switch } from "react-router-dom";
import CreateUser from '../../../views/Users/CreateUser.js';
import { PrivateRoute } from './PrivateRoute.js';
import UpdateUser from '../../../views/Users/UpdateUser.js';

function RouteConfiguration(props) {

    return (
        <Switch>
            {/* User routes */}
            <PrivateRoute path="/admin/users/create" component={CreateUser} />
            <PrivateRoute path="/admin/users/:id" component={UpdateUser} />

            {/* Sidebar routes */}
            {/* This function must be placed at the end */}
            {props.routes.map((prop, key) => (
                <PrivateRoute
                    path={prop.layout + prop.path}
                    component={prop.component}
                    key={key}
                />
            ))}

        </Switch>
    );
}

export default RouteConfiguration;