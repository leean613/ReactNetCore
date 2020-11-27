import { callApi } from "../../utils/apiCaller";

const login = async (username, password) => {
    const params = { username: username, password: password };
    return await callApi("api/glotech/login", "POST", params);
}

const logout = () => {
    // remove user from local storage to log user out
    localStorage.removeItem('currentUser');
    localStorage.removeItem('token');
}

export const authenticationService = {
    login,
    logout,
    get token() {
        const token = localStorage.getItem('token') ? localStorage.getItem('token') : null;
        return token
    }
};
