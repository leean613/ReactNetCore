import {
    LOGIN_LOADING,
    LOGIN_SUCCESS,
    LOGIN_FAILED,
    LOGOUT
} from "../constants/actionTypes/auth.js";

const auth = (state, { payload, type }) => {
    switch (type) {
        case LOGIN_LOADING:
            return {
                auth: {
                    ...state.auth,
                    isLoading: true
                }
            }

        case LOGIN_SUCCESS:
            return {
                auth: {
                    ...state.auth,
                    isLoading: false,
                    data: payload,
                    isFailed: false,
                    isSuccess: true
                }
            }

        case LOGIN_FAILED:
            return {
                auth: {
                    ...state.auth,
                    isLoading: false,
                    data: payload,
                    isFailed: true,
                    isSuccess: false
                }
            }

        default:
            return state
    }
}

export default auth;