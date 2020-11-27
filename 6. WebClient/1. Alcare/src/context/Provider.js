import React, { createContext, useReducer } from "react";
import authInitialState from "./initialstates/authInitialState";
import auth from "./reducers/auth";

export const GlobalContext = createContext({});

export const GlobalProvider = ({ children }) => {
    const [authState, authDispatch] = useReducer(auth, authInitialState);

    return (
        <GlobalContext.Provider
            value={{
                authState,
                authDispatch
            }}
        >
            {children}
        </GlobalContext.Provider>
    )
}